using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisableRay : MonoBehaviour
{
    protected Vector2 origin; //Where the beam originates from
    protected Vector2 destination; //The direction and range of the beam
    protected LineRenderer lr;

    public VisableRay()
    {
        
    }

    public void Initiate(Vector2 origin, Vector2 destination)
    {
        this.origin = origin;
        this.destination = destination;
        GameObject ray = new GameObject("VisableRay");
        ray.transform.position = origin;
        ray.AddComponent<LineRenderer>();
        lr = ray.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("UI/Default"));
        lr.SetPosition(0, origin);
        lr.SetPosition(1, destination);
    }

    public void UpdatePos(Vector2 origin, Vector2 dest)
    {
        this.destination = dest;
        this.origin = origin;
        lr.SetPosition(0, origin);
        lr.SetPosition(1, destination);
    }

    public List<RaycastHit2D> Cast()
    {
        ContactFilter2D cf = new ContactFilter2D();
        cf.NoFilter();
        List<RaycastHit2D> hitList = new List<RaycastHit2D>();
        Physics2D.Raycast(origin, destination, cf, hitList, destination.magnitude);
        return hitList;
    }
}

public class RadarBeam : VisableRay
{
    

    public RadarBeam() : base()
    {

    }

    public void initiate(Vector2 origin, Vector2 destination, float power)
    {
        base.Initiate(origin, destination);
        lr.startWidth = 0.06f * power;
        lr.endWidth = 0.06f * power;
        lr.material.color = Color.green;
        lr.startColor = Color.green;
        lr.endColor = Color.green;
        lr.SetPosition(0, origin);
        lr.SetPosition(1, destination);
    }
}


public class LaserBeam : VisableRay
{
    private float power;
    public bool isActive = false;
    public void Initiate(Vector2 origin, Vector2 destination, float power)
    {
        base.Initiate(origin, destination);
        this.power = power;
        lr.startWidth = 0.04f * this.power;
        lr.endWidth = 0.04f * this.power;
        lr.material.color = Color.red;
        lr.startColor = Color.red;
        lr.endColor = Color.red;
        lr.SetPosition(0, origin);
        lr.SetPosition(1, destination);
    }

    public void Fire(Collider2D collider)
    {
        if (isActive)
        {
            lr.enabled = true;
            List<RaycastHit2D> hitList = base.Cast();
            int itter = 0;
            foreach (RaycastHit2D hit in hitList)
            {
                if (collider != hit.collider && itter < 1)
                {
                    itter++;
                    HitPoints hp = hit.collider.gameObject.GetComponentInParent<HitPoints>();
                    if (hp != null)
                    {
                        hp.Damage(this.power * 3);
                    }


                    /*GameObject radHit = new GameObject("RadarPing");
                    radHit.transform.position = new Vector2(hit.point.x, hit.point.y);
                    radHit.AddComponent<LineRenderer>();
                    LineRenderer lr = radHit.GetComponent<LineRenderer>();
                    lr.material = new Material(Shader.Find("UI/Default"));
                    lr.material.color = Color.green;
                    DrawPolygon(6, 0.08f * power, new Vector3(hit.point.x, hit.point.y, 0), 0.07f * power, 0.07f * power, lr);
                    Destroy(radHit, 3);*/
                }
            }
        } else
        {
            lr.enabled = false;
        }
    }


    
}
