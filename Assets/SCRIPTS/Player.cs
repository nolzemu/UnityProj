using UnityEngine.SceneManagement;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private Animator animator;
    private bool isDead = false; // Флаг для отслеживания состояния живучести игрока

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
    // Метод для получения урона
    public void TakeDamage(int damage)
    {
        // Проверяем, не умер ли игрок
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
                // Вызываем анимацию получения урона
                animator.SetTrigger("Hurt");
            }
        }
    }

    // Метод для смерти игрока
    private void Die()
    {
        Debug.Log("Player died!");
        // Активируем анимацию смерти
        animator.SetBool("Die", true);

        // Блокируем возможность двигаться после смерти
        isDead = true;

        // Переходим на другую сцену через определенное время (например, 4 секунды)
        Invoke("LoadNextScene", 4f);
    }

    // Метод для загрузки следующей сцены
    private void LoadNextScene()
    {
        SceneManager.LoadScene(2);
    }

    // Метод для проверки, мертв ли игрок
    public bool IsDead()
    {
        return isDead;
    }
}
