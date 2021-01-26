using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dumbfire : MonoBehaviour
{
    //This whole class is very broken
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

    public void SetTarget(Vector3 target)
    {
        this.target = new Vector2(target.x,target.y);
    }

    void Thrust(float amount)
    {
        if (rb.velocity.magnitude < 500)
        {
            Vector2 force =  Math.fromPolar(amount,Math.bearingTo(rb.transform.position,target));
            rb.AddForce(force);
        }
        
    }
}
