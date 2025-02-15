using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLoop : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float waterWidth;
    
    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
        
        if(transform.position.x <= -waterWidth)
        {
            transform.position += new Vector3(waterWidth * 2, 0, 0);
        }
    }
}
