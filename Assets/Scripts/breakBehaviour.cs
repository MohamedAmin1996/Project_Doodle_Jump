using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakBehaviour : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {       
        if (other.relativeVelocity.y <= 0f) 
        {
            Rigidbody2D rb = other.collider.GetComponent<Rigidbody2D>(); 
            if (rb != null) 
            {
                Destroy(this.gameObject);
            }
        }  
    }
}
