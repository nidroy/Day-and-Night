using UnityEngine;

/// <summary>
/// Реализация лог-синка для вывода сообщений в консоль Unity.
/// Перенаправляет сообщения в Debug.Log, Debug.LogWarning и Debug.LogError в зависимости от уровня логирования.
/// </summary>
public class UnityConsoleLogSink : ILogSink
{
    /// <summary>
    /// Метод записывает сообщение в консоль Unity.
    /// </summary>
    /// <param name="level">Уровень важности сообщения.</param>
    /// <param name="source">Источник сообщения.</param>
    /// <param name="message">Текст сообщения.</param>
    public void Write(LogLevel level, string source, string message)
    {
        // Формируем базовую строку лога.
        string formattedMessage = LoggerMessageFormatter.Format(level, source, message);

        // Подсвечиваем сообщение в зависимости от уровня.
        string coloredMessage = ApplyColor(level, formattedMessage);

        switch (level)
        {
            case LogLevel.Info:
                Debug.Log(coloredMessage);
                break;

            case LogLevel.Warning:
                Debug.LogWarning(coloredMessage);
                break;

            case LogLevel.Error:
                Debug.LogError(coloredMessage);
                break;
        }
    }



    /// <summary>
    /// Метод применяет цвет к сообщению в зависимости от уровня важности.
    /// 
    /// Info      — обычный цвет текста.
    /// Warning   — жёлтый цвет.
    /// Error     — красный цвет.
    /// </summary>
    /// <param name="level">Уровень важности сообщения.</param>
    /// <param name="message">Текст сообщения.</param>
    /// <returns>Сообщение с тегами rich text при необходимости.</returns>
    private string ApplyColor(LogLevel level, string message)
    {
        switch (level)
        {
            case LogLevel.Warning:
                return $"<color=#FFD54F>{message}</color>";

            case LogLevel.Error:
                return $"<color=#EF5350>{message}</color>";

            case LogLevel.Info:
            default:
                return message;
        }
    }
}