using UnityEngine;

public class Patroul : MonoBehaviour
{
    public float speed;
    public float startWaitTime;
    public Transform[] moveSpots;
    private int currentSpotIndex;
    private Vector2 currentDirection;

    private bool isMoving = false;

    void Start()
    {
        currentSpotIndex = Random.Range(0, moveSpots.Length);
        SetNextDirection();
    }

    void Update()
    {
        if (isMoving)
        {
            transform.Translate(currentDirection * speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, moveSpots[currentSpotIndex].position) < 0.2f)
            {
                if (startWaitTime <= 0)
                {
                    SetNextDirection();
                }
                else
                {
                    startWaitTime -= Time.deltaTime;
                }
            }
        }
    }

    void SetNextDirection()
    {
        startWaitTime = 0;
        currentSpotIndex = (currentSpotIndex + 1) % moveSpots.Length;
        Vector2 targetPosition = moveSpots[currentSpotIndex].position;
        currentDirection = (targetPosition - (Vector2)transform.position).normalized;
        FlipSprite(currentDirection.x > 0);
        isMoving = true;
    }

    void FlipSprite(bool facingRight)
    {
        // Поворачиваем спрайт птички по оси X в зависимости от направления
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
