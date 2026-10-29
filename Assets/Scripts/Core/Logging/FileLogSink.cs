using System;
using System.IO;
using System.Text;
using UnityEngine;

/// <summary>
/// Реализация лог-синка для записи сообщений в файл.
/// </summary>
public class FileLogSink : ILogSink, IDisposable
{
    /// <summary>
    /// Полный путь к файлу логов.
    /// </summary>
    private readonly string _logFilePath;

    /// <summary>
    /// Максимальный размер файла в байтах.
    /// </summary>
    private readonly long _maxLogFileSizeBytes;

    /// <summary>
    /// Объект блокировки для синхронизации записи в файл.
    /// </summary>
    private readonly object _lockObject = new object();



    /// <summary>
    /// Поток записи в файл.
    /// </summary>
    private StreamWriter _writer;

    /// <summary>
    /// Текущий размер файла в байтах.
    /// </summary>
    private long _currentSizeBytes;

    /// <summary>
    /// Признак того, что синк освобождён.
    /// </summary>
    private bool _isDisposed;



    /// <summary>
    /// Конструктор создаёт новый файловый лог-синк.
    /// </summary>
    /// <param name="logFilePath">Путь к файлу логов.</param>
    /// <param name="maxLogFileSizeMB">Максимальный размер файла в мегабайтах.</param>
    public FileLogSink(string logFilePath, int maxLogFileSizeMB)
    {
        if (string.IsNullOrWhiteSpace(logFilePath))
        {
            throw new ArgumentException("Log file path cannot be empty.", nameof(logFilePath));
        }

        _logFilePath = logFilePath;
        _maxLogFileSizeBytes = Math.Max(1, maxLogFileSizeMB) * 1024L * 1024L;

        CreateLogFileDirectory();
        OpenWriter();
    }



    /// <summary>
    /// Метод записывает сообщение в файл.
    /// Перед записью проверяет размер файла и при необходимости выполняет ротацию.
    /// </summary>
    /// <param name="level">Уровень важности сообщения.</param>
    /// <param name="source">Источник сообщения.</param>
    /// <param name="message">Текст сообщения.</param>
    public void Write(LogLevel level, string source, string message)
    {
        string formattedMessage = LoggerMessageFormatter.Format(level, source, message);

        lock (_lockObject)
        {
            if (_isDisposed)
            {
                return;
            }

            try
            {
                EnsureWriter();
                CheckFileSize();

                _writer.WriteLine(formattedMessage);

                _currentSizeBytes += Encoding.UTF8.GetByteCount(formattedMessage) + Environment.NewLine.Length;
            }
            catch (Exception exception)
            {
                Debug.LogError($"FileLogSink write failed. Path: {_logFilePath}. Error: {exception.Message}");
            }
        }
    }



    /// <summary>
    /// Метод освобождает ресурсы синка и закрывает файл.
    /// </summary>
    public void Dispose()
    {
        lock (_lockObject)
        {
            if (_isDisposed)
            {
                return;
            }

            _isDisposed = true;
            CloseWriter();
        }
    }



    /// <summary>
    /// Метод создаёт директорию для файла логов, если она ещё не существует.
    /// </summary>
    private void CreateLogFileDirectory()
    {
        try
        {
            string directory = Path.GetDirectoryName(_logFilePath);

            if (!string.IsNullOrWhiteSpace(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }
        catch (Exception exception)
        {
            Debug.LogError($"Failed to create log directory. Path: {_logFilePath}. Error: {exception.Message}");
        }
    }

    /// <summary>
    /// Метод открывает поток записи в файл.
    /// </summary>
    private void OpenWriter()
    {
        try
        {
            FileInfo fileInfo = new FileInfo(_logFilePath);
            _currentSizeBytes = fileInfo.Exists ? fileInfo.Length : 0;

            _writer = new StreamWriter(_logFilePath, append: true, new UTF8Encoding(false))
            {
                AutoFlush = true
            };
        }
        catch (Exception exception)
        {
            Debug.LogError($"Failed to open log file. Path: {_logFilePath}. Error: {exception.Message}");
            _writer = null;
        }
    }

    /// <summary>
    /// Метод гарантирует, что поток записи открыт.
    /// </summary>
    private void EnsureWriter()
    {
        if (_writer == null)
        {
            OpenWriter();
        }
    }

    /// <summary>
    /// Метод закрывает поток записи.
    /// </summary>
    private void CloseWriter()
    {
        if (_writer == null)
        {
            return;
        }

        try
        {
            _writer.Flush();
            _writer.Dispose();
        }
        catch (Exception exception)
        {
            Debug.LogError($"Failed to close log file. Path: {_logFilePath}. Error: {exception.Message}");
        }
        finally
        {
            _writer = null;
        }
    }

    /// <summary>
    /// Метод проверяет размер файла логов.
    /// Если файл превысил лимит, он переименовывается в "*.old", а запись начинается в новый файл.
    /// </summary>
    private void CheckFileSize()
    {
        if (_currentSizeBytes < _maxLogFileSizeBytes)
        {
            return;
        }

        CloseWriter();

        try
        {
            string backupPath = _logFilePath + ".old";

            if (File.Exists(backupPath))
            {
                File.Delete(backupPath);
            }

            if (File.Exists(_logFilePath))
            {
                File.Move(_logFilePath, backupPath);
            }
        }
        catch (Exception exception)
        {
            Debug.LogError($"Failed to rotate log file. Path: {_logFilePath}. Error: {exception.Message}");
        }

        _currentSizeBytes = 0;
        OpenWriter();
    }
}