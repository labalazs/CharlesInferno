using TMPro;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public TextMeshProUGUI checkpointText;
    private float elapsedTime = 0.0f;
    private bool isTriggered = false;

    private void Start()
    {
        elapsedTime = 0.0f;
        isTriggered = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player" && !isTriggered)
        {
            isTriggered = true;
            checkpointText.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (checkpointText.gameObject.activeInHierarchy)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= 2)
            {
                checkpointText.gameObject.SetActive(false);
            }
        }
    }
}
