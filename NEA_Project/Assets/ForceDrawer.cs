using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceDrawer : MonoBehaviour {

    public LineRenderer velocityLine, GravityLine;
    private Vector3[] pointsV = new Vector3[2], pointsG = new Vector3[2];
    public float lengthV = 1, lengthG = 1;
    public GameObject parent;

    void Start () {
        velocityLine.material = new Material(Shader.Find("Sprites/Default"));
        GravityLine.material = new Material(Shader.Find("Sprites/Default"));

        pointsV[0] = new Vector3(0,0,10);
        pointsG[0] = new Vector3(0, 0, 10);
    }

	void Update () {
        Vector2 velocity = GetComponent<Rigidbody2D>().velocity;

        pointsV[1] = new Vector3(velocity.x, velocity.y, 10) * lengthV;
        velocityLine.SetPositions(pointsV);

        Vector2 direction = -transform.position + parent.transform.position;
        float distance = direction.magnitude;
        float force = ((GetComponent<Attractor>().rb.mass) * (parent.GetComponent<Attractor>().rb.mass)) / Mathf.Pow(distance, 2);

        pointsG[1] = direction.normalized * force * lengthG;
        GravityLine.SetPositions(pointsG);
	}

    
}
