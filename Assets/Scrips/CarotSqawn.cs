using System.Collections;
using UnityEngine;

public class CarotSqawn : MonoBehaviour
{
    public GameObject carrotPrefab;
    public GameObject goldCarrotPrefab;
    public Transform[] spawnPoints; 
    private GameObject currentGoldCarrot;

    void Start()
    {
        Invoke("SpawnCarrot", 1f);
        StartCoroutine(SpawnGoldCarrot());
    }

    void SpawnCarrot()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(carrotPrefab, spawnPoints[randomIndex].position, Quaternion.identity);
    }

    IEnumerator SpawnGoldCarrot()
    {
        while (true)
        {
            yield return new WaitForSeconds(8f); 
            int randomIndex = Random.Range(0, spawnPoints.Length);
            currentGoldCarrot = Instantiate(goldCarrotPrefab, spawnPoints[randomIndex].position, Quaternion.identity);
            
            yield return new WaitForSeconds(3f); 
            if (currentGoldCarrot != null)
                Destroy(currentGoldCarrot);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SpawnCarrot(); 
            Destroy(gameObject);
        }
    }
}