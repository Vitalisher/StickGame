using UnityEngine;

public abstract class Virus : ScriptableObject
{
    public string virusName;
    public Color virusColor = Color.green;
    public GameObject infectedPrefab; // Префаб зараженного существа
    public float infectionDuration = 10f; // Длительность заражения (0 = постоянно)

    public abstract void OnInfect(TestSubject subject);

    public virtual void OnCure(TestSubject subject)
    {
    }
}