using UnityEngine;

public class BladeSaw : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public Transform pointC;
    public Transform pointD;

    public float moveSpeed = 3f;

    private Vector3[] pathPoints;
    private int currentTargetIndex = 0;

    void Start()
    {
        pathPoints = new Vector3[]
        {
            pointC.position,
            pointB.position,
            pointC.position,
            pointD.position,
            pointA.position,
            pointD.position,
            pointC.position
        };
        transform.position = transform.position;
    }

    void Update()
    {
        if (pathPoints.Length == 0) return;

        Vector3 target = pathPoints[currentTargetIndex];
        
        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            currentTargetIndex++;
            if (currentTargetIndex >= pathPoints.Length)
            {
                currentTargetIndex = 0;
            }
        }
    }
}