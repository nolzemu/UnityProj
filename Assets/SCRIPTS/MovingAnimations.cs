using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingAnimations : MonoBehaviour
{
    public Animator _animator;
    public Transform attackPoint; // Точка атаки персонажа
    public LayerMask enemyLayers; // Слой врагов

    public float attackRange = 0.5f; // Дальность атаки
    public int attackDamage = 40; // Урон от атаки 
    public bool IsMoving { private get; set; } // Флаг движения персонажа

    public bool IsFlying { private get; set; } // Флаг полета персонажа

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>(); // Получаем компонент Animator из дочерних объектов
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0)) // Обработка атаки при нажатии на левую кнопку мыши
        {
            Attack();
        }
        // Устанавливаем значения флагов движения и полета для управления анимациями
        _animator.SetBool("IsMoving", IsMoving);
        _animator.SetBool("IsFlying", IsFlying);

    }

    public void Jump() // Вызывается для запуска анимации прыжка
    {
        _animator.SetTrigger("Jump");
    }
    void Attack()
    {
        _animator.SetTrigger("Attack"); // Запускаем анимацию атаки

        // Находим всех врагов в зоне атаки и наносим им урон
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(20); // Передаем урон врагу
        }
    }

    void OnDrawGizmosSelected() // Отрисовываем графическое представление точки атаки
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange); // not fixed on X and Y player attack, player attack only right no left (может бить спиной)
        // // Рисуем окружность вокруг точки атаки с заданным радиусом
    }
}
 