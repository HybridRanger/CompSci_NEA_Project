using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(LineRenderer))]

public class EllipseDrawer : MonoBehaviour {

    public int numberOfPoints;
    public float semiMinorAxis, semiMajorAxis;
    public GameObject parent, centre;
    public Rigidbody2D rb;
    private LineRenderer lr;
    private List<Vector3> points = new List<Vector3>();

    private float previousAngle;

    // Use this for initialization
    void Start () {
        lr = centre.AddComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.widthMultiplier = 0.2f;
        lr.useWorldSpace = false;
        lr.loop = true;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        DrawLine();
	}

    void DrawLine()
    {
        points.Add(-(parent.transform.position) + transform.position);

        if (CheckAngle())
        {
            lr.positionCount = points.Count;
            lr.SetPositions(points.ToArray());
            points.Clear();
        }
    }

    bool CheckAngle()
    {
        Vector2 p1 = parent.transform.position;
        Vector2 p2 = transform.position;

        float angle = (Mathf.Atan2(p2.y - p1.y, p2.x - p1.x) * Mathf.Rad2Deg) + 180;
        float angleDifference = previousAngle - angle;

        previousAngle = angle;

        if (angleDifference > 100 || angleDifference < -100)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }
}
