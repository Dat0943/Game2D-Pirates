using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : MonoBehaviour
{
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void KnockBack(Vector2 force)
    {
        rb.isKinematic = false;
        rb.drag = 2f;
        rb.AddForce(force, ForceMode2D.Impulse);
        rb.AddTorque(20f); 
    }
}
