using UnityEngine;

public class SyringeProjectile : MonoBehaviour
{
    public Virus virus;
    public float speed = 20f;
    public float lifetime = 5f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
            rb.velocity = transform.forward * speed;

        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        TestSubject subject = collision.gameObject.GetComponent<TestSubject>();

        if (subject != null && virus != null)
        {
            subject.Infect(virus);
            Debug.Log($"Ўприц попал в {subject.subjectName}!");
        }

        Destroy(gameObject);
    }
}