using UnityEngine;

public class Patroul : MonoBehaviour
{
    public float speed;
    public float patrolDistance;

    private Vector2 originalPosition;
    private bool movingRight = true;
    private float timer = 5f;
    void Start()
    {
        originalPosition = transform.position;
    }
    void Die()
    {
        Destroy(gameObject);
        // ƒополнительные действи€ при смерти мишки
    }

    void Update()
    {
        // ѕровер€ем наличие преп€тствий перед NPC перед каждым перемещением
        RaycastHit2D hit = Physics2D.Raycast(transform.position, movingRight ? Vector2.right : Vector2.left, 0.1f);
        if (hit.collider != null)
        {
            // ≈сли есть преп€тствие, продолжаем движение в том же направлении
            transform.Translate((movingRight ? Vector2.right : Vector2.left) * speed * Time.deltaTime);
        }
        else
        {
            // ≈сли нет преп€тствий, продолжаем обычное патрулирование
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

        // ѕровер€ем, двигаетс€ ли NPC, перед обновлением спрайта
        if ((Vector2)transform.position != (Vector2)originalPosition)
        {
            FlipSprite(movingRight);
        }

        // ќграничение движени€ NPC до patrolDistance
        /*if (Vector2.Distance(transform.position, originalPosition) >= patrolDistance)
        { 
            movingRight = !movingRight;
            timer = 5f;
        } */
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
