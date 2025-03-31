using UnityEngine;
using DG.Tweening;
using System.Collections;

public class GoldenCarrotAnimation : MonoBehaviour
{
    public int scoreValue = 5; // Điểm cao hơn cà rốt thường
    public BoxCollider2D spawnArea;
    public float lifeTime = 10f; // Thời gian tồn tại trước khi tự hủy
    public ParticleSystem collectEffect; // Hiệu ứng khi ăn
    
    public delegate void OnGoldenCarrotCollected(int score);
    public static event OnGoldenCarrotCollected GoldenCarrotCollected;

    private void Start()
    {

        transform.DOMoveY(transform.position.y + 1, 0.5f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutQuad);

        transform.DORotate(new Vector3(0, 0, 5), 2f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);

        StartCoroutine(DestroyAfterTime());
    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(lifeTime);

        transform.DOScale(Vector3.zero, 0.5f)
            .SetEase(Ease.InBack)
            .OnComplete(() => Destroy(gameObject));
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