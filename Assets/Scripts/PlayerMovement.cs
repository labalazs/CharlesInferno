using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Camera playerCamera;
    public Animator animator;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 9.81f;
    public int allowedJumps = 1;
    public int remainingJumps = 1;
    public bool movementLocked = false;
    public float verticalVelocity
    {
        get
        {
            return moveDirection.y;
        }
        set
        {
            moveDirection.y = value;
        }
    }

    private InputAction moveAction;
    private InputAction runAction;
    private InputAction jumpAction;
    private CharacterController characterController;
    private CameraMovement cameraMovement;
    private Vector3 moveDirection = Vector3.zero;
    
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        moveAction = InputSystem.actions.FindAction("Move");
        runAction = InputSystem.actions.FindAction("Sprint");
        jumpAction = InputSystem.actions.FindAction("Jump");
        cameraMovement = playerCamera.GetComponentInParent<CameraMovement>();
        GameData.checkpointPosition = transform.position;
        GameData.checkpointYaw = cameraMovement.HorizontalRotation;
    }

    private void Update()
    {
        if (characterController == null)
        {
            return;
        }
        if (movementLocked)
        {
            return;
        }
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        bool isRunning = runAction.ReadValue<float>() == 1;
        bool isJumping = jumpAction.WasPressedThisFrame();
        float currentXSpeed = (isRunning ? runSpeed : walkSpeed) * moveValue.x;
        float currentYSpeed = (isRunning ? runSpeed : walkSpeed) * moveValue.y;
        float movementDirectionY = moveDirection.y;
        moveDirection = (transform.right * currentXSpeed) + (transform.forward * currentYSpeed);
        float speed = Mathf.Abs(moveDirection.magnitude);
        if (isJumping && (characterController.isGrounded || remainingJumps > 1))
        {
            animator.SetTrigger("TriggerJump");
            moveDirection.y += jumpPower;
            remainingJumps--;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
        else
        {
            remainingJumps = allowedJumps;
        }
        animator.SetFloat("Speed", speed);
        animator.SetBool("IsRunning", isRunning);
        characterController.Move(moveDirection * Time.deltaTime);
    }

    public void Respawn()
    {
        characterController.enabled = false;
        transform.position = GameData.checkpointPosition;
        characterController.enabled = true;
        moveDirection = Vector3.zero;
        cameraMovement.SetLookDirecton(GameData.checkpointYaw);
    }
}
