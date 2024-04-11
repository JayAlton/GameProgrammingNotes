using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerPlayer : MonoBehaviour
{
    public float speed = 4.5f;
    public float jumpForce = 12.0f;

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D box;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        Vector2 movement = new Vector2(deltaX, body.velocity.y);
        body.velocity = movement;

        Vector3 max = box.bounds.max;
        Vector3 min = box.bounds.min;
        Vector2 corner1 = new Vector2(max.x, min.y - 0.1f);
        Vector2 corner2 = new Vector2(min.x, min.y - 0.2f);
        Collider2D hit = Physics2D.OverlapArea(corner1, corner2);

        bool grounded = false;
        if (hit != null) {
            grounded = true;
        }

        anim.SetFloat("speed", Mathf.Abs(deltaX));
        if(!Mathf.Approximately(deltaX, 0)) {
            transform.localScale = new Vector3(Mathf.Sign(deltaX), 1, 1);
        }

        if (Input.GetKeyDown(KeyCode.Space) && grounded) {
            body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        // Cast a ray downwards to detect the platform below the player
       // RaycastHit2D hitPlat = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);

        // If the player is on the moving platform, attach to it by becoming
        // a child object of the platform. If it's not on the moving platform,
        // set the parent back to null (no parent) so it's no longer a child
        // object. 
        MovingPlatform platform = null;
        if (hit != null) {
            platform = hit.GetComponent<MovingPlatform>();
        }
        if (platform != null) {
            transform.SetParent(platform.transform, true);
        } else {
            transform.SetParent(null, true);
        }

        // Counteract scaling of the moving platform
        Vector3 playerScale = Vector3.one;
        if (platform != null) {
            playerScale = platform.transform.localScale;
            if(!Mathf.Approximately(deltaX, 0)) {
                transform.localScale = 
                    new Vector3(Mathf.Sign(deltaX) / playerScale.x, 1 / playerScale.y, 1);
            }
        }
    }
}
