using TMPro;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public TextMeshProUGUI CheckpointText;
    public GameObject Camera;
    public GameObject Lava;
    public AudioClip ReachedSound;
    private AudioSource audioSource;
    private CameraMovement cameraMovement;
    private float elapsedTime = 0.0f;
    private bool isTriggered = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

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
            elapsedTime = 0.0f;
            GameData.Set(other.gameObject.transform.position, cameraMovement.HorizontalRotation, Lava.transform.position.y);
            CheckpointText.gameObject.SetActive(true);
            audioSource.PlayOneShot(ReachedSound);
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
                elapsedTime = 0.0f;
            }
        }
    }
}
