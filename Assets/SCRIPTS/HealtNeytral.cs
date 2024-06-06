using UnityEngine;

public class HealtNeytral : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Дополнительные действия при смерти медведя, например, удаление его из игры
        Destroy(gameObject);
    }
}
