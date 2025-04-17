using UnityEngine;

public class CarrotAnimation : MonoBehaviour
{
    public int scoreValue = 1; // Số điểm cộng khi ăn cà rốt
    public BoxCollider2D spawnArea; // Vùng sinh cà rốt
    public delegate void OnCarrotCollected(int score);
    public static event OnCarrotCollected CarrotCollected;

    private float startY;
    private bool movingUp = true;
    public float moveSpeed = 1f; // Tốc độ di chuyển lên xuống

    private void Start()
    {
        startY = transform.position.y;
    }

    private void Update()
    {
        // Hiệu ứng di chuyển lên xuống nhẹ nhàng
        float movement = movingUp ? moveSpeed * Time.deltaTime : -moveSpeed * Time.deltaTime;
        transform.position += new Vector3(0, movement, 0);

        // Đảo hướng khi đạt đến giới hạn
        if (transform.position.y >= startY + 0.5f) movingUp = false;
        if (transform.position.y <= startY) movingUp = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CarrotCollected?.Invoke(scoreValue);
            RespawnCarrot();
        }
    }

    void RespawnCarrot()
    {
        if (spawnArea != null)
        {
            float randomX = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            float randomY = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
            transform.position = new Vector3(randomX, randomY, transform.position.z);
        }
    }
}