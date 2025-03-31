using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnTrap : MonoBehaviour
{
    public GameObject leftCannon;
    public GameObject rightCannon;
    public GameObject steelBall;
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

            if (originalIndex >= 1 && originalIndex <= 3)
            {
                objectToSpawn = leftCannon;
                warningToSpawn = warningLeftCannon;
            }
            else if (originalIndex >= 4 && originalIndex <= 6)
            {
                objectToSpawn = rightCannon;
                warningToSpawn = warningRightCannon;
            }
            else if (originalIndex >= 7 && originalIndex <= 9)
            {
                objectToSpawn = steelBall;
                warningToSpawn = warningSteelBall;
            }
            else if (originalIndex >= 10 && originalIndex <= 12)
            {
                objectToSpawn = giantBall;
                warningToSpawn = warningGiantBall;
            }
            else if (originalIndex == 13)
            {
                objectToSpawn = leftBladeSaw;
                warningToSpawn = warningLeftBladeSaw;
            }
            else
            {
                objectToSpawn = rightBladeSaw;
                warningToSpawn = warningRightBladeSaw;
            }
            //canh bao
            //Instantiate(warningToSpawn, availablePoints[randomIndex].position, Quaternion.identity);
            //yield return new WaitForSeconds(warningTime);

            Instantiate(objectToSpawn, availablePoints[randomIndex].position, Quaternion.identity);
            availablePoints.RemoveAt(randomIndex);
        }
    }
}
