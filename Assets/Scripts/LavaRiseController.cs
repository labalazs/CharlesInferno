using UnityEngine;

public class LavaRiseController : MonoBehaviour
{
    public float riseSpeed = 0.15f;
    public float startDelay = 3f;
    public GameObject player;
    public GameObject gameManager;

    private float timer = 0;
    private bool rising = true;
    private GameManager gm;

    private void Start()
    {
        gm = gameManager.GetComponent<GameManager>();
        GameData.lavaHeight = transform.position.y;
    }

    private void Update()
    {
        if (!rising)
        {
            return;
        }
        timer += Time.deltaTime;
        if (timer < startDelay)
        {
            return;
        }
        transform.position += Vector3.up * riseSpeed * Time.deltaTime;
        if (player.transform.position.y < transform.position.y)
        {
            gm.GameOver();
        }
    }

    public void SetHeight(float y)
    {
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
        timer = startDelay;
    }

    public void Reset()
    {
        SetHeight(GameData.lavaHeight);
    }

    public void Stop() => rising = false;
}
