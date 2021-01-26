using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dumbfire : MonoBehaviour
{
    private Vector2 target;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Thrust(10);
    }

    void Thrust(float amount)
    {
        if(rb.velocity.magnitude < 500)
        {
            Vector2 force = transform.up * amount;
            rb.AddForce(force);
        }
        
    }
}
