using UnityEngine;
using UnityEngine.InputSystem;

public class ChestController : MonoBehaviour
{
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
            Debug.Log("Press E to open");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        float action = interactAction.ReadValue<float>();
        if (other.gameObject.name == "Player" && !isEmpty && action != 0)
        {
            isEmpty = true;
            Debug.Log("Looted");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Come back!");
    }

    void Update()
    {
        
    }
}
