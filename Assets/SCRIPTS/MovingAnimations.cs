using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingAnimations : MonoBehaviour
{
    public Animator _animator;
    public Transform attackPoint; // ����� ����� ���������
    public LayerMask enemyLayers; // ���� ������

    public float attackRange = 0.5f; // ��������� �����
    public int attackDamage = 40; // ���� �� ����� 
    public bool IsMoving { private get; set; } // ���� �������� ���������

    public bool IsFlying { private get; set; } // ���� ������ ���������

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>(); // �������� ��������� Animator �� �������� ��������
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0)) // ��������� ����� ��� ������� �� ����� ������ ����
        {
            Attack();
        }
        // ������������� �������� ������ �������� � ������ ��� ���������� ����������
        _animator.SetBool("IsMoving", IsMoving);
        _animator.SetBool("IsFlying", IsFlying);

    }

    public void Jump() // ���������� ��� ������� �������� ������
    {
        _animator.SetTrigger("Jump");
    }
    void Attack()
    {
        _animator.SetTrigger("Attack"); // ��������� �������� �����

        // ������� ���� ������ � ���� ����� � ������� �� ����
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(20); // �������� ���� �����
        }
    }

    void OnDrawGizmosSelected() // ������������ ����������� ������������� ����� �����
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange); // not fixed on X and Y player attack, player attack only right no left (����� ���� ������)
        // // ������ ���������� ������ ����� ����� � �������� ��������
    }
}
 