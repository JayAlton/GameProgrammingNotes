using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    // Start is called before the first frame update

    //Class variables for moving platform
    public float radius = 3f;
    public float speed = 2f;
    public Transform originPoint;
    public bool clockwise;
    private float angleOffset;
    public float heightOffset = 2.5f;
    private float angle = 0f;

    //variables for moving player on platform
    public Transform player;
    //public Transform sightStart, sightEnd;

    private bool isOnMovingPlatform;
    //private PlayerController playerController;

    void Start()
    {

        if(originPoint == null) {
            Debug.LogError("Origin point is not set for Moving Platform script");
        }

        Vector3 relativePosition = transform.position - originPoint.position;
        angleOffset = Mathf.Atan2(relativePosition.z, relativePosition.x) * Mathf.Rad2Deg;
       // playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        RaycastHit2D hit;
        hit = Physics2D.Linecast(sightStart.transform.position, sightEnd.transform.position);

        Debug.DrawLine(sightStart.transform.position, sightEnd.transform.position, Color.red);

        if(hit.transform != null)
        {
            if(hit.transform.tag == ("MovingPlatform")) {
                Transform movingPlatform = hit.collider.transform;
            }
        }
        */
        
        float angle = (Time.time * speed + angleOffset) % 360;

        float x;
        float z;

        float yOffset = Mathf.Sin(angle * Mathf.Deg2Rad) * heightOffset;
        if(clockwise) {
            x = originPoint.position.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
            z = originPoint.position.z + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        } else {
            x = originPoint.position.x - radius * Mathf.Cos(angle * Mathf.Deg2Rad);
            z = originPoint.position.z - radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        }
        transform.position = new Vector3(x, originPoint.position.y + yOffset, z);
    }
}
