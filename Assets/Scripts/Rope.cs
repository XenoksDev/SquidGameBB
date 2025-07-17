using UnityEngine;

public class Rope : MonoBehaviour
{

    public float force = 5f;
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.transform.tag == "Player")
        {
            Player p = collision.transform.GetComponent<Player>();

            p.Ragdoll(force);
        }
    }
}
