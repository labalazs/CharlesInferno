using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GrapplingHookAbility : Ability
{
    public override AbilityType Type => AbilityType.GrapplingHook;

    [Header("References")]
    public PlayerMovement movement;
    public CharacterController characterController;
    public Image crosshair;
    public LineRenderer lineRenderer;
    public Transform playerOrigin;

    [Header("Settings")]
    public LayerMask grappleMask;
    public float maxDistance = 25f;
    public float pullSpeed = 20f;
    public float stopDistance = 2f;
    public float maxGrappleTime = 2f;
    public float cooldown = 0.5f;
    public float releaseUpBoost = 7f;
    public float crosshairYOffset = 200f;

    private InputAction grappleAction;
    private Camera camera;
    private bool grappling;
    private Vector3 hookPoint;
    private float nextUseTime;
    private float grappleTimer;
    private Transform lineOrigin;

    protected override void Awake()
    {
        base.Awake();
        camera = Camera.main;
        grappleAction = InputSystem.actions.FindAction("Grapple");
        crosshair.rectTransform.anchoredPosition = new Vector2(0, crosshairYOffset);
        crosshair.gameObject.SetActive(false);
        lineOrigin = playerOrigin;
        lineOrigin.position += new Vector3(0, 1, 0);
        lineRenderer.enabled = false;
    }

    public override void OnEquip()
    {
        base.OnEquip();
        crosshair.gameObject.SetActive(true);
    }

    public override void OnUnequip()
    {
        base.OnUnequip();
        Release(false);
        crosshair.gameObject.SetActive(false);
    }

    public void Cancel() => Release(false);

    private void Update()
    {
        if (Time.timeScale == 0f)
        {
            return;
        }
        if (!grappling)
        {
            bool hasTarget = TryGetTarget(out Vector3 point);
            crosshair.color = hasTarget ? Color.green : Color.white;
            if (hasTarget && Time.time >= nextUseTime && grappleAction.WasPressedThisFrame())
            {
                StartGrapple(point);
            }
        }
        else
        {
            PullStep();
        }
    }

    private bool TryGetTarget(out Vector3 point)
    {
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(null, crosshair.rectTransform.position);
        Ray ray = camera.ScreenPointToRay(screenPoint);
        float offset = Vector3.Distance(camera.transform.position, characterController.bounds.center);
        Vector3 origin = ray.origin + ray.direction * offset;
        if (Physics.Raycast(origin, ray.direction, out RaycastHit hit, maxDistance, grappleMask, QueryTriggerInteraction.Ignore))
        {
            point = hit.point;
            return true;
        }
        point = default;
        return false;
    }

    private void StartGrapple(Vector3 point)
    {
        grappling = true;
        hookPoint = point;
        grappleTimer = 0f;
        movement.movementLocked = true;
        movement.verticalVelocity = 0f;
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = true;
        crosshair.color = Color.white;
    }

    private void PullStep()
    {
        grappleTimer += Time.deltaTime;
        Vector3 toHook = hookPoint - characterController.bounds.center;
        if (toHook.magnitude <= stopDistance)
        {
            Release(true);
            return;
        }
        if (grappleTimer > maxGrappleTime || grappleAction.WasReleasedThisFrame())
        {
            Release(false);
            return;
        }
        characterController.Move(toHook.normalized * pullSpeed * Time.deltaTime);
    }

    private void Release(bool boost)
    {
        if (!grappling)
        {
            return;
        }
        grappling = false;
        lineRenderer.enabled = false;
        movement.movementLocked = false;
        movement.verticalVelocity = boost ? releaseUpBoost : 0f;
        nextUseTime = Time.time + cooldown;
    }

    private void LateUpdate()
    {
        if (!grappling)
        {
            return;
        }
        Vector3 start = lineOrigin != null ? lineOrigin.position : characterController.bounds.center;
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, hookPoint);
    }
}
