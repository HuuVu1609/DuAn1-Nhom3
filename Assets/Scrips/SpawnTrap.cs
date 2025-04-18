using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnTrap : MonoBehaviour
{
    public GameObject leftCannon;
    public GameObject rightCannon;
    public GameObject steelBall1;
    public GameObject steelBall2;
    public GameObject steelBall3;
    public GameObject giantBall;
    public GameObject leftBladeSaw;
    public GameObject rightBladeSaw;

    public GameObject warningLeftCannon;
    public GameObject warningRightCannon;
    public GameObject warningSteelBall;
    public GameObject warningGiantBall;
    public GameObject warningLeftBladeSaw;
    public GameObject warningRightBladeSaw;

    public Transform[] spawnPoints;
    public float delay = 1f;
    public float warningTime = 1f;
    
    public GameObject smoke1;
    public GameObject smoke2;
    public float effectTime = 0.5f;
    private void Start()
    {
        StartCoroutine(SpawnRandomly());
    }

    private IEnumerator SpawnRandomly()
    {
        List<Transform> availablePoints = new List<Transform>(spawnPoints);

        while (availablePoints.Count > 0)
        {
            yield return new WaitForSeconds(delay);

            int randomIndex = Random.Range(0, availablePoints.Count);
            int originalIndex = System.Array.IndexOf(spawnPoints, availablePoints[randomIndex]) + 1;

            GameObject objectToSpawn;
            GameObject warningToSpawn;
            GameObject smokeToSpawn;

            if (originalIndex >= 1 && originalIndex <= 3)
            {
                objectToSpawn = leftCannon;
                warningToSpawn = warningLeftCannon;
                smokeToSpawn =  smoke1;
            }
            else if (originalIndex >= 4 && originalIndex <= 6)
            {
                objectToSpawn = rightCannon;
                warningToSpawn = warningRightCannon;
                smokeToSpawn =  smoke1;
            }
            else if (originalIndex == 7)
            {
                objectToSpawn = steelBall1;
                warningToSpawn = warningSteelBall;
                smokeToSpawn =  smoke1;
            }
            else if (originalIndex == 8)
            {
                objectToSpawn = steelBall2;
                warningToSpawn = warningSteelBall;
                smokeToSpawn =  smoke1;
            }
            else if (originalIndex == 9)
            {
                objectToSpawn = steelBall3;
                warningToSpawn = warningSteelBall;
                smokeToSpawn =  smoke1;
            }
            else if (originalIndex >= 10 && originalIndex <= 12)
            {
                objectToSpawn = giantBall;
                warningToSpawn = warningGiantBall;
                smokeToSpawn =  smoke2;
            }
            else if (originalIndex == 13)
            {
                objectToSpawn = leftBladeSaw;
                warningToSpawn = warningLeftBladeSaw;
                smokeToSpawn =  smoke1;
            }
            else
            {
                objectToSpawn = rightBladeSaw;
                warningToSpawn = warningRightBladeSaw;
                smokeToSpawn =  smoke1;
            }

            GameObject warningObject = Instantiate(warningToSpawn, availablePoints[randomIndex].position, Quaternion.identity);
            Destroy(warningObject, warningTime);
            yield return new WaitForSeconds(warningTime);
            
            GameObject smokeObject = Instantiate(smokeToSpawn, availablePoints[randomIndex].position, Quaternion.identity);
            Destroy(smokeObject, effectTime);
            
            Instantiate(objectToSpawn, availablePoints[randomIndex].position, Quaternion.identity);
            availablePoints.RemoveAt(randomIndex);
        }
    }
}
