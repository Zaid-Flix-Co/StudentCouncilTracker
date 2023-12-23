using Microsoft.AspNetCore.StaticFiles;

namespace StudentCouncilTracker.Application.FileSavers;

public static class FileSaverMimeTypeHelper
{
    public static string GetContentType(string name)
    {
        var fName = Path.GetFileName(name);
        new FileExtensionContentTypeProvider().TryGetContentType(fName, out var contentType);
        return contentType ?? "application/octet-stream";
    }

    public static bool IsImage(string contentType)
    {
        return ImageTypes.Find(f => f.Equals(contentType, StringComparison.OrdinalIgnoreCase)) != null;
    }

    private static readonly List<string> ImageTypes = new()
    {
        "image/bmp",
        "image/png",
        "image/apng",
        "image/cis-cod",
        "image/gif",
        "image/webp",
        "image/ief",
        "image/jpeg",
        "image/pjpeg",
        "image/jpeg",
        "image/pipeg",
        "image/svg+xml",
        "image/tiff",
        "image/vnd.microsoft.icon",
        "image/vnd.wap.wbmp",
        "image/x-cmu-raster",
        "image/x-cmx",
        "image/x-icon",
        "image/x-portable-anymap",
        "image/x-portable-bitmap",
        "image/x-portable-graymap",
        "image/x-portable-pixmap",
        "image/x-rgb",
        "image/x-xbitmap",
        "image/x-xpixmap",
        "image/x-xwindowdump"
    };
}