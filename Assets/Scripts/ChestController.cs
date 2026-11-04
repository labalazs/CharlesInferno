using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChestController : MonoBehaviour
{
    public TextMeshProUGUI interactText;
    public int Item = 0;
    private bool isEmpty = false;
    private InputAction interactAction;

    void Start()
    {
        interactAction = InputSystem.actions.FindAction("Interact");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player" && !isEmpty)
        {
            interactText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        float action = interactAction.ReadValue<float>();
        if (other.gameObject.name == "Player" && !isEmpty && action != 0)
        {
            isEmpty = true;
            interactText.gameObject.SetActive(false);
            Debug.Log("Looted");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        interactText.gameObject.SetActive(false);
    }
}
