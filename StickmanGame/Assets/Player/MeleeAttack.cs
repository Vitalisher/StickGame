using System.Collections;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    public float attackRange = 1.5f;
    public float comboCooldown = 2f;
    public int[] attackDamage = { 10, 10, 20 };
    public float[] attackDurations = { 0.6f, 0.6f, 1.0f };
    public LayerMask enemyLayer;

    [Header("References")]
    public Transform attackPoint;
    public RobloxStyleController playerController;

    private Animator animator;
    private int comboStep = 0;
    private float lastComboTime = -99f;
    private bool isAttacking = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null) animator = GetComponentInChildren<Animator>();

        if (playerController == null)
            playerController = GetComponent<RobloxStyleController>();
    }

    void Update()
    {
        if (comboStep > 0 && Time.time - lastComboTime > comboCooldown)
            comboStep = 0;

        if (!isAttacking && Input.GetMouseButtonDown(0))
            TryStartAttack();
    }

    void TryStartAttack()
    {
        if (!CanAttack()) return;

        lastComboTime = Time.time;
        StartCoroutine(PerformAttack(comboStep));
        comboStep = (comboStep + 1) % 3;
    }

    bool CanAttack()
    {
        if (playerController == null) return true;

        bool isGrounded = playerController.controller.isGrounded;
        bool isMoving = playerController.animator.GetBool("isRunning");

        if (!isGrounded || isMoving) return false;

        return true;
    }

    IEnumerator PerformAttack(int step)
    {
        isAttacking = true;

        animator.SetTrigger($"Attack{step + 1}");

        yield return new WaitForSeconds(0.3f);
        
        Collider[] hits = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);
        foreach (var hit in hits)
        {
            Health enemyHealth = hit.GetComponent<Health>();
            if (enemyHealth != null)
                enemyHealth.TakeDamage(attackDamage[step]);
        }
        
        yield return new WaitForSeconds(attackDurations[step]);

        isAttacking = false;
    }


    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}