using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    /////// REFERANCES ///////
    public CharacterController controller;
    public Animator animator;
    public Camera cam;
    /////// ---------- ///////

    /////// MOVEMENT ///////
    public float InputX;
    public float InputZ;
    public Vector3 desiredMoveDirection;
    public float desiredRotationSpeed;
    public float turnSpeedTreshold;
    public float maxSpeed;
    public float minSpeed;
    public float currentSpeed;
    public float speedIncrement;
    public float speedDecrement;
    public Vector3 moveVector;
    public bool canMove;
    public float moveMagnitude;
    /////// -------- ///////
    public string HORIZONTAL_AXIS = "Horizontal";
    public string VERTICAL_AXIS = "Vertical";
    ////// ROLL ///////
    public float rollSpeed;
    public float rollDuration;
    public float rollCooldown;
    public bool canRoll, stop;
    private void Start() {
        cam = Camera.main;
    }
    
    ////// -------- ///////    
    private void Update() 
    {        
        Roll();

        MovementAndRotation();
    }
    void FixedUpdate()
    {
        if(stop) return;
        controller.Move(moveVector * Time.deltaTime);   
    }
    void MovementAndRotation()
    {
        if(!canMove) return;

        InputX = Input.GetAxis(HORIZONTAL_AXIS);
        InputZ = Input.GetAxis(VERTICAL_AXIS);

        moveMagnitude = new Vector2(InputX, InputZ).sqrMagnitude;

        if(moveMagnitude > turnSpeedTreshold)
        {
            animator.SetFloat("speed", currentSpeed); // canMove animation

            Vector3 forward = cam.transform.forward;
            Vector3 right = cam.transform.right;

            forward.y = 0;
            right.y = 0;

            forward.Normalize();
            right.Normalize(); 

            desiredMoveDirection = forward * InputZ + right * InputX;

            currentSpeed = Mathf.Clamp( currentSpeed, minSpeed, maxSpeed );
            currentSpeed += speedIncrement * Time.deltaTime;
            
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed); 
        }
        else if(moveMagnitude < turnSpeedTreshold)
        {
            animator.SetFloat("speed", currentSpeed); // canMove animation
            
            currentSpeed -= speedDecrement * Time.deltaTime;
            currentSpeed = Mathf.Clamp( currentSpeed, 0, maxSpeed );
        }

        moveVector = desiredMoveDirection.normalized * currentSpeed;
    }
    void Roll()
    {
        if( Input.GetKeyDown(KeyCode.Space) && canRoll )
        {
            canMove = false;
            canRoll = false;
            animator.SetTrigger("roll");
            StartCoroutine( Roller() );
        }
    }
    IEnumerator Roller()
    {
        float t = 0;
        while (t < rollDuration)
        {
            t += Time.deltaTime;
            moveVector = desiredMoveDirection.normalized * rollSpeed; 
            yield return null;
        }
        canMove = true;
        StartCoroutine( RollCooldownCounter() );
    }
    IEnumerator RollCooldownCounter()
    {
        yield return new WaitForSeconds(rollCooldown);
        canRoll = true;
    }
}