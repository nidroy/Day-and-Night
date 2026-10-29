using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

/// <summary>
/// Реализация лог-синка для вывода сообщений во внутриигровую консоль.
/// </summary>
public class GameConsoleLogSink : ILogSink
{
    /// <summary>
    /// Текстовый объект, в который выводятся сообщения консоли.
    /// </summary>
    private readonly TMP_Text _consoleText;

    /// <summary>
    /// Максимальное количество строк, которое может храниться в консоли.
    /// </summary>
    private readonly int _maxLines;

    /// <summary>
    /// Буфер строк консоли.
    /// Хранит историю сообщений в том порядке, в котором они были добавлены.
    /// </summary>
    private readonly Queue<string> _lines = new Queue<string>();



    /// <summary>
    /// Конструктор создаёт новый лог-синк для внутриигровой консоли.
    /// </summary>
    /// <param name="consoleText">Текстовый объект, отображающий консоль.</param>
    /// <param name="maxLines">Максимальное количество строк в консоли.</param>
    public GameConsoleLogSink(TMP_Text consoleText, int maxLines = 100)
    {
        _consoleText = consoleText;
        _maxLines = Mathf.Max(1, maxLines);
    }



    /// <summary>
    /// Метод записывает сообщение в внутриигровую консоль.
    /// </summary>
    /// <remarks>
    /// Метод должен вызываться из главного потока Unity, так как обновляет UI.
    /// </remarks>
    /// <param name="level">Уровень важности сообщения.</param>
    /// <param name="source">Источник сообщения.</param>
    /// <param name="message">Текст сообщения.</param>
    public void Write(LogLevel level, string source, string message)
    {
        if (_consoleText == null)
        {
            return;
        }

        string formattedMessage = LoggerMessageFormatter.Format(level, source, message);

        string coloredMessage = LoggerMessageFormatter.ApplyColor(level, formattedMessage);

        _lines.Enqueue(coloredMessage);

        DeleteOldLines();

        RefreshConsoleText();
    }



    /// <summary>
    /// Метод удаляет самые старые строки, если их количество превышает допустимый лимит.
    /// </summary>
    private void DeleteOldLines()
    {
        while (_lines.Count > _maxLines)
        {
            _lines.Dequeue();
        }
    }

    /// <summary>
    /// Метод обновляет текст консоли.
    /// </summary>
    private void RefreshConsoleText()
    {
        StringBuilder stringBuilder = new StringBuilder();

        foreach (string line in _lines)
        {
            stringBuilder.AppendLine(line);
        }

        _consoleText.text = stringBuilder.ToString().TrimEnd('\r', '\n');
    }
}