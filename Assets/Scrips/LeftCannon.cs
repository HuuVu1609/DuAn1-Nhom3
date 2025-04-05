using System.Collections;
using UnityEngine;

public class LeftCannon : MonoBehaviour
{
    public GameObject leftCannonShell;
    public GameObject smoke;
    public float effectTime = 1;
    public float delayShooting = 1;
    public float delayFire = 1;
    public float delayReFire = 10;
    public float shellSpeed;
    
    private Animator animator;
    void Start()
    {   
        animator = GetComponent<Animator>();
        
        GameObject smokeObject = Instantiate(smoke, this.transform.position, Quaternion.identity);
        Destroy(smokeObject, effectTime);

        StartCoroutine(Shooting());
    }

    private IEnumerator Shooting()
    {
        // animator.SetTrigger("Pre Fire");
        animator.Play("Left Cannon Pre Fire Animation");
        yield return new WaitForSeconds(delayShooting);
        animator.ResetTrigger("Pre Fire");
        
    
        animator.SetTrigger("Fire");
        yield return new WaitForSeconds(delayFire);
    
        FireShell();

        yield return new WaitForSeconds(delayReFire);
        
        StartCoroutine(Shooting());
    }
    private void FireShell()
    {
        if (leftCannonShell != null)
        {
            GameObject shell = Instantiate(leftCannonShell, this.transform.position, Quaternion.identity);
            Rigidbody2D rb = shell.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.linearVelocity = transform.right * shellSpeed;
            }

            Destroy(shell, 5);
        }
    }
}