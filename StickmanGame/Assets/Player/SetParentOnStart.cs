using UnityEngine;

public class SetParentOnStart : MonoBehaviour
{
    public Transform targetParent;

    private string targetParentName = "mixamorig:RightHandThumb2";

    void Start()
    {
        // Если targetParent не назначен, пытаемся найти по имени
        if (targetParent == null)
        {
            targetParent = GameObject.Find(targetParentName)?.transform;

            if (targetParent == null)
            {
                Debug.LogError($"Не удалось найти объект с именем: {targetParentName}");
                return;
            }
        }

        // Сохраняем глобальные позицию и поворот
        Vector3 currentWorldPosition = transform.position;
        Quaternion currentWorldRotation = transform.rotation;

        // Делаем объект дочерним
        transform.SetParent(targetParent);

        // Восстанавливаем глобальные позицию и поворот
        transform.position = currentWorldPosition;
        transform.rotation = currentWorldRotation;

        Debug.Log($"Объект {gameObject.name} стал дочерним для {targetParent.name}");
    }
}
