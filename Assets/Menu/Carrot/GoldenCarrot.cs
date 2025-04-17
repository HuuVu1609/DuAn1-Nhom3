using UnityEngine;
using System.Collections;

public class GoldenCarrotAnimation : MonoBehaviour
{
    public int scoreValue = 5; // Điểm cao hơn cà rốt thường
    public float moveSpeed = 1f; // Tốc độ di chuyển lên/xuống
    public float rotationSpeed = 30f; // Tốc độ xoay
    public float lifeTime = 10f; // Thời gian tồn tại trước khi tự hủy
    public ParticleSystem collectEffect; // Hiệu ứng khi ăn
    
    public delegate void OnGoldenCarrotCollected(int score);
    public static event OnGoldenCarrotCollected GoldenCarrotCollected;

    private float startY;
    private bool movingUp = true;

    private void Start()
    {
        startY = transform.position.y;
        StartCoroutine(DestroyAfterTime());
    }

    private void Update()
    {
        // Hiệu ứng lắc nhẹ (di chuyển lên/xuống)
        float movement = movingUp ? moveSpeed * Time.deltaTime : -moveSpeed * Time.deltaTime;
        transform.position += new Vector3(0, movement, 0);

        // Đổi hướng di chuyển khi đạt đến một giới hạn
        if (transform.position.y >= startY + 1f) movingUp = false;
        if (transform.position.y <= startY) movingUp = true;

        // Hiệu ứng xoay đơn giản
        transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GoldenCarrotCollected?.Invoke(scoreValue);

            if (collectEffect != null)
            {
                ParticleSystem effect = Instantiate(collectEffect, transform.position, Quaternion.identity);
                effect.Play();
                Destroy(effect.gameObject, effect.main.duration);
            }

            Destroy(gameObject);
        }
    }
}