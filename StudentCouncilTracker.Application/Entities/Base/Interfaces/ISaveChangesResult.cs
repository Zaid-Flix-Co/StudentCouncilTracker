namespace StudentCouncilTracker.Application.Entities.Base.Interfaces;

/// <summary>
/// Interface ISaveChangesResult
/// </summary>
public interface ISaveChangesResult
{
    /// <summary>
    /// Gets or sets the exception.
    /// </summary>
    /// <value>The exception.</value>
    Exception? Exception { get; set; }

    /// <summary>
    /// Gets a value indicating whether this instance is ok.
    /// </summary>
    /// <value><c>true</c> if this instance is ok; otherwise, <c>false</c>.</value>
    bool IsOk { get; }
}