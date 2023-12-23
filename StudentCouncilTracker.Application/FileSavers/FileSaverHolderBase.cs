using System.ComponentModel.DataAnnotations;

namespace StudentCouncilTracker.Application.FileSavers;

public abstract class FileSaverHolderBase
{
    public abstract string Folder { get; }

    public abstract string SubFolder { get; }

    public long Size { get; set; }

    [MaxLength(300)]
    public string FileName { get; set; } = null!;

    [MaxLength(300)]
    public string FileNameOnDisk { get; set; } = null!;

    [MaxLength(300)]
    public string? FileNamePreviewOnDisk { get; set; }

    public bool IsImage
    {
        get
        {
            var contentType = FileSaverMimeTypeHelper.GetContentType(FileName);
            return FileSaverMimeTypeHelper.IsImage(contentType);
        }
    }

    public string Extension => Path.GetExtension(FileName);
}