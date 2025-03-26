using UnityEngine;
using DG.Tweening; // Dùng DOTween để tạo hiệu ứng di chuyển

public class CarrotAnimation : MonoBehaviour
{
    public int scoreValue = 1; // Số điểm cộng khi ăn cà rốt
    public BoxCollider2D spawnArea; // Vùng sinh cà rốt
    public delegate void OnCarrotCollected(int score);
    public static event OnCarrotCollected CarrotCollected;

    void Start()
    {
        // Hiệu ứng di chuyển lên xuống liên tục
        transform.DOMoveY(transform.position.y + 1, 1.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Cộng điểm
            CarrotCollected?.Invoke(scoreValue);
            
            // Sinh lại củ cà rốt mới
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