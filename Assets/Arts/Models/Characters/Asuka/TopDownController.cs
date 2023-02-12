using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TopDownController : MonoBehaviour
{

    private Vector2 playerInputVector;
    private bool isRunning;
    private Vector3 playerMovement;
    private float rotateSpeed = 1000;
    Transform playerTransform;

    Animator animator;

    float walkSpeed = 0.5f;

    float runSpeed = 1.0f;

    float targetSpeed = 0.0f;

    float currentSpeed = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        RotatePlayer();
        MovePlayer();
    }

    public void GetPlayerMoveInput(InputAction.CallbackContext ctx)
    {
        playerInputVector = ctx.ReadValue<Vector2>();
        //Debug.Log($"playerInput Vector2 {playerInputVector}");
    }

    public void GetPlayerRunningInput(InputAction.CallbackContext ctx)
    {
        isRunning = ctx.ReadValue<float>() > 0 ? true : false;
        Debug.Log($"isRunning {isRunning}");
    }


    void RotatePlayer()
    {
        

        if (playerInputVector.Equals(Vector2.zero))
            return;

        //Debug.Log($"playerInput Vector2 {playerInputVector}");

        playerMovement.x = playerInputVector.x;
        playerMovement.z = playerInputVector.y; 

        Quaternion targetQuaternaion = Quaternion.LookRotation(playerMovement, Vector3.up);
        playerTransform.rotation = Quaternion.RotateTowards(playerTransform.rotation, targetQuaternaion, rotateSpeed * Time.deltaTime);
    }

    void MovePlayer()
    {
        targetSpeed = isRunning ? runSpeed : walkSpeed;

        targetSpeed *= playerInputVector.magnitude;

        Debug.Log($"targetSpeed {targetSpeed}");

        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, 0.2f);

        animator.SetFloat("VertialValue", currentSpeed);
    }

}
 