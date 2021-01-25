using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{

    [SerializeField]
    float rotationSpeed = 0.1f;
    [SerializeField]
    float maxForce = 0.6f;
    //float maxXForce = 0.1f;
    #region Monobehaviour
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float yAxis = Input.GetAxis("Vertical");
        float xAxis = Input.GetAxis("Horizontal");
        float translate = Input.GetAxis("Translate");
        float zeroSpeed = Input.GetAxis("ZeroSpeed");
        float zeroTurn = Input.GetAxis("ZeroTurn");

        ThrustForward(yAxis * maxForce);
        ThrustSideways(translate * maxForce);
        Rotate(transform, xAxis * rotationSpeed);
        
        ZeroSpeed(zeroSpeed);
        ZeroTurn(zeroTurn);
        


    }
    #endregion

    #region ManeurveringAPI
    private void ClampVelocity()
    {

    }

    private void ZeroSpeed(float zeroSpeed)
    {
        if (zeroSpeed != 0)
        {
            rb.angularDrag = maxForce;
            rb.drag = maxForce;
        } else
        {
            rb.angularDrag = 0;
            rb.drag = 0;
        }

    }

    private void ZeroTurn(float zeroTurn)
    {
        if(zeroTurn != 0)
        {
            rb.angularDrag = 2 * maxForce;
        } 

    }

    private void ThrustForward(float amount)
    {
        Vector2 force = transform.up * amount;
        rb.AddForce(force);
    }
    private void ThrustSideways(float amount)
    {
        Vector2 force = transform.right * amount;
        rb.AddForce(force);
    }

    private void Rotate(Transform t, float amount)
    {
        rb.AddTorque(amount);
    }

    #endregion
}
