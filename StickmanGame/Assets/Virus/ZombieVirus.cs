using UnityEngine;

[CreateAssetMenu(fileName = "ZombieVirus", menuName = "Viruses/Zombie Virus")]
public class ZombieVirus : Virus
{
    public override void OnInfect(TestSubject subject)
    {
        Debug.Log($"{subject.name} заражен вирусом зомби!");
        subject.TransformToInfected(infectedPrefab);
    }

    public override void OnCure(TestSubject subject)
    {
        Debug.Log($"{subject.name} вылечен от вируса зомби!");
        subject.ReturnToNormal();
    }
}