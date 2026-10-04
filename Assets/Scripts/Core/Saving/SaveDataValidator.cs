using UnityEngine;

/// <summary>
/// Валидатор данных сохранения.
/// Нормализатор приводит значения к безопасным диапазонам.
/// </summary>
public static class SaveDataValidator
{
    /// <summary>
    /// Метод приводит данные сохранения к безопасному и корректному состоянию.
    /// </summary>
    /// <param name="data">Объект данных сохранения, который нужно проверить и исправить.</param>
    public static void Normalize(SaveData data)
    {
        // Если объект не передан, ничего не делаем.
        if (data == null)
        {
            return;
        }

        // Версия не может быть меньше 1.
        data.Version = Mathf.Max(1, data.Version);

        // Название сцены должно быть задано.
        if (string.IsNullOrWhiteSpace(data.SceneName))
        {
            data.SceneName = Globals.SaveDefaultSceneName;
        }
        else
        {
            data.SceneName = data.SceneName.Trim();
        }
    }
}