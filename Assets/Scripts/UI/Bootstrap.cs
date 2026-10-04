using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Класс выполняет первичную инициализацию в стартовой сцене.
/// </summary>
public class Bootstrap : MonoBehaviour
{
    /// <summary>
    /// Название сцены главного меню.
    /// </summary>
    [SerializeField] private string _mainMenuSceneName = "Main Menu Scene";

    /// <summary>
    /// Минимальное время отображения стартовой сцены в секундах.
    /// </summary>
    [SerializeField] private float _minimumStartupTime = 1f;



    /// <summary>
    /// Флаг, предотвращающий повторную инициализацию.
    /// </summary>
    private static bool _isInitialized;



    /// <summary>
    /// Метод Unity запускает стартовую корутину инициализации.
    /// </summary>
    private void Start()
    {
        StartCoroutine(StartInitialization());
    }



    /// <summary>
    /// Метод выполняет основную последовательность инициализации стартовой сцены.
    /// </summary>
    private IEnumerator StartInitialization()
    {
        if (_isInitialized)
        {
            LogInfo("Bootstrap already completed. Loading main menu.");
            LoadMainMenu();
            yield break;
        }

        _isInitialized = true;

        LogInfo("Bootstrap started.");

        float startTime = Time.realtimeSinceStartup;

        InitializeSettings();
        InitializeLocalization();
        InitializeSave();

        float elapsedTime = Time.realtimeSinceStartup - startTime;
        float remainingTime = _minimumStartupTime - elapsedTime;

        if (remainingTime > 0f)
        {
            yield return new WaitForSeconds(remainingTime);
        }

        LogInfo("Bootstrap finished.");

        LoadMainMenu();
    }



    /// <summary>
    /// Метод загружает и применяет настройки игры.
    /// </summary>
    private void InitializeSettings()
    {
        try
        {
            if (!SettingsService.Load())
            {
                LogWarning("Settings loading failed. Default settings will be used.");
                SettingsService.Default();
                SettingsService.Save();
            }

            SettingsService.Apply();
        }
        catch (Exception exception)
        {
            LogError($"Settings initialization failed: {exception.Message}");
            SettingsService.Default();
            SettingsService.Save();
            SettingsService.Apply();
        }
    }

    /// <summary>
    /// Метод загружает локализацию согласно текущему коду языка из настроек.
    /// </summary>
    private void InitializeLocalization()
    {
        try
        {
            string languageCode = SettingsService.LanguageCode;

            if (!LocalizationService.LoadLocalization(languageCode))
            {
                LogWarning($"Localization loading failed. Language: {languageCode}");
            }
        }
        catch (Exception exception)
        {
            LogError($"Localization initialization failed: {exception.Message}");
        }
    }

    /// <summary>
    /// Метод загружает данные сохранения.
    /// Если загрузка не удалась, используются значения по умолчанию.
    /// </summary>
    private void InitializeSave()
    {
        try
        {
            if (!SaveService.Load())
            {
                LogWarning("Save loading failed. Default save data will be used.");
                SaveService.Default();
            }
        }
        catch (Exception exception)
        {
            LogError($"Save initialization failed: {exception.Message}");
            SaveService.Default();
        }
    }



    /// <summary>
    /// Метод выполняет переход в главное меню.
    /// </summary>
    private void LoadMainMenu()
    {
        try
        {
            LogInfo($"Loading main menu scene: {_mainMenuSceneName}");
            SceneManager.LoadScene(_mainMenuSceneName);
        }
        catch (Exception exception)
        {
            LogError($"Failed to load main menu scene: {exception.Message}");
        }
    }



    /// <summary>
    /// Метод записывает информационное сообщение в лог.
    /// </summary>
    private void LogInfo(string message)
    {
        Logger.Log(LogLevel.Info, nameof(Bootstrap), message);
    }

    /// <summary>
    /// Метод записывает предупреждение в лог.
    /// </summary>
    private void LogWarning(string message)
    {
        Logger.Log(LogLevel.Warning, nameof(Bootstrap), message);
    }

    /// <summary>
    /// Метод записывает ошибку в лог.
    /// </summary>
    private void LogError(string message)
    {
        Logger.Log(LogLevel.Error, nameof(Bootstrap), message);
    }
}