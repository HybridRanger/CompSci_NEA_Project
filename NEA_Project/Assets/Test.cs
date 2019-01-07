using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

    public int numberOfPoints;
    public Rigidbody2D rb;
    public float trajectoryTimeStep;
    private LineRenderer lr;
    public Rigidbody2D target;
    private Vector2 position;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 curserPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (curserPos - rb.position).normalized;
            rb.AddForce(direction * 800);
            //Debug.Log("jump");
        }
    }
    void FixedUpdate () {
        DrawTrajectory();
	}

    void DrawTrajectory()
    {
        Vector3[] pointsArray = new Vector3[numberOfPoints];
        float velocityX = rb.velocity.x, velocityY = rb.velocity.y;
        float time;
        Vector2 position = rb.transform.position;

        for (int i = 0; i < numberOfPoints; i++)
        {
            //get acceleration
            Vector2 force = CalculateForce(position);
            float aX = force.x / rb.mass, aY = force.y / rb.mass;
            time = trajectoryTimeStep * i;
            Debug.Log(i +" " + force + " " + aX + ", " + aY + "  " + time);

            //calculate displacement
            float dispX = suat(velocityX, aX, time), dispY = suat(velocityY, aY, time);

            //set line vertex
            pointsArray[i] = new Vector3(dispX,dispY,0) + rb.transform.position;

            //set new values
            velocityX = vuat(velocityX, aX, time);
            velocityY = vuat(velocityY, aY, time);
            position = pointsArray[i];

            //Debug.Log(position);
        }

        lr.positionCount = numberOfPoints;
        lr.SetPositions(pointsArray);
    }

    float suat(float u, float a, float t)
    {
        float s;

        s = (float)((u * t) + (0.5 * a * Mathf.Pow(t, 2)));

        return s;
    }

    float vuat(float u, float a, float t)
    {
        float v;

        v = (float)(u + (a * t));

        return v;
    }

    Vector2 CalculateForce(Vector2 pos)
    {
        Vector2 direction = target.position - pos;
        float distance = direction.magnitude;
        float magnitude = (target.mass * rb.mass) / Mathf.Pow(distance, 2);
        Vector2 force = direction.normalized * magnitude;

        return force;
    }
}
