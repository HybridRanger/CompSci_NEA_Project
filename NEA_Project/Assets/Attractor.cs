using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour {
    
    public Rigidbody2D rb;
    public int TrajectoryVertices;
    public float TrajectoryTimeSkip;

    private float G = 6.67e-11f;

	void FixedUpdate () {
        Attractor[] attractors = FindObjectsOfType<Attractor>();
        foreach (Attractor attractor in attractors)
        {
            if (attractor != this)
            {
                Attract(attractor);
            }
        }
	}

    void Attract (Attractor targetObj)
    {
        Rigidbody2D rbTarget = targetObj.rb;
        Vector2 direction = rbTarget.position - rb.position;
        float distance = direction.magnitude;
        float magnitude = (rbTarget.mass * rb.mass)/Mathf.Pow(distance, 2);
        Vector2 force = direction.normalized * magnitude;

        Debug.Log(force);
        rb.AddForce(force);
    }
}
