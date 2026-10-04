using System;

/// <summary>
/// Вспомогательный класс для форматирования лог-сообщения.
/// Преобразует уровень, источник и текст сообщения в единый строковый формат.
/// </summary>
public static class LoggerMessageFormatter
{
    /// <summary>
    /// Метод форматирует лог-сообщение в строку с датой, уровнем, источником и текстом.
    /// </summary>
    /// <param name="level">Уровень важности сообщения.</param>
    /// <param name="source">Источник сообщения, например имя класса.</param>
    /// <param name="message">Текст сообщения.</param>
    /// <returns>Сформированная строка лога.</returns>
    public static string Format(LogLevel level, string source = "Unknown", string message = "Empty message")
    {
        source = string.IsNullOrWhiteSpace(source) ? "Unknown" : source;
        message = string.IsNullOrWhiteSpace(message) ? "Empty message" : message;

        return $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] [{source}] {message}";
    }
}