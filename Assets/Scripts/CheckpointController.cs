using TMPro;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public TextMeshProUGUI CheckpointText;
    public GameObject Camera;
    public GameObject Lava;
    private CameraMovement cameraMovement;
    private float elapsedTime = 0.0f;
    private bool isTriggered = false;

    private void Start()
    {
        elapsedTime = 0.0f;
        isTriggered = false;
        cameraMovement = Camera.GetComponent<CameraMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player" && !isTriggered)
        {
            isTriggered = true;
            GameData.Set(other.gameObject.transform.position, cameraMovement.HorizontalRotation, Lava.transform.position.y);
            CheckpointText.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (CheckpointText.gameObject.activeInHierarchy)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= 2)
            {
                CheckpointText.gameObject.SetActive(false);
            }
        }
    }
}
