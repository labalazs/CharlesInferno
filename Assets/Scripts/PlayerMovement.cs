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
    private InputAction moveAction;
    private InputAction runAction;
    private InputAction jumpAction;
    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        moveAction = InputSystem.actions.FindAction("Move");
        runAction = InputSystem.actions.FindAction("Sprint");
        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    private void Update()
    {
        if (characterController == null)
        {
            return;
        }
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        bool isRunning = runAction.ReadValue<float>() == 1;
        bool isJumping = jumpAction.ReadValue<float>() == 1;
        float currentXSpeed = (isRunning ? runSpeed : walkSpeed) * moveValue.x;
        float currentYSpeed = (isRunning ? runSpeed : walkSpeed) * moveValue.y;
        float movementDirectionY = moveDirection.y;
        moveDirection = (transform.right * currentXSpeed) + (transform.forward * currentYSpeed);
        float speed = Mathf.Abs(moveDirection.magnitude);
        if (isJumping && characterController.isGrounded)
        {
            animator.SetBool("IsJumping", true);
            moveDirection.y = jumpPower;
        }
        else
        {
            animator.SetBool("IsJumping", false);
            moveDirection.y = movementDirectionY;
        }
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
        animator.SetFloat("Speed", speed);
        animator.SetBool("IsRunning", isRunning);
        characterController.Move(moveDirection * Time.deltaTime);
    }
}
