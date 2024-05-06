using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;

    public int maxHealth = 100;
    int currentHealth;

    // Новые переменные для атаки и движения
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
        target = GameObject.FindGameObjectWithTag("Player").transform; // Находим игрока
    }

    void Update()
    {
        if (target != null)
        {
            // Перемещаемся к игроку
            float distanceToTarget = Vector2.Distance(transform.position, target.position);
            if (distanceToTarget > attackRange)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            }

            // Проверяем возможность атаки
            if (distanceToTarget <= attackRange && !isAttacking && Time.time - lastAttackTime >= attackCooldown)
            {
                StartCoroutine(Attack());
            }
        }
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetTrigger("Attack"); // Запускаем анимацию атаки
        yield return new WaitForSeconds(0.5f); // Ждем, пока анимация завершится
        if (Vector2.Distance(transform.position, target.position) <= attackRange)
        {
            // Наносим урон игроку
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

        animator.SetBool("isDead", true); // Анимация смерти

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false; // Отключаем компонент Enemy
    }
}
