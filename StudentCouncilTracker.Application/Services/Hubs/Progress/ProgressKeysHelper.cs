namespace StudentCouncilTracker.Application.Services.Hubs.Progress;

/// <summary>
/// Class ProgressKeysHelper.
/// </summary>
public static class ProgressKeysHelper
{
    /// <summary>
    /// Reports the catalog client.
    /// </summary>
    /// <param name="userName">Name of the user.</param>
    /// <returns>System.String.</returns>
    public static string REPORT_CATALOG_CLIENT(string userName) => $"{nameof(REPORT_CATALOG_CLIENT)}_{userName}";

    /// <summary>
    /// Reports the catalog organization.
    /// </summary>
    /// <param name="userName">Name of the user.</param>
    /// <returns>System.String.</returns>
    public static string REPORT_CATALOG_ORGANIZATION(string userName) => $"{nameof(REPORT_CATALOG_ORGANIZATION)}_{userName}";

    /// <summary>
    /// Reports the catalog stock.
    /// </summary>
    /// <param name="userName">Name of the user.</param>
    /// <returns>System.String.</returns>
    public static string REPORT_CATALOG_STOCK(string userName) => $"{nameof(REPORT_CATALOG_STOCK)}_{userName}";

    /// <summary>
    /// Reports the catalog bonus.
    /// </summary>
    /// <param name="userName">Name of the user.</param>
    /// <returns>System.String.</returns>
    public static string REPORT_CATALOG_BONUS(string userName) => $"{nameof(REPORT_CATALOG_BONUS)}_{userName}";

    /// <summary>
    /// Reports the catalog combination.
    /// </summary>
    /// <param name="userName">Name of the user.</param>
    /// <returns>System.String.</returns>
    public static string REPORT_CATALOG_COMBINATION(string userName) => $"{nameof(REPORT_CATALOG_COMBINATION)}_{userName}";

    /// <summary>
    /// Reports the catalog sticker.
    /// </summary>
    /// <param name="userName">Name of the user.</param>
    /// <returns>System.String.</returns>
    public static string REPORT_CATALOG_STICKER(string userName) => $"{nameof(REPORT_CATALOG_STICKER)}_{userName}";
}