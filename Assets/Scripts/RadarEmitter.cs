using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarEmitter : MonoBehaviour
{
    [SerializeField]
    private float power = 1;
    [SerializeField]
    private float rotationRate = 0.0003f;
    private float currentAngle = 0;
    private RadarBeam radarBeam;
    // Start is called before the first frame update
    void Start()
    {
       radarBeam = new RadarBeam(this.gameObject.transform.position,Math.fromPolar(this.power * 1000,this.currentAngle),power);
    }

    // Update is called once per frame
    void Update()
    {
        this.currentAngle = Math.stepAngle(rotationRate, currentAngle, 1f/60f);
        radarBeam.updatePos(this.gameObject.transform.position,Math.fromPolar(this.power*1000, this.currentAngle));
        detect();
    }

    public void detect()
    {
        List<RaycastHit2D> hitList = radarBeam.Cast();


        int itter = 0;
        foreach(RaycastHit2D hit in hitList){
            if(hit.fraction > 0.00001 && itter < 1)
            {
                itter++;
                GameObject radHit = new GameObject();
                radHit.transform.position = new Vector2(hit.point.x, hit.point.y);
                radHit.AddComponent<LineRenderer>();
                LineRenderer lr = radHit.GetComponent<LineRenderer>();
                lr.material = new Material(Shader.Find("UI/Default"));
                lr.material.color = Color.green;
                DrawPolygon(6, 0.08f * power, new Vector3(hit.point.x, hit.point.y, 0), 0.07f*power, 0.07f*power, lr);
                Destroy(radHit, 3);
            }

        }
    }

    void DrawPolygon(int vertexNumber, float radius, Vector3 centerPos, float startWidth, float endWidth, LineRenderer lineRenderer)
    {
        lineRenderer.startWidth = startWidth;
        lineRenderer.endWidth = endWidth;
        lineRenderer.loop = true;
        float angle = 2 * Mathf.PI / vertexNumber;
        lineRenderer.positionCount = vertexNumber;

        for (int i = 0; i < vertexNumber; i++)
        {
            Matrix4x4 rotationMatrix = new Matrix4x4(new Vector4(Mathf.Cos(angle * i), Mathf.Sin(angle * i), 0, 0),
                                                     new Vector4(-1 * Mathf.Sin(angle * i), Mathf.Cos(angle * i), 0, 0),
                                       new Vector4(0, 0, 1, 0),
                                       new Vector4(0, 0, 0, 1));
            Vector3 initialRelativePosition = new Vector3(0, radius, 0);
            lineRenderer.SetPosition(i, centerPos + rotationMatrix.MultiplyPoint(initialRelativePosition));

        }
    }

    void SetRotationRate(float rate)
    {
        rotationRate = Mathf.Abs(rate);
    }
}

public class RadarBeam
{
    private Vector2 origin; //Where the beam originates from
    private Vector2 destination; //The direction and range of the beam
    LineRenderer lr;

    public RadarBeam(Vector2 origin, Vector2 destination, float power)
    {
        this.origin = origin;
        this.destination = destination;
        GameObject beamLine = new GameObject();
        beamLine.transform.position = origin;
        beamLine.AddComponent<LineRenderer>();
        lr = beamLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("UI/Default"));
        lr.SetWidth(0.06f * power, 0.06f * power);
        lr.material.color = Color.green;
        lr.startColor = Color.green;
        lr.endColor = Color.green;
        lr.SetPosition(0, origin);
        lr.SetPosition(1, destination);
    }

    public void updatePos(Vector2 origin,Vector2 dest)
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
