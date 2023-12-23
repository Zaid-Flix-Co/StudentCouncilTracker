namespace StudentCouncilTracker.Application.Services.Hubs.Progress;

/// <summary>
/// Класс вывода прогресса
/// </summary>
public class Progress
{
    /// <summary>
    /// Текущее кол-во
    /// </summary>
    /// <value>The count.</value>
    public int Count { get; set; }

    /// <summary>
    /// Всего
    /// </summary>
    /// <value>The total.</value>
    public int Total { get; set; }

    /// <summary>
    /// Сообщение
    /// </summary>
    /// <value>The message.</value>
    public string Message { get; set; }

    /// <summary>
    /// Процент выполнения
    /// </summary>
    /// <value>The percent.</value>
    public double Percent => Total == 0
        ? 0
        : Count / (double)Total * 100;
}