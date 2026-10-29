using TMPro;
using UnityEngine;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

/// <summary>
/// Контроллер внутриигровой консоли.
/// Показывает и скрывает панель консоли, а также выводит сообщения лога.
/// </summary>
public class GameConsoleController : MonoBehaviour
{
    #region Инспектор

    /// <summary>
    /// Панель внутриигровой консоли.
    /// </summary>
    [Header("Console")]
    [SerializeField]
    private GameObject _consolePanel;

    /// <summary>
    /// Текст для отображения логов внутриигровой консоли.
    /// </summary>
    [SerializeField]
    private TMP_Text _consoleText;



#if ENABLE_INPUT_SYSTEM
    /// <summary>
    /// Клавиша переключения консоли (Input System). По умолчанию "ё" (Backquote).
    /// </summary>
    [SerializeField]
    private Key _toggleKey = Key.Backquote;
#elif ENABLE_LEGACY_INPUT_MANAGER
    /// <summary>
    /// Клавиша переключения консоли (старый Input Manager). По умолчанию "ё" (BackQuote).
    /// </summary>
    [SerializeField]
    private KeyCode _toggleKey = KeyCode.BackQuote;
#endif

    /// <summary>
    /// Состояние видимости консоли при старте сцены.
    /// </summary>
    [SerializeField]
    private bool _isVisibleOnStart = false;

    #endregion



    #region Поля

    /// <summary>
    /// Текущее состояние видимости консоли.
    /// </summary>
    private bool _isVisible;

    #endregion



    #region Unity Lifecycle

    /// <summary>
    /// Метод Unity вызывается при старте объекта.
    /// Регистрирует консоль в Logger и задаёт начальную видимость.
    /// </summary>
    private void Awake()
    {
        if (_consolePanel == null)
        {
            LogError("Console panel is not assigned. Console controller disabled.");
            enabled = false;
            return;
        }

        if (_consoleText != null)
        {
            Logger.InitializeGameConsole(_consoleText);
        }
        else
        {
            LogWarning("Console text is not assigned. Console logging disabled.");
        }

        SetVisible(_isVisibleOnStart);

        LogInfo("Game console initialized.");
    }

    /// <summary>
    /// Метод Unity вызывается каждый кадр.
    /// Переключает видимость консоли и выводит накопленные сообщения.
    /// </summary>
    private void Update()
    {
        if (IsTogglePressed())
        {
            ToggleVisible();
        }
    }

    #endregion



    #region Ввод

    /// <summary>
    /// Метод проверяет, нажата ли клавиша переключения консоли в этом кадре.
    /// </summary>
    /// <returns>True, если клавиша нажата в этом кадре; иначе false.</returns>
    private bool IsTogglePressed()
    {
#if ENABLE_INPUT_SYSTEM
        return Keyboard.current != null && Keyboard.current[_toggleKey].wasPressedThisFrame;
#elif ENABLE_LEGACY_INPUT_MANAGER
        return Input.GetKeyDown(_toggleKey);
#else
        return false;
#endif
    }

    #endregion



    #region Вспомогательные методы

    /// <summary>
    /// Метод переключает видимость консоли.
    /// </summary>
    public void ToggleVisible()
    {
        SetVisible(!_isVisible);
    }

    /// <summary>
    /// Метод задаёт видимость консоли.
    /// </summary>
    /// <param name="isVisible">True, чтобы показать консоль; иначе false.</param>
    private void SetVisible(bool isVisible)
    {
        _isVisible = isVisible;
        _consolePanel.SetActive(isVisible);

        LogInfo($"Game console visibility set to: {isVisible}.");
    }

    #endregion



    #region Логирование

    /// <summary>
    /// Метод записывает информационное сообщение в лог.
    /// </summary>
    private void LogInfo(string message)
    {
        Logger.Log(LogLevel.Info, nameof(GameConsoleController), message);
    }

    /// <summary>
    /// Метод записывает предупреждение в лог.
    /// </summary>
    private void LogWarning(string message)
    {
        Logger.Log(LogLevel.Warning, nameof(GameConsoleController), message);
    }

    /// <summary>
    /// Метод записывает ошибку в лог.
    /// </summary>
    private void LogError(string message)
    {
        Logger.Log(LogLevel.Error, nameof(GameConsoleController), message);
    }

    #endregion
}