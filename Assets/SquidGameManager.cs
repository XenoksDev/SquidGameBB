using UnityEngine;

public class SquidGameManager : MonoBehaviour
{
    public static SquidGameManager instance;
    public Material greenMat;

    public bool gameStarted;
    private void Start()
    {
        instance = this;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.transform.tag == "Player" && !gameStarted)
        {

            gameStarted = true;
            GetComponent<MeshRenderer>().material = greenMat;

            // SFX
        }
    }

}
