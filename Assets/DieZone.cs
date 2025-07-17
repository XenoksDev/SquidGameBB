using UnityEngine;

public class DieZone : MonoBehaviour
{
    public Player p;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            p.DieB();
        }
        if (other.TryGetComponent<GiHun>(out GiHun g))
        {
            p.Win();
        }
    }
}
