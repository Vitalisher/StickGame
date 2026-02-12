using UnityEngine;
using UnityEngine.UI;

public class SyringeGun : MonoBehaviour
{
    [Header("Gun Settings")]
    public Transform firePoint; // Точка выстрела
    public GameObject syringePrefab; // Префаб шприца
    public int maxAmmo = 1;
    private int currentAmmo = 0;

    [Header("Current Virus")]
    public Virus loadedVirus;

    [Header("Shooting")]
    public float fireRate = 1f;
    private float nextFireTime = 0f;

    [Header("UI")]
    public Text ammoText;
    public Text virusText;
    public Image virusColorIndicator;

    void Update()
    {
        UpdateUI();

        if (Input.GetMouseButtonDown(0) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    public void LoadVirus(Virus virus)
    {
        if (currentAmmo >= maxAmmo)
        {
            Debug.Log("Пушка уже заряжена!");
            return;
        }

        loadedVirus = virus;
        currentAmmo = maxAmmo;

        Debug.Log($"Заряжен вирус: {virus.virusName}");
    }

    void Shoot()
    {
        if (currentAmmo <= 0)
        {
            Debug.Log("Нет зарядов!");
            return;
        }

        if (loadedVirus == null)
        {
            Debug.Log("Вирус не загружен!");
            return;
        }

        if (firePoint == null)
        {
            Debug.LogError("Fire Point не назначен!");
            return;
        }

        // Создаем шприц
        GameObject syringe = Instantiate(syringePrefab, firePoint.position, firePoint.rotation);

        // Передаем вирус шприцу
        SyringeProjectile projectile = syringe.GetComponent<SyringeProjectile>();
        if (projectile != null)
        {
            projectile.virus = loadedVirus;
        }

        // Расходуем заряд
        currentAmmo--;

        // Если патроны закончились - сбрасываем вирус
        if (currentAmmo <= 0)
        {
            loadedVirus = null;
        }

        Debug.Log($"Выстрел! Осталось зарядов: {currentAmmo}");
    }

    void UpdateUI()
    {
        if (ammoText != null)
            ammoText.text = $"Заряды: {currentAmmo}/{maxAmmo}";

        if (virusText != null)
        {
            if (loadedVirus != null)
                virusText.text = $"Вирус: {loadedVirus.virusName}";
            else
                virusText.text = "Вирус: Нет";
        }

        if (virusColorIndicator != null)
        {
            if (loadedVirus != null)
                virusColorIndicator.color = loadedVirus.virusColor;
            else
                virusColorIndicator.color = Color.gray;
        }
    }
}