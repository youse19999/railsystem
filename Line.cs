using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
     public Transform trans1;
     public Transform trans2;
     private int middlePoints = 0;
     private Vector3 controlPoint = new Vector3(0, 0, 2);
 
     public bool considerDistance = false;
    float distanceEffect = 0.05f;
    public GameObject lineobject;
    public Vector3 beforepos;
 	// Use this for initialization
 	void Start () {
        float tt = 0;
        float arrow_speed = 10 / Vector3.Distance(trans1.position, trans2.position);
        LineRenderer render = GetComponent<LineRenderer>();
        middlePoints = (int)Vector3.Distance(trans1.position,trans2.position) / 5;
         var de = 1.0f;
         if (considerDistance) {
             de = (trans1.position - trans2.position).magnitude * distanceEffect;   
         }
         var control = (trans1.position + trans2.position) / 2 + controlPoint * de;
 
         var totalPoints = middlePoints + 2;
         render.positionCount = totalPoints;
 
         render.SetPosition(0, trans1.position);
         for (int i = 1; i <= middlePoints; i++)
         {
            var t = (float)i / (float)(totalPoints - 1);
            var mpos = SampleCurve(trans1.position, trans2.position, control, t);
                
            GameObject obj = Instantiate(lineobject,mpos,Quaternion.identity);
            render.SetPosition(i, mpos);
            tt += arrow_speed * Time.deltaTime;
            Vector3 arrow_middle = Vector3.Lerp(trans1.position, trans2.position, 0f);
            Vector3 b = Vector3.Lerp(arrow_middle, trans2.position, tt);
            obj.transform.LookAt(b, Vector3.up);
            //obj.transform.rotation = Quaternion.Slerp(trans1.rotation,trans2.rotation,0.5f);
         }
         render.SetPosition(totalPoints-1, trans2.position);
 	}
 	//public static void CreateMesh(Mesh mesh,Vector3[] vertices,Vector3[])
 	// Update is called once per frame
 	void Update () {
        
 	}
    Vector3 SampleCurve(Vector3 start, Vector3 end, Vector3 control, float t)
     {
         // Interpolate along line S0: control - start;
         Vector3 Q0 = Vector3.Lerp(start, control, t);
         // Interpolate along line S1: S1 = end - control;
         Vector3 Q1 = Vector3.Lerp(control, end, t);
         // Interpolate along line S2: Q1 - Q0
         Vector3 Q2 = Vector3.Lerp(Q0, Q1, t);
         return Q2; // Q2 is a point on the curve at time t
     }
}
