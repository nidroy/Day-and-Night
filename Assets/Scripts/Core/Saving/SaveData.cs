using System;

/// <summary>
/// Модель данных сохранения.
/// </summary>
[Serializable]
public class SaveData
{
    /// <summary>
    /// Версия структуры сохранения.
    /// </summary>
    public int Version = Globals.SaveDefaultVersion;

    /// <summary>
    /// Название сцены, в которой находится игрок или в которую нужно загрузить его при восстановлении.
    /// </summary>
    public string SceneName = Globals.SaveDefaultSceneName;
}