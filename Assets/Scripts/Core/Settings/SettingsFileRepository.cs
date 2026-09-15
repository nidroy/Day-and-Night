using System;
using System.IO;

/// <summary>
/// Репозиторий для работы с файлом настроек.
/// Отвечает только за чтение, запись и удаление JSON-файла на диске.
/// </summary>
public class SettingsFileRepository
{
    /// <summary>
    /// Полный путь к файлу настроек.
    /// </summary>
    private readonly string _settingsFilePath;



    /// <summary>
    /// Конструктор создаёт новый репозиторий для работы с файлом настроек.
    /// </summary>
    /// <param name="settingsFilePath">Путь к файлу настроек.</param>
    public SettingsFileRepository(string settingsFilePath)
    {
        if (string.IsNullOrWhiteSpace(settingsFilePath))
        {
            throw new ArgumentException("Settings file path cannot be empty.", nameof(settingsFilePath));
        }

        _settingsFilePath = settingsFilePath;

        CreateSettingsFileDirectory();

        LogInfo($"SettingsFileRepository initialized. Path: {_settingsFilePath}");
    }



    /// <summary>
    /// Метод проверяет, существует ли файл настроек.
    /// </summary>
    /// <returns>True, если файл существует, иначе false.</returns>
    public bool Exists()
    {
        try
        {
            bool exists = File.Exists(_settingsFilePath);

            LogInfo($"Settings file existence checked. Exists: {exists}");
            return exists;
        }
        catch (Exception exception)
        {
            LogError($"Failed to check settings file existence. Path: {_settingsFilePath}. Error: {exception.Message}");
            return false;
        }
    }



    /// <summary>
    /// Метод сохраняет JSON в файл настроек.
    /// </summary>
    /// <param name="json">JSON-строка с настройками.</param>
    public void Save(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            LogWarning("Settings save skipped because JSON is empty.");
            return;
        }

        try
        {
            File.WriteAllText(_settingsFilePath, json);

            LogInfo($"Settings saved successfully. Path: {_settingsFilePath}");
        }
        catch (Exception exception)
        {
            LogError($"Failed to save settings file. Path: {_settingsFilePath}. Error: {exception.Message}");
        }
    }

    /// <summary>
    /// Метод загружает JSON из файла настроек.
    /// </summary>
    /// <returns>JSON-строка, либо null если загрузка не удалась.</returns>
    public string Load()
    {
        try
        {
            if (!File.Exists(_settingsFilePath))
            {
                LogWarning($"Settings file not found. Path: {_settingsFilePath}");
                return null;
            }

            string json = File.ReadAllText(_settingsFilePath);

            if (string.IsNullOrWhiteSpace(json))
            {
                LogWarning($"Settings file is empty. Path: {_settingsFilePath}");
                return null;
            }

            LogInfo($"Settings loaded successfully. Path: {_settingsFilePath}");
            return json;
        }
        catch (Exception exception)
        {
            LogError($"Failed to load settings file. Path: {_settingsFilePath}. Error: {exception.Message}");
            return null;
        }
    }

    /// <summary>
    /// Метод удаляет файл настроек, если он существует.
    /// </summary>
    public void Delete()
    {
        try
        {
            if (!File.Exists(_settingsFilePath))
            {
                LogWarning($"Settings file not found for delete. Path: {_settingsFilePath}");
                return;
            }

            File.Delete(_settingsFilePath);

            LogInfo($"Settings file deleted successfully. Path: {_settingsFilePath}");
        }
        catch (Exception exception)
        {
            LogError($"Failed to delete settings file. Path: {_settingsFilePath}. Error: {exception.Message}");
        }
    }



    /// <summary>
    /// Метод создаёт директорию для файла настроек, если она ещё не существует.
    /// </summary>
    private void CreateSettingsFileDirectory()
    {
        try
        {
            string directory = Path.GetDirectoryName(_settingsFilePath);

            if (!string.IsNullOrWhiteSpace(directory))
            {
                Directory.CreateDirectory(directory);

                LogInfo($"Settings directory ensured. Directory: {directory}");
            }
        }
        catch (Exception exception)
        {
            LogError($"Failed to create settings directory. Path: {_settingsFilePath}. Error: {exception.Message}");
        }
    }



    /// <summary>
    /// Метод записывает информационное сообщение в лог.
    /// </summary>
    private void LogInfo(string message)
    {
        Logger.Log(LogLevel.Info, nameof(SettingsFileRepository), message);
    }

    /// <summary>
    /// Метод записывает предупреждение в лог.
    /// </summary>
    private void LogWarning(string message)
    {
        Logger.Log(LogLevel.Warning, nameof(SettingsFileRepository), message);
    }

    /// <summary>
    /// Метод записывает ошибку в лог.
    /// </summary>
    private void LogError(string message)
    {
        Logger.Log(LogLevel.Error, nameof(SettingsFileRepository), message);
    }
}