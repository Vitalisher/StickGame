using System.Collections;
using UnityEngine;

public class TestSubject : MonoBehaviour
{
    [Header("Subject Settings")]
    public string subjectName = "Subject-001";

    private GameObject originalPrefab;
    private Virus currentVirus;
    private bool isInfected = false;
    private Coroutine cureCoroutine;

    [Header("Visual Feedback")]
    public Renderer subjectRenderer;
    private Color originalColor;

    void Start()
    {
        if (subjectRenderer == null)
            subjectRenderer = GetComponentInChildren<Renderer>();

        if (subjectRenderer != null)
            originalColor = subjectRenderer.material.color;
    }

    public void Infect(Virus virus)
    {
        if (isInfected)
        {
            Debug.Log($"{subjectName} уже заражен!");
            return;
        }

        currentVirus = virus;
        isInfected = true;

        // Применяем эффект вируса
        virus.OnInfect(this);

        // Визуальная индикация заражения
        if (subjectRenderer != null)
            subjectRenderer.material.color = virus.virusColor;

        // Если есть длительность - запускаем таймер лечения
        if (virus.infectionDuration > 0)
        {
            if (cureCoroutine != null)
                StopCoroutine(cureCoroutine);

            cureCoroutine = StartCoroutine(CureAfterTime(virus.infectionDuration));
        }
    }

    public void TransformToInfected(GameObject infectedPrefab)
    {
        if (infectedPrefab == null)
        {
            Debug.LogWarning("Префаб зараженного не назначен!");
            return;
        }

        // Сохраняем позицию и поворот
        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation;

        // Создаем зараженную версию
        GameObject infected = Instantiate(infectedPrefab, position, rotation);

        // Переносим компонент TestSubject на новый объект
        TestSubject newSubject = infected.GetComponent<TestSubject>();
        if (newSubject == null)
            newSubject = infected.AddComponent<TestSubject>();

        newSubject.subjectName = this.subjectName;
        newSubject.currentVirus = this.currentVirus;
        newSubject.isInfected = true;
        newSubject.originalPrefab = this.gameObject;

        // Если было лечение - переносим корутину
        if (cureCoroutine != null && currentVirus.infectionDuration > 0)
        {
            newSubject.cureCoroutine = newSubject.StartCoroutine(
                newSubject.CureAfterTime(currentVirus.infectionDuration)
            );
        }

        // Отключаем старый объект
        gameObject.SetActive(false);
    }

    public void ReturnToNormal()
    {
        if (originalPrefab != null)
        {
            // Возвращаем оригинальный префаб
            Vector3 position = transform.position;
            Quaternion rotation = transform.rotation;

            originalPrefab.transform.position = position;
            originalPrefab.transform.rotation = rotation;
            originalPrefab.SetActive(true);

            TestSubject originalSubject = originalPrefab.GetComponent<TestSubject>();
            if (originalSubject != null)
            {
                originalSubject.isInfected = false;
                originalSubject.currentVirus = null;

                if (originalSubject.subjectRenderer != null)
                    originalSubject.subjectRenderer.material.color = originalSubject.originalColor;
            }

            Destroy(gameObject);
        }
        else
        {
            // Просто сбрасываем статус заражения
            isInfected = false;
            currentVirus = null;

            if (subjectRenderer != null)
                subjectRenderer.material.color = originalColor;
        }
    }

    private IEnumerator CureAfterTime(float duration)
    {
        yield return new WaitForSeconds(duration);

        if (currentVirus != null)
            currentVirus.OnCure(this);

        cureCoroutine = null;
    }
}