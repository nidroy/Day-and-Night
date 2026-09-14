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
    /// Имя папки, в которой хранятся настройки игры.
    /// </summary>
    private const string _gameSettingsFolderName = "Game settings";

    /// <summary>
    /// Имя файла настроек игры.
    /// </summary>
    private const string _gameSettingsFileName = "settings.json";



    /// <summary>
    /// Имя папки, в которой хранятся сохранения игры.
    /// </summary>
    private const string _gameSavesFolderName = "Game saves";

    /// <summary>
    /// Имя файла сохранения игры.
    /// </summary>
    private const string _gameSaveFileName = "save.json";

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
    /// Полный путь к файлу настроек игры.
    /// </summary>
    public static readonly string GameSettingsFilePath =
        Path.Combine(
            Application.persistentDataPath,
            _gameFolderName,
            _gameSettingsFolderName,
            _gameSettingsFileName);

    #endregion



    #region Настройки сохранения игры

    /// <summary>
    /// Полный путь к файлу сохранения игры.
    /// </summary>
    public static readonly string GameSaveFilePath =
        Path.Combine(
            Application.persistentDataPath,
            _gameFolderName,
            _gameSavesFolderName,
            _gameSaveFileName);

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

    #endregion



    #region Значения сохранения игры по умолчанию

    /// <summary>
    /// Значение версии сохранения игры по умолчанию.
    /// </summary>
    public const int GameSaveDefaultVersion = 1;



    /// <summary>
    /// Название сцены по умолчанию.
    /// </summary>
    public const string GameSaveDefaultSceneName = "Village Scene";

    #endregion
}
