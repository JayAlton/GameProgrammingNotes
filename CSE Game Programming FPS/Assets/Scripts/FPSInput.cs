using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSInput : MonoBehaviour
{
    public float speed = 3.0f;
    public float gravity = -9.8f;
    private bool allowedToMove = true;

    private CharacterController charController;

    // Start is called before the first frame update
    void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!allowedToMove) {
            return;
        }
        float deltaX = Input.GetAxis("Horizontal") * speed; //* Time.deltaTime;
        float deltaZ = Input.GetAxis("Vertical") * speed; // * Time.deltaTime;

        //transform.Translate(deltaX, 0, deltaY);

        //move the character 
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);
        movement.y = gravity;
        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        charController.Move(movement);
    }

    public void SetMovement() {
        allowedToMove = !allowedToMove;
    }
}
