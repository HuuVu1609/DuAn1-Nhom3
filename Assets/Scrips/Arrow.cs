using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    public GameObject arrowPrefab; 
    public Transform targetObject; 
    public float spawnInterval = 1f;
    public float minX = -10f;
    public float maxX = 10f;
    public float randomMinX = -4f;
    public float randomMaxX = 4f;
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnArrow();
        }
    }

    void SpawnArrow()
    {
        if (targetObject == null || arrowPrefab == null) return;
        
        float baseX = targetObject.position.x;
        
        float randomOffset = Random.Range(randomMinX, randomMaxX);
        float newX = Mathf.Clamp(baseX + randomOffset, minX, maxX);
        
        Vector3 spawnPosition = new Vector3(newX, 4f, 0f);
        GameObject createdArrow = Instantiate(arrowPrefab, spawnPosition, Quaternion.identity);

        Destroy(createdArrow, 4);
    }
}