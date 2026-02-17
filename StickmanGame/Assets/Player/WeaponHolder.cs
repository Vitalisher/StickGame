using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [Header("Настройки руки")]
    public string handBoneName = "hand_r_end"; // Имя кости руки, куда будет вставлено оружие
    public Vector3 localPosition = Vector3.zero; // Локальная позиция оружия относительно руки
    public Vector3 localRotation = new Vector3(-90f, 100f, 70f); // Локальный поворот оружия относительно руки

    private Transform handBone; // Ссылка на кость руки
    private GameObject currentWeapon; // Текущее оружие в руке

    void Start()
    {
        // Находим кость руки у игрока
        handBone = FindDeepChild(transform, handBoneName);

        if (handBone == null)
        {
            Debug.LogError($"Кость руки с именем '{handBoneName}' не найдена!");
            return;
        }

        // Ищем оружие с тегом "Weapon"
        GameObject weapon = GameObject.FindGameObjectWithTag("Weapon");

        if (weapon != null)
        {
            EquipWeapon(weapon);
        }
        else
        {
            Debug.LogWarning("Оружие с тегом 'Weapon' не найдено!");
        }
    }

    // Метод для поиска глубокого дочернего объекта по имени
    private Transform FindDeepChild(Transform parent, string name)
    {
        foreach (Transform child in parent)
        {
            if (child.name == name)
            {
                return child;
            }

            Transform result = FindDeepChild(child, name);
            if (result != null)
            {
                return result;
            }
        }
        return null;
    }

    // Метод для установки оружия в руку
    public void EquipWeapon(GameObject weapon)
    {
        if (handBone == null)
        {
            Debug.LogError("Кость руки не назначена!");
            return;
        }

        // Удаляем предыдущее оружие, если оно есть
        if (currentWeapon != null)
        {
            Destroy(currentWeapon);
        }

        // Прикрепляем новое оружие к руке
        weapon.transform.SetParent(handBone);
        weapon.transform.localPosition = localPosition;
        weapon.transform.localRotation = Quaternion.Euler(localRotation);

        currentWeapon = weapon;
        Debug.Log("Оружие установлено в руку: " + weapon.name);
    }
}
