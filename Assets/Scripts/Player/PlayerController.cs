using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerStateController stateController;
    private PlayerCarryController carryController;

    private Rigidbody playerRb;

    [Header("Movement Settings")]
    [SerializeField] private float carrySpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float rotationSpeed;
    [Header("Child Settings")]
    [SerializeField] private Transform characterVisual;

    private float horizontalInput;
    private float verticalInput;
    private float movementSpeed;

    private Vector3 movementDirection;

    private void Awake()
    {
        stateController = GetComponent<PlayerStateController>();
        carryController = GetComponent<PlayerCarryController>();

        playerRb = GetComponent<Rigidbody>();
        movementSpeed = runSpeed;
    }

    void Update()
    {
        if (stateController.currentState == PlayerStateController.States.Chopping || stateController.currentState == PlayerStateController.States.Mining)
        {
            //When player chopping or mining, character can't move
            return;
        }

        AdjustSpeed();
        HandleMovement();
        HandleRotation();
    }

    private void AdjustSpeed()
    {
        if (IsMoving())
        {
            if (carryController.IsCarrying())
                movementSpeed = carrySpeed;
            else
                movementSpeed = runSpeed;
        }
    }
    private void HandleMovement()
    {
        horizontalInput = Input.GetAxis(Consts.PlayerInputs.HORIZONTAL_INPUT);
        verticalInput = Input.GetAxis(Consts.PlayerInputs.VERTICAL_INPUT);

        movementDirection = new Vector3(horizontalInput, 0f, verticalInput);

        if (IsMoving())
            playerRb.velocity = movementDirection * movementSpeed;
    }
    private void HandleRotation()
    {
        if (IsMoving())
        {
            var visualForward = Vector3.RotateTowards(characterVisual.transform.forward, movementDirection, rotationSpeed * Time.deltaTime, 0f);
            var visualRotation = Quaternion.LookRotation(visualForward);

            characterVisual.transform.rotation = visualRotation;
        }
    }
    public void SetPlayerController(bool isActive)
    {
        movementDirection = Vector3.zero;
        this.enabled = isActive;
    }
    public bool IsMoving()
    {
        return movementDirection != Vector3.zero;
    }

}
