using UnityEngine;

public class GiantBallCenter : MonoBehaviour
{
    private Animator animator;
    public GameObject[] pointPrefabs;
    public float[] distances;
    public float speed = 1.0f;

    private GameObject[] points;
    private Vector2 centerPoint;

    void Start()
    {
        animator = GetComponent<Animator>();
        centerPoint = transform.position;

        //animator.SetTrigger("Warning");

        //Invoke("EndWarning", 2.5f);
        //Invoke("SpawnPoints", 3f);
        SpawnPoints();
    }

    void EndWarning()
    {
        animator.SetTrigger("Idle");
    }

    void SpawnPoints()
    {   
        if (pointPrefabs.Length != distances.Length)
        {
            Debug.LogError("Số lượng prefab và khoảng cách không khớp!");
            return;
        }

        points = new GameObject[pointPrefabs.Length];

        for (int i = 0; i < pointPrefabs.Length; i++)
        {
            Vector2 spawnPosition = centerPoint + new Vector2(0,distances[i]);
            points[i] = Instantiate(pointPrefabs[i], spawnPosition, Quaternion.identity);
        }
    }

    void Update()
    {
        if (points == null) return;

        float angle = Time.time * speed;
        Vector2 direction;

        for (int i = 0; i < points.Length; i++)
        {   
            direction = new Vector2(Mathf.Cos(angle + Mathf.PI / 2), Mathf.Sin(angle + Mathf.PI / 2));
            Vector2 newPosition = centerPoint + direction * distances[i];
            points[i].transform.position = newPosition;
        }
    }
    //chinh sua lan 2
}
