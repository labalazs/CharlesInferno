using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    public GameObject cameraPivot;
    public Camera camera;
    public float xSensitivity = 2f;
    public float ySensitivity = 2f;
    public float distance = 5f;
    public float minVerticalAngle = -30f;
    public float maxVerticalAngle = 60f;
    public float HorizontalRotation => horizontalRotation;

    private float horizontalRotation;
    private float verticalRotation;
    private InputAction lookAction;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        lookAction = InputSystem.actions.FindAction("Look");
    }

    private void LateUpdate()
    {
        if (player == null || lookAction == null)
        {
            return;
        }
        transform.position = player.transform.position;
        Vector2 mouseMovement = lookAction.ReadValue<Vector2>();
        horizontalRotation += mouseMovement.x * xSensitivity;
        verticalRotation -= mouseMovement.y * ySensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, minVerticalAngle, maxVerticalAngle);
        transform.rotation = Quaternion.Euler(0, horizontalRotation, 0);
        player.transform.rotation = Quaternion.Euler(0, horizontalRotation, 0);
        cameraPivot.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        camera.transform.localPosition = new Vector3(0, 0, -distance);
    }

    public void SetLookDirecton(float yaw, float pitch = 15f)
    {
        horizontalRotation = yaw;
        verticalRotation = Mathf.Clamp(pitch, minVerticalAngle, maxVerticalAngle);
    }
}
