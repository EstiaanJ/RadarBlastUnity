﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipController : MonoBehaviour
{

    [SerializeField]
    float rotationSpeed = 0.1f;
    [SerializeField]
    float maxForce = 0.6f;

    public GameObject dumbfire;
    public LaserBeam laserBeam;

    public float zoomFactor = 1;


    private bool hasFired = false;


    #region Monobehaviour
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        gameObject.AddComponent<LaserBeam>();
        laserBeam = gameObject.GetComponent<LaserBeam>();
        laserBeam.Initiate(gameObject.transform.position, gameObject.transform.position, 1);
    }

    // Update is called once per frame
    void Update()
    {
        float yAxis = Input.GetAxis("Vertical");
        float xAxis = Input.GetAxis("Horizontal");
        float translate = Input.GetAxis("Translate");

        float zeroSpeed = Input.GetAxis("ZeroSpeed");
        float zeroTurn = Input.GetAxis("ZeroTurn");

        float fireMain = Input.GetAxis("FireMain");

        float zoomLevel = Input.GetAxis("Zoom");

        ThrustForward(yAxis * maxForce);
        ThrustSideways(translate * maxForce);
        Rotate(transform, xAxis * rotationSpeed);
        
        ZeroSpeed(zeroSpeed);
        ZeroTurn(zeroTurn);

        FireMain(fireMain);

        Zoom(zoomLevel);
    }

    #endregion

    #region Other Controls
    void Zoom(float zoomLevel)
    {
        Camera cam = gameObject.GetComponentInChildren<Camera>();
        
        cam.orthographicSize += zoomLevel;
        
        if(cam.orthographicSize <= 0)
        {
            cam.orthographicSize = 0.1f;
        }
        zoomFactor = cam.orthographicSize/10;

    }

    #endregion

    #region Weaponfire API
    private void FireDumbfire(float fireMain)
    {

    }


    private void FireRailgun(float fireMain)
    {

    }

    private void FireLaser(float fireMain)
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (fireMain != 0 && !hasFired)
        {
            hasFired = true;
            laserBeam.isActive = true;
        }
        if (fireMain == 0)
        {
            hasFired = false;
            if(laserBeam != null)
            {
                laserBeam.isActive = false;
            }
        }
        if(laserBeam != null)
        {
            laserBeam.UpdatePos(gameObject.GetComponentInParent<Rigidbody2D>().position, mouseWorldPos);
            laserBeam.Fire(gameObject.GetComponent<Collider2D>());
        }
    }
    #endregion



    #region ManeurveringAPI
    private void FireMain(float fireMain)
    {
        FireLaser(fireMain);
        /* For firing dumbfire missiles BROKEN
        
        */
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


