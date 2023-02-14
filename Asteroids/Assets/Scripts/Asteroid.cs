using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An asteroid
/// </summary>
public class Asteroid : MonoBehaviour
{
    [SerializeField]
    Sprite asteroidSprite0;
    [SerializeField]
    Sprite asteroidSprite1;
    [SerializeField]
    Sprite asteroidSprite2;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
	{
        // set random sprite for asteroid
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        int spriteNumber = Random.Range(0, 3);
        if (spriteNumber < 1)
        {
            spriteRenderer.sprite = asteroidSprite0;
        }
        else if (spriteNumber < 2)
        {
            spriteRenderer.sprite = asteroidSprite1;
        }
        else
        {
            spriteRenderer.sprite = asteroidSprite2;
        }
	}

    /// <summary>
    /// Starts the asteroid moving in the given direction
    /// </summary>
    /// <param name="direction">direction for the asteroid to move</param>
    /// <param name="position">position for the asteroid</param>
    public void Initialize(Direction direction, Vector3 position)
    {
        // set asteroid position
        transform.position = position;

        // set random angle based on direction
        float angle;
        float randomAngle = Random.value * 30f * Mathf.Deg2Rad;
        if (direction == Direction.Up)
        {
            angle = 75 * Mathf.Deg2Rad + randomAngle;
        }
        else if (direction == Direction.Left)
        {
            angle = 165 * Mathf.Deg2Rad + randomAngle;
        }
        else if (direction == Direction.Down)
        {
            angle = 255 * Mathf.Deg2Rad + randomAngle;
        }
        else
        {
            angle = -15 * Mathf.Deg2Rad + randomAngle;
        }

        StartMoving(angle);
    }

    /// <summary>
    /// Destroys the asteroid on collision with a bullet
    /// </summary>
    /// <param name="collision">collision info</param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            AudioManager.Play(AudioClipName.AsteroidHit);

            Vector3 localScale = transform.localScale;

            if (localScale.x < 0.5f)
            {
                Destroy(gameObject);
            }
            else
            {
                localScale.x /= 2;
                localScale.y /= 2;

                transform.localScale = localScale;

                CircleCollider2D cc2d = GetComponent<CircleCollider2D>();
                cc2d.radius /= 2;

                GameObject splitAsteroid1 = Instantiate(gameObject);
                GameObject splitAsteroid2 = Instantiate(gameObject);

                Destroy(gameObject);

                float randomAngle1 = Random.value * 360f * Mathf.Deg2Rad;
                splitAsteroid1.GetComponent<Asteroid>().StartMoving(randomAngle1);

                float randomAngle2 = Random.value * 360f * Mathf.Deg2Rad;
                splitAsteroid2.GetComponent<Asteroid>().StartMoving(randomAngle2);
            }
        }
    }

    public void StartMoving(float angle)
    {
        // apply impulse force to get asteroid moving
        const float MinImpulseForce = 1f;
        const float MaxImpulseForce = 3f;
        Vector2 moveDirection = new Vector2(
            Mathf.Cos(angle), Mathf.Sin(angle));
        float magnitude = Random.Range(MinImpulseForce, MaxImpulseForce);
        GetComponent<Rigidbody2D>().AddForce(
            moveDirection * magnitude,
            ForceMode2D.Impulse);
    }
}
