using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    //Where the platform will go; editable in inspector
    public Vector3 finishPos = Vector3.zero;

    // How fast the platform will move 
    public float speed = 0.5f;

    // Starting point of the platform is where the platform is in the scene
    private Vector3 startPos;

    // Percentage of completion (from 0.0 to 1.0) from start to finish
    private float trackPercent = 0;

    // Current movement direction
    private int direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        // Get starting position
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Interpolate new location between start and finish postions
        trackPercent += direction * speed * Time.deltaTime;
        float x = (finishPos.x - startPos.x) * trackPercent + startPos.x;
        float y = (finishPos.y - startPos.y) * trackPercent + startPos.y;
        transform.position = new Vector3(x, y, startPos.z);

        // Reverse direction at start/finish positions
        if ((direction == 1 && trackPercent > 0.9f) || (direction == -1 && trackPercent < 0.1f)) {
            direction *= -1;
        }
    }
}
