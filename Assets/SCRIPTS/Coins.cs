using UnityEngine;

public class Coins : MonoBehaviour
{
    private LevelChanger levelChanger;

    private void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StaticCoins.IncreaseCoins();
            Destroy(gameObject);

            // Проверяем, существует ли объект levelChanger
            
        }
    }
}