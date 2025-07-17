using UnityEngine;

public class BarToPick : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if(other.TryGetComponent<HitBehaviour>(out HitBehaviour hit) && Input.GetKeyDown(KeyCode.E))
        {
            hit.GetBar();
            Destroy(gameObject);
        }
    }
}
