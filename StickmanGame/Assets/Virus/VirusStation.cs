using UnityEngine;
using UnityEngine.UI;

public class VirusStation : MonoBehaviour
{
    [Header("Station Settings")]
    public Virus virusToLoad; // Вирус который заправляется на этой станции
    public float interactionDistance = 3f;
    public KeyCode interactKey = KeyCode.E;

    [Header("UI")]
    public GameObject interactionUI; // UI подсказка "Нажмите E"
    public Text stationInfoText;

    private Transform player;
    private SyringeGun playerGun;
    private bool playerInRange = false;

    void Start()
    {
        if (interactionUI != null)
            interactionUI.SetActive(false);

        if (stationInfoText != null && virusToLoad != null)
            stationInfoText.text = $"Станция: {virusToLoad.virusName}";
    }

    void Update()
    {
        if (player == null)
            FindPlayer();

        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            playerInRange = distance <= interactionDistance;

            if (interactionUI != null)
                interactionUI.SetActive(playerInRange);

            if (playerInRange && Input.GetKeyDown(interactKey))
            {
                LoadVirus();
            }
        }
    }

    void FindPlayer()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
            playerGun = playerObj.GetComponentInChildren<SyringeGun>();
            Debug.Log($"Player found. SyringeGun: {playerGun != null}"); // Отладка
        }
        else
        {
            Debug.LogError("Player not found! Check the 'Player' tag.");
        }
    }


    void LoadVirus()
    {
        if (playerGun != null && virusToLoad != null)
        {
            playerGun.LoadVirus(virusToLoad);
            Debug.Log($"Заправлен вирус: {virusToLoad.virusName}");
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, interactionDistance);
    }
}