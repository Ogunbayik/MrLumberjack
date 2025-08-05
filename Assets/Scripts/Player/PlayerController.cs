using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerStateController stateController;

    private Rigidbody playerRb;

    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Transform characterVisual;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 movementDirection;

    private bool isMoving;
    private bool isCarrying;
    private void Awake()
    {
        stateController = GetComponent<PlayerStateController>();

        playerRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
        if (stateController.currentState == PlayerStateController.States.Chopping || stateController.currentState == PlayerStateController.States.Mining)
        {
            //When player chopping or mining, character can't move
            return;
        }
        
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        horizontalInput = Input.GetAxis(Consts.PlayerInputs.HORIZONTAL_INPUT);
        verticalInput = Input.GetAxis(Consts.PlayerInputs.VERTICAL_INPUT);

        movementDirection = new Vector3(horizontalInput, 0f, verticalInput);

        if (movementDirection != Vector3.zero)
            isMoving = true;
        else
            isMoving = false;

        if (isMoving)
            playerRb.velocity = movementDirection * movementSpeed;
    }

    private void HandleRotation()
    {
        if(isMoving)
        {
            var visualForward = Vector3.RotateTowards(characterVisual.transform.forward, movementDirection, rotationSpeed * Time.deltaTime, 0f);
            var visualRotation = Quaternion.LookRotation(visualForward);

            characterVisual.transform.rotation = visualRotation;
        }
    }

    public bool IsMoving()
    {
        return isMoving;
    }

    public bool IsCarrying()
    {
        return isCarrying;
    }
}
