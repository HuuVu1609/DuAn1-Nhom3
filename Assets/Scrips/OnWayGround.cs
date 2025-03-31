using System;
using UnityEngine;
using System.Collections;
public class OnWayGround : MonoBehaviour
{
    public static OnWayGround instance;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag ("Player"))
        {
        }
    }

    public IEnumerator GroundOneWay(CompositeCollider2D collider)
    {
        collider.isTrigger = false;
        yield return new WaitForSeconds(0.1f);
        collider.isTrigger = true;
        
    }
}
