using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;

    public int maxHealth = 100;
    int currentHealth;

    public float moveSpeed = 3f;
    public float attackRange = 1.5f;
    public int attackDamage = 10;
    public float attackCooldown = 2f;

    private Transform target;
    private bool isAttacking = false;
    private float lastAttackTime;
    public float activationRadius;

    void Start()
    {
        currentHealth = maxHealth;
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (target != null)
        {
            float distanceToTarget = Vector2.Distance(transform.position, target.position);

            // ���������� ��������� ���� � �������
            if (distanceToTarget > attackRange && !isAttacking) // ��������� ������� !isAttacking
            {
                animator.SetBool("isRunning", true);
            }
            else
            {
                animator.SetBool("isRunning", false);
            }

            // ����������� � ������
            if (distanceToTarget > attackRange && !isAttacking)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            }

            // �������� ����������� �����
            if (distanceToTarget <= attackRange && !isAttacking && Time.time - lastAttackTime >= attackCooldown)
            {
                StartCoroutine(Attack());
            }
        }
    }



    IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.5f);
        if (Vector2.Distance(transform.position, target.position) <= attackRange)
        {
            target.GetComponent<Player>().TakeDamage(attackDamage);
        }
        lastAttackTime = Time.time;
        isAttacking = false;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Hurt");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");
        animator.SetBool("isDead", true);
        GetComponent<Collider2D>().enabled = false; // ��������� ���������
        this.enabled = false; // ��������� ������
        GetComponent<Rigidbody2D>().isKinematic = true; // ��������� ������� �����
    }
}
