using System;
using System.IO;

/// <summary>
/// Репозиторий для работы с файлом сохранения.
/// Отвечает только за чтение, запись и удаление JSON-файла на диске.
/// </summary>
public class SaveFileRepository
{
    /// <summary>
    /// Полный путь к файлу сохранения.
    /// </summary>
    private readonly string _saveFilePath;



    /// <summary>
    /// Конструктор создаёт новый репозиторий для работы с файлом сохранения.
    /// </summary>
    /// <param name="saveFilePath">Путь к файлу сохранения.</param>
    public SaveFileRepository(string saveFilePath)
    {
        if (string.IsNullOrWhiteSpace(saveFilePath))
        {
            throw new ArgumentException("Save file path cannot be empty.", nameof(saveFilePath));
        }

        _saveFilePath = saveFilePath;

        CreateSaveFileDirectory();

        LogInfo($"SaveFileRepository initialized. Path: {_saveFilePath}");
    }



    /// <summary>
    /// Метод проверяет, существует ли файл сохранения.
    /// </summary>
    /// <returns>True, если файл существует, иначе false.</returns>
    public bool Exists()
    {
        try
        {
            bool exists = File.Exists(_saveFilePath);

            LogInfo($"Save file existence checked. Exists: {exists}");
            return exists;
        }
        catch (Exception exception)
        {
            LogError($"Failed to check save file existence. Path: {_saveFilePath}. Error: {exception.Message}");
            return false;
        }
    }



    /// <summary>
    /// Метод сохраняет JSON-строку в файл сохранения.
    /// </summary>
    /// <param name="json">JSON-строка с данными сохранения.</param>
    public void Save(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            LogWarning("Save skipped because JSON is empty.");
            return;
        }

        try
        {
            File.WriteAllText(_saveFilePath, json);

            LogInfo($"Save file written successfully. Path: {_saveFilePath}");
        }
        catch (Exception exception)
        {
            LogError($"Failed to save file. Path: {_saveFilePath}. Error: {exception.Message}");
        }
    }

    /// <summary>
    /// Метод загружает JSON-строку из файла сохранения.
    /// </summary>
    /// <returns>JSON-строка, либо null если загрузка не удалась.</returns>
    public string Load()
    {
        try
        {
            if (!File.Exists(_saveFilePath))
            {
                LogWarning($"Save file not found. Path: {_saveFilePath}");
                return null;
            }

            string json = File.ReadAllText(_saveFilePath);

            if (string.IsNullOrWhiteSpace(json))
            {
                LogWarning($"Save file is empty. Path: {_saveFilePath}");
                return null;
            }

            LogInfo($"Save file loaded successfully. Path: {_saveFilePath}");
            return json;
        }
        catch (Exception exception)
        {
            LogError($"Failed to load save file. Path: {_saveFilePath}. Error: {exception.Message}");
            return null;
        }
    }

    /// <summary>
    /// Метод удаляет файл сохранения, если он существует.
    /// </summary>
    public void Delete()
    {
        try
        {
            if (!File.Exists(_saveFilePath))
            {
                LogWarning($"Save file not found for delete. Path: {_saveFilePath}");
                return;
            }

            File.Delete(_saveFilePath);

            LogInfo($"Save file deleted successfully. Path: {_saveFilePath}");
        }
        catch (Exception exception)
        {
            LogError($"Failed to delete save file. Path: {_saveFilePath}. Error: {exception.Message}");
        }
    }



    /// <summary>
    /// Метод создаёт директорию для файла сохранения, если она ещё не существует.
    /// </summary>
    private void CreateSaveFileDirectory()
    {
        try
        {
            string directory = Path.GetDirectoryName(_saveFilePath);

            if (!string.IsNullOrWhiteSpace(directory))
            {
                Directory.CreateDirectory(directory);

                LogInfo($"Save directory ensured. Directory: {directory}");
            }
        }
        catch (Exception exception)
        {
            LogError($"Failed to create save directory. Path: {_saveFilePath}. Error: {exception.Message}");
        }
    }



    /// <summary>
    /// Метод записывает информационное сообщение в лог.
    /// </summary>
    private void LogInfo(string message)
    {
        Logger.Log(LogLevel.Info, nameof(SaveFileRepository), message);
    }

    /// <summary>
    /// Метод записывает предупреждение в лог.
    /// </summary>
    private void LogWarning(string message)
    {
        Logger.Log(LogLevel.Warning, nameof(SaveFileRepository), message);
    }

    /// <summary>
    /// Метод записывает ошибку в лог.
    /// </summary>
    private void LogError(string message)
    {
        Logger.Log(LogLevel.Error, nameof(SaveFileRepository), message);
    }
}