using System;
using UnityEngine;

/// <summary>
/// Сервис управления сохранением.
/// Отвечает за загрузку, сохранение и удаление состояния игры.
/// </summary>
public static class SaveService
{
    /// <summary>
    /// Репозиторий для работы с файлом сохранения.
    /// </summary>
    private static readonly SaveFileRepository _saveFileRepository =
        new SaveFileRepository(Globals.SaveFilePath);

    /// <summary>
    /// Данные сохранения, хранящиеся в памяти.
    /// </summary>
    private static SaveData _saveData = new SaveData();



    /// <summary>
    /// Признак того, что файл сохранения существует.
    /// </summary>
    public static bool IsSaveFileExists => _saveFileRepository.Exists();



    /// <summary>
    /// Свойство определяет версию сохранения.
    /// </summary>
    public static int Version
    {
        get => _saveData.Version;
        set => _saveData.Version = Mathf.Max(1, value);
    }

    /// <summary>
    /// Свойство определяет название сцены.
    /// </summary>
    public static string SceneName
    {
        get => _saveData.SceneName;
        set => _saveData.SceneName = value?.Trim() ?? _saveData.SceneName;
    }



    /// <summary>
    /// Метод сохраняет текущее состояние игры в файл.
    /// </summary>
    public static void Save()
    {
        try
        {
            SaveDataValidator.Normalize(_saveData);

            string json = JsonUtility.ToJson(_saveData, true);

            if (string.IsNullOrWhiteSpace(json))
            {
                LogWarning("Save serialization returned empty JSON. Save skipped.");
                return;
            }

            _saveFileRepository.Save(json);

            LogInfo("Saved successfully.");
        }
        catch (Exception exception)
        {
            LogError($"Failed to save: {exception.Message}");
        }
    }

    /// <summary>
    /// Метод загружает сохранение из файла.
    /// </summary>
    /// <returns>True, если сохранение загружено успешно; иначе false.</returns>
    public static bool Load()
    {
        try
        {
            if (!_saveFileRepository.Exists())
            {
                LogWarning("Save file not found.");

                return false;
            }

            string json = _saveFileRepository.Load();

            if (string.IsNullOrWhiteSpace(json))
            {
                LogWarning("Save file is empty.");

                return false;
            }

            SaveData loaded = JsonUtility.FromJson<SaveData>(json);

            if (loaded == null)
            {
                LogWarning("Failed to deserialize save.");

                return false;
            }

            _saveData = loaded;

            SaveDataValidator.Normalize(_saveData);

            LogInfo("Save loaded successfully.");

            return true;
        }
        catch (Exception exception)
        {
            LogError($"Failed to load save: {exception.Message}");

            return false;
        }
    }

    /// <summary>
    /// Метод сбрасывает сохранение к значениям по умолчанию.
    /// </summary>
    public static void Default()
    {
        _saveData = new SaveData();
        LogInfo("Save reset to default values.");
    }

    /// <summary>
    /// Метод удаляет файл сохранения.
    /// </summary>
    public static void Delete()
    {
        try
        {
            if (!IsSaveFileExists)
            {
                LogWarning("Save file not found. Delete skipped.");
                return;
            }

            _saveFileRepository.Delete();
            LogInfo("Save deleted successfully.");
        }
        catch (Exception exception)
        {
            LogError($"Failed to delete save: {exception.Message}");
        }
    }



    /// <summary>
    /// Метод записывает информационное сообщение в лог.
    /// </summary>
    private static void LogInfo(string message)
    {
        Logger.Log(LogLevel.Info, nameof(SaveService), message);
    }

    /// <summary>
    /// Метод записывает предупреждение в лог.
    /// </summary>
    private static void LogWarning(string message)
    {
        Logger.Log(LogLevel.Warning, nameof(SaveService), message);
    }

    /// <summary>
    /// Метод записывает ошибку в лог.
    /// </summary>
    private static void LogError(string message)
    {
        Logger.Log(LogLevel.Error, nameof(SaveService), message);
    }
}