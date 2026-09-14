using System;
using TMPro;
using UnityEngine;

/// <summary>
/// Класс для логирования сообщений приложения.
/// </summary>
public static class Logger
{
    /// <summary>
    /// Максимальный размер файла логов в мегабайтах.
    /// </summary>
    private static readonly int _maxLogFileSizeMB = Globals.MaxLogFileSizeMB;

    /// <summary>
    /// Максимальное количество строк во внутриигровой консоли.
    /// </summary>
    private static readonly int _gameConsoleMaxLines = Globals.GameConsoleMaxLines;

    /// <summary>
    /// Полный путь к файлу логов.
    /// </summary>
    private static readonly string _logFilePath = Globals.LogFilePath;



    /// <summary>
    /// Лог-синк для вывода сообщений в консоль Unity.
    /// </summary>
    private static ILogSink _unityConsoleLogSink = new UnityConsoleLogSink();

    /// <summary>
    /// Лог-синк для записи сообщений в файл.
    /// </summary>
    private static ILogSink _fileLogSink = new FileLogSink(_logFilePath, _maxLogFileSizeMB);

    /// <summary>
    /// Лог-синк для внутриигровой консоли.
    /// </summary>
    private static ILogSink _gameConsoleLogSink;



    /// <summary>
    /// Флаг, указывающий, включено ли файловое логирование.
    /// </summary>
    private static bool _isFileLoggingEnabled = true;

    /// <summary>
    /// Флаг, указывающий, включён ли вывод во внутриигровую консоль.
    /// </summary>
    private static bool _isGameConsoleLoggingEnabled = true;



    /// <summary>
    /// Флаг возвращает текущее состояние файлового логирования.
    /// </summary>
    public static bool IsFileLoggingEnabled => _isFileLoggingEnabled;

    /// <summary>
    /// Возвращает текущее состояние логирования во внутриигровую консоль.
    /// </summary>
    public static bool IsGameConsoleLoggingEnabled => _isGameConsoleLoggingEnabled;



    /// <summary>
    /// Метод инициализирует внутриигровую консоль в системе логирования.
    /// </summary>
    /// <param name="consoleText">Текстовый объект консоли.</param>
    public static void InitializeGameConsole(TMP_Text consoleText)
    {
        _gameConsoleLogSink = new GameConsoleLogSink(consoleText, _gameConsoleMaxLines);
    }



    /// <summary>
    /// Метод включает или отключает запись логов в файл.
    /// </summary>
    /// <param name="enabled">true — включить файловое логирование, false — отключить.</param>
    public static void ToggleFileLogging(bool enabled)
    {
        _isFileLoggingEnabled = enabled;

        if (enabled && _fileLogSink == null)
        {
            _fileLogSink = new FileLogSink(_logFilePath, _maxLogFileSizeMB);
        }
    }

    /// <summary>
    /// Метод включает или отключает вывод логов во внутриигровую консоль.
    /// </summary>
    /// <param name="enabled">true — включить вывод в игровую консоль, false — отключить.</param>
    public static void ToggleGameConsoleLogging(bool enabled)
    {
        _isGameConsoleLoggingEnabled = enabled;
    }



    /// <summary>
    /// Метод записывает сообщение лога в доступные каналы вывода.
    /// </summary>
    /// <param name="level">Уровень важности сообщения.</param>
    /// <param name="source">Источник сообщения.</param>
    /// <param name="message">Текст сообщения.</param>
    public static void Log(LogLevel level, string source, string message)
    {
        try
        {
            _unityConsoleLogSink?.Write(level, source, message);

            if (_isGameConsoleLoggingEnabled)
            {
                _gameConsoleLogSink?.Write(level, source, message);
            }

            if (_isFileLoggingEnabled)
            {
                _fileLogSink?.Write(level, source, message);
            }
        }
        catch (Exception exception)
        {
            Debug.LogError($"Logger internal error: {exception.Message}");
        }
    }
}