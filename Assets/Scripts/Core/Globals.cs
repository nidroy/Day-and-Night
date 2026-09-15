using System.IO;
using UnityEngine;

/// <summary>
/// Глобальные флаги и значения, используемые разными системами игры.
/// </summary>
public static class Globals
{
    #region Константы

    /// <summary>
    /// Имя папки игры.
    /// </summary>
    private const string _gameFolderName = "Day and Night";



    /// <summary>
    /// Имя папки, в которой хранятся логи.
    /// </summary>
    private const string _logsFolderName = "Logs";

    /// <summary>
    /// Имя файла логов.
    /// </summary>
    private const string _logFileName = "game.log";



    /// <summary>
    /// Имя папки, в которой хранятся настройки.
    /// </summary>
    private const string _settingsFolderName = "Settings";

    /// <summary>
    /// Имя файла настроек.
    /// </summary>
    private const string _settingsFileName = "settings.json";



    /// <summary>
    /// Имя папки, в которой хранятся сохранения.
    /// </summary>
    private const string _savesFolderName = "Saves";

    /// <summary>
    /// Имя файла сохранения.
    /// </summary>
    private const string _saveFileName = "save.json";

    #endregion



    #region Настройки логирования

    /// <summary>
    /// Максимальный размер файла логов в мегабайтах.
    /// </summary>
    public const int MaxLogFileSizeMB = 5;

    /// <summary>
    /// Максимальное количество строк во внутриигровой консоли.
    /// </summary>
    public const int GameConsoleMaxLines = 1000;

    /// <summary>
    /// Полный путь к файлу логов.
    /// </summary>
    public static readonly string LogFilePath =
        Path.Combine(
            Application.persistentDataPath,
            _gameFolderName,
            _logsFolderName,
            _logFileName);

    #endregion



    #region Настройки игры

    /// <summary>
    /// Полный путь к файлу настроек.
    /// </summary>
    public static readonly string SettingsFilePath =
        Path.Combine(
            Application.persistentDataPath,
            _gameFolderName,
            _settingsFolderName,
            _settingsFileName);

    #endregion



    #region Настройки сохранения

    /// <summary>
    /// Полный путь к файлу сохранения.
    /// </summary>
    public static readonly string SaveFilePath =
        Path.Combine(
            Application.persistentDataPath,
            _gameFolderName,
            _savesFolderName,
            _saveFileName);

    #endregion



    #region Значения настроек по умолчанию

    /// <summary>
    /// Значение версии настроек по умолчанию.
    /// </summary>
    public const int SettingsDefaultVersion = 1;



    /// <summary>
    /// Разрешение экрана по умолчанию.
    /// </summary>
    public const string SettingsDefaultScreenResolution = "1920x1080";

    /// <summary>
    /// Значение полноэкранного режима по умолчанию.
    /// </summary>
    public const bool SettingsDefaultIsFullScreen = true;



    /// <summary>
    /// Громкость музыки по умолчанию.
    /// </summary>
    public const float SettingsDefaultMusicVolume = 100f;

    /// <summary>
    /// Громкость звуков по умолчанию.
    /// </summary>
    public const float SettingsDefaultSoundVolume = 100f;



    /// <summary>
    /// Название локализации по умолчанию.
    /// </summary>
    public const string SettingsDefaultLocalization = "English";

    /// <summary>
    /// Код языка по умолчанию.
    /// </summary>
    public const string SettingsDefaultLanguageCode = "EN";



    /// <summary>
    /// Значение включённой записи логов в файл по умолчанию.
    /// </summary>
    public const bool SettingsDefaultIsFileLogging = true;

    /// <summary>
    /// Значение включённого логирования во внутриигровую консоль по умолчанию.
    /// </summary>
    public const bool SettingsDefaultIsGameConsoleLogging = true;

    #endregion



    #region Значения сохранения по умолчанию

    /// <summary>
    /// Значение версии сохранения по умолчанию.
    /// </summary>
    public const int SaveDefaultVersion = 1;



    /// <summary>
    /// Название сцены по умолчанию.
    /// </summary>
    public const string SaveDefaultSceneName = "Village Scene";

    #endregion
}
