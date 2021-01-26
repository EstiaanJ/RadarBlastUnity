using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarEmitter : MonoBehaviour
{
    [SerializeField]
    private float power = 1;
    [SerializeField]
    private float rotationRate = 0.0003f;

    public float zoomFactor = 1;

    public GameObject radarPing;
    


    private float currentAngle = 0;
    private RadarBeam radarBeam;
    private PlayerShipController psc;
    // Start is called before the first frame update
    void Start()
    {
        //radarBeam = this.gameObject.AddComponent(new RadarBeam(this.gameObject.transform.position, Math.fromPolar(this.power * 1000, this.currentAngle), power));//new 
        //radarBeam = new RadarBeam(this.gameObject.transform.position, Math.fromPolar(this.power * 1000, this.currentAngle), power);
        gameObject.AddComponent<RadarBeam>();
        radarBeam = gameObject.GetComponent<RadarBeam>();
        radarBeam.Initiate(this.gameObject.transform.position, Math.fromPolar(this.power * 1000, this.currentAngle), power);

        psc = gameObject.GetComponentInParent<PlayerShipController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        this.currentAngle = Math.stepAngle(rotationRate, currentAngle, 1f/60f);
        radarBeam.UpdatePos(this.gameObject.transform.position,Math.fromPolar(this.power*1000, this.currentAngle));
        Detect(this.gameObject.GetComponentInParent<Collider2D>());
        this.zoomFactor = psc.zoomFactor;
    }

    public void Detect(Collider2D collider)
    {
        List<RaycastHit2D> hitList = radarBeam.Cast();


        int itter = 0;
        foreach(RaycastHit2D hit in hitList){
            if(collider != hit.collider && itter < 1) 
            {
                itter++;
                Instantiate(radarPing, new Vector3(hit.point.x, hit.point.y, 0), Quaternion.identity);
                //Destroy(radarPing, 3);
                /*
                GameObject radHit = new GameObject("RadarPing");
                radHit.transform.position = new Vector2(hit.point.x, hit.point.y);
                radHit.AddComponent<LineRenderer>();
                LineRenderer lr = radHit.GetComponent<LineRenderer>();
                lr.material = new Material(Shader.Find("UI/Default"));
                lr.material.color = Color.green;
                DrawPolygon(6, 0.08f * power * zoomFactor, new Vector3(hit.point.x, hit.point.y, 0), 0.07f*power *zoomFactor, 0.07f*power*zoomFactor, lr);
                Destroy(radHit, 3);
                */
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







