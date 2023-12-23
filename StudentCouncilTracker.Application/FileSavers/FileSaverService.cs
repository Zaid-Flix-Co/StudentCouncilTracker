using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using Softius_Extensions_NetStandart;
using StudentCouncilTracker.Application.Extensions;

namespace StudentCouncilTracker.Application.FileSavers;

public class FileSaverService(IConfiguration configuration, IHostingEnvironment env)
{
    private string _getFilePathOnDisk(FileSaverHolderBase model, bool isPreview)
    {
        return Path.Combine(env.ContentRootPath, _getFilePath(model, isPreview));
    }

    private string _getFilePath(FileSaverHolderBase model, bool isPreview)
    {
        return Path.Combine("files", model.Folder, model.SubFolder, isPreview ? model.FileNamePreviewOnDisk : model.FileNameOnDisk);
    }

    public string GetFilePath(FileSaverHolderBase model)
    {
        return _getFilePathOnDisk(model, false);
    }

    public string GetUrl(FileSaverHolderBase model)
    {
        return Path.Combine(configuration["FILES_BASE_URL"], _getFilePath(model, false));
    }

    public string GetUrlPreview(FileSaverHolderBase model)
    {
        return Path.Combine(configuration["FILES_BASE_URL"], _getFilePath(model, true));
    }

    public async Task<FileSaverHolderBase> UploadFile(FileSaverHolderBase model, IFormFile file)
    {
        //Главная папка
        var dir = Path.Combine(env.ContentRootPath, "files");
        var mainfolder = Path.Combine(dir, model.Folder);
        if (!Directory.Exists(mainfolder))
            Directory.CreateDirectory(mainfolder);

        //Подпапка с конкретным id например
        var subfolder = Path.Combine(mainfolder, model.SubFolder);
        if (!Directory.Exists(subfolder))
            Directory.CreateDirectory(subfolder);

        var randomFileName = RandomExtensions.GetRandomString(8);
        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file.FileName);
        var extension = Path.GetExtension(file.FileName);
        var storeFileName = $"{fileNameWithoutExtension}__{randomFileName}{extension}";//Изначальное имя файла, которое в последствии может быть урезано/модифицировано (исключая невалидные символы)
        var filePath = Path.Combine(subfolder, storeFileName);
        var correctFilePath = filePath.GetValidFilePath();//Поправили конечный путь до файла как следует
        model.FileNameOnDisk = Path.GetFileName(correctFilePath);//запомнили конечное имя файла. Полный путь не храним

        if (File.Exists(correctFilePath))
            File.Delete(correctFilePath);

        if (correctFilePath == null)
            return model;

        await using (var stream = File.Create(correctFilePath))
        {
            await file.CopyToAsync(stream);
        }

        if (!model.IsImage)
            return model;

        var storeFileNamePreview = $"{fileNameWithoutExtension}__{randomFileName}__preview{extension}"; //Изначальное имя файла, которое в последствии может быть урезано/модифицировано (исключая невалидные символы)
        var filePathPreview = Path.Combine(subfolder, storeFileNamePreview);
        var correctFilePathPreview = filePathPreview.GetValidFilePath(); //Поправили конечный путь до файла как следует
        model.FileNamePreviewOnDisk = Path.GetFileName(correctFilePathPreview);

        if (File.Exists(correctFilePathPreview))
            File.Delete(correctFilePathPreview);

        using var image = await Image.LoadAsync(correctFilePath);
        image.Mutate(x => x.Resize(Width, Height));
        await image.SaveAsync(correctFilePathPreview);
        return model;
    }

    private int Width =>
        int.TryParse(configuration["Files:Preview:Width"], out var ret)
            ? ret
            : 70;

    private int Height =>
        int.TryParse(configuration["Files:Preview:Height"], out var ret)
            ? ret
            : 70;

    public async Task<FileSaverResult> DownloadFile(FileSaverHolderBase model)
    {
        return await GetDownloadResult(_getFilePathOnDisk(model, false), model.FileName);
    }

    public async Task<FileSaverResult> DownloadImagePreview(FileSaverHolderBase model)
    {
        return model.IsImage
            ? await GetDownloadResult(_getFilePathOnDisk(model, true), model.FileName)
            : new FileSaverResult();
    }

    public bool DeleteFile(FileSaverHolderBase model)
    {
        if (!File.Exists(_getFilePathOnDisk(model, false)))
            return false;

        try
        {
            File.Delete(_getFilePathOnDisk(model, false));
            if (model.IsImage)
                File.Delete(_getFilePathOnDisk(model, true));
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }

    public async Task<FileSaverResult> GetDownloadResult(string filePath, string filename)
    {
        if (!File.Exists(filePath))
            return new FileSaverResult();

        var fileBytes = await File.ReadAllBytesAsync(filePath);
        var contentType = FileSaverMimeTypeHelper.GetContentType(filePath);

        return new FileSaverResult
        {
            Bytes = fileBytes,
            Filename = filename,
            ContentType = contentType
        };
    }
}