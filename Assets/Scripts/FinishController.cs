using UnityEngine;

public class FinishController : MonoBehaviour
{
    public GameObject gameManager;
    private GameManager gm;

    private void Start()
    {
        gm = gameManager.GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player" && gm != null)
        {
            gm.Win();
        }
    }
}
