using UnityEngine;

public class Patroul : MonoBehaviour
{
    public float speed;
    public float patrolDistance;

    private Vector2 originalPosition;
    private bool movingRight = true;
    private float timer = 5f;
    public float health = 1f;
    void Start()
    {
        originalPosition = transform.position;
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        // Дополнительные действия при получении урона, например, анимация или звук
        if (health <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject);
        // Дополнительные действия при смерти мишки
    }

    void Update()
    {
        // Проверяем наличие препятствий перед NPC перед каждым перемещением
        RaycastHit2D hit = Physics2D.Raycast(transform.position, movingRight ? Vector2.right : Vector2.left, 0.1f);
        if (hit.collider != null)
        {
            // Если есть препятствие, продолжаем движение в том же направлении
            transform.Translate((movingRight ? Vector2.right : Vector2.left) * speed * Time.deltaTime);
        }
        else
        {
            // Если нет препятствий, продолжаем обычное патрулирование
            if (movingRight)
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
            }
        }

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            movingRight = !movingRight;
            timer = 5f;
        }

        // Проверяем, двигается ли NPC, перед обновлением спрайта
        if ((Vector2)transform.position != (Vector2)originalPosition)
        {
            FlipSprite(movingRight);
        }

        // Ограничение движения NPC до patrolDistance
        if (Vector2.Distance(transform.position, originalPosition) >= patrolDistance)
        {
            movingRight = !movingRight;
            timer = 5f;
        }
    }

    void FlipSprite(bool facingRight)
    {
        if (facingRight)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
}
