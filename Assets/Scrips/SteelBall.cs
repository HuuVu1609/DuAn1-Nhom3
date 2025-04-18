using UnityEngine;

public class SteelBall : MonoBehaviour
{
    public float moveSpeed = 5f;              
    public float moveAngle = 45f;            

    private Vector2 moveDirection;            
    void Start()
    {
        float angleRad = moveAngle * Mathf.Deg2Rad;
        moveDirection = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad)).normalized;
    }

    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            Vector2 normal = collision.contacts[0].normal;
            
            moveDirection = Vector2.Reflect(moveDirection, normal);
        }
    }
}