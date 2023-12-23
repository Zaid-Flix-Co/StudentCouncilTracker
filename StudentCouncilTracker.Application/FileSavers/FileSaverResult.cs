namespace StudentCouncilTracker.Application.FileSavers;

/// <summary>
/// Class FileSaverResult.
/// </summary>
public class FileSaverResult
{
    /// <summary>
    /// Gets or sets the bytes.
    /// </summary>
    /// <value>The bytes.</value>
    public byte[] Bytes { get; set; }

    /// <summary>
    /// Gets or sets the type of the content.
    /// </summary>
    /// <value>The type of the content.</value>
    public string ContentType { get; set; }

    /// <summary>
    /// Gets or sets the filename.
    /// </summary>
    /// <value>The filename.</value>
    public string Filename { get; set; }
}