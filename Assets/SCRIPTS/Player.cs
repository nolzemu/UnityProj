using UnityEngine.SceneManagement;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private Animator animator;
    private bool isDead = false; // ���� ��� ������������ ��������� ��������� ������

    private void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    int i = 1;
    public GameObject BGText;
    public void Update()
    {
        if(i == 1 && StaticCoins.Coins == 1)
        {
            BGText.SetActive(true);
        }
    }
    // ����� ��� ��������� �����
    public void TakeDamage(int damage)
    {
        // ���������, �� ���� �� �����
        if (!isDead)
        {
            currentHealth -= damage;
            Debug.Log("Player took " + damage + " damage. Current health: " + currentHealth);

            if (currentHealth <= 0)
            {
                Die();
            }
            else
            {
                // �������� �������� ��������� �����
                animator.SetTrigger("Hurt");
            }
        }
    }

    // ����� ��� ������ ������
    private void Die()
    {
        Debug.Log("Player died!");
        // ���������� �������� ������
        animator.SetBool("Die", true);

        // ��������� ����������� ��������� ����� ������
        isDead = true;

        // ��������� �� ������ ����� ����� ������������ ����� (��������, 4 �������)
        Invoke("LoadNextScene", 4f);
    }

    // ����� ��� �������� ��������� �����
    private void LoadNextScene()
    {
        SceneManager.LoadScene(2);
    }

    // ����� ��� ��������, ����� �� �����
    public bool IsDead()
    {
        return isDead;
    }
}
