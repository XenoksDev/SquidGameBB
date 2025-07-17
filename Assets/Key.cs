using UnityEngine;

public class Key : MonoBehaviour
{
    public int keyI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (keyI)
            {
                case 0:
                    Player.instance.hasR = true;
                    break;
                case 1:
                    Player.instance.hasT = true;
                    break;
                case 2:
                    Player.instance.hasC = true;
                    break;
            }
            Destroy(gameObject);
        }
    }
}
