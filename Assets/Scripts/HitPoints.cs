using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoints : MonoBehaviour
{
    [SerializeField]
    public float startingHP = 1000;
    // Start is called before the first frame update

    private float hitPoints;
    void Start()
    {
        hitPoints = startingHP;
    }

    // Update is called once per frame
    void Update()
    {
        if(hitPoints <= 0)
        {
            //Destroy(gameObject.)
            //print("DESTROY");
            Destroy(this);
            Destroy(this.gameObject);
        }
    }

    public void Damage(float hpd)
    {
        hitPoints -= hpd;
        print("Damage! " + hitPoints);
    }
}
