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
    private static readonly ILogSink _unityConsoleLogSink = new UnityConsoleLogSink();

    /// <summary>
    /// Лог-синк для записи сообщений в файл.
    /// </summary>
    private static ILogSink _fileLogSink;

    /// <summary>
    /// Лог-синк для внутриигровой консоли.
    /// </summary>
    private static ILogSink _gameConsoleLogSink;



    /// <summary>
    /// Флаг, указывающий, включено ли файловое логирование.
    /// </summary>
    private static bool _isFileLoggingEnabled = Globals.SettingsDefaultIsFileLogging;

    /// <summary>
    /// Флаг, указывающий, включён ли вывод во внутриигровую консоль.
    /// </summary>
    private static bool _isGameConsoleLoggingEnabled = Globals.SettingsDefaultIsGameConsoleLogging;



    /// <summary>
    /// Минимальный уровень сообщений, которые попадают в лог.
    /// </summary>
    private static LogLevel _minLevel = LogLevel.Info;



    /// <summary>
    /// Свойство определяет текущее состояние файлового логирования.
    /// </summary>
    public static bool IsFileLoggingEnabled
    {
        get => _isFileLoggingEnabled;
        set => _isFileLoggingEnabled = value;
    }

    /// <summary>
    /// Свойство определяет текущее состояние логирования во внутриигровую консоль.
    /// </summary>
    public static bool IsGameConsoleLoggingEnabled
    {
        get => _isGameConsoleLoggingEnabled;
        set => _isGameConsoleLoggingEnabled = value;
    }



    /// <summary>
    /// Свойство определяет минимальный уровень сообщений, которые записываются в лог.
    /// </summary>
    public static LogLevel MinLevel
    {
        get => _minLevel;
        set => _minLevel = value;
    }



    /// <summary>
    /// Метод инициализирует внутриигровую консоль в системе логирования.
    /// </summary>
    /// <param name="consoleText">Текстовый объект консоли.</param>
    public static void InitializeGameConsole(TMP_Text consoleText)
    {
        _gameConsoleLogSink = new GameConsoleLogSink(consoleText, _gameConsoleMaxLines);
    }



    /// <summary>
    /// Метод записывает сообщение лога в доступные каналы вывода.
    /// </summary>
    /// <param name="level">Уровень важности сообщения.</param>
    /// <param name="source">Источник сообщения.</param>
    /// <param name="message">Текст сообщения.</param>
    public static void Log(LogLevel level, string source, string message)
    {
        if (level < _minLevel)
        {
            return;
        }

        try
        {
            _unityConsoleLogSink?.Write(level, source, message);

            if (_isGameConsoleLoggingEnabled)
            {
                _gameConsoleLogSink?.Write(level, source, message);
            }

            if (_isFileLoggingEnabled)
            {
                CreateFileLogSink();
                _fileLogSink?.Write(level, source, message);
            }
        }
        catch (Exception exception)
        {
            Debug.LogError($"Logger internal error: {exception.Message}");
        }
    }



    /// <summary>
    /// Метод записывает информационное сообщение.
    /// </summary>
    public static void LogInfo(string source, string message) => Log(LogLevel.Info, source, message);

    /// <summary>
    /// Метод записывает предупреждение.
    /// </summary>
    public static void LogWarning(string source, string message) => Log(LogLevel.Warning, source, message);

    /// <summary>
    /// Метод записывает ошибку.
    /// </summary>
    public static void LogError(string source, string message) => Log(LogLevel.Error, source, message);



    /// <summary>
    /// Метод закрывает логгер и освобождает ресурсы.
    /// Рекомендуется вызывать из OnApplicationQuit.
    /// </summary>
    public static void Close()
    {
        if (_fileLogSink is IDisposable disposable)
        {
            disposable.Dispose();
        }

        _fileLogSink = null;
    }



    /// <summary>
    /// Метод создаёт файловый лог-синк, если он ещё не создан.
    /// </summary>
    private static void CreateFileLogSink()
    {
        if (_fileLogSink != null)
        {
            return;
        }

        try
        {
            _fileLogSink = new FileLogSink(_logFilePath, _maxLogFileSizeMB);
        }
        catch (Exception exception)
        {
            Debug.LogError($"Failed to create file log sink: {exception.Message}");
            _isFileLoggingEnabled = false;
        }
    }
}