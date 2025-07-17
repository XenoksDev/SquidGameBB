using UnityEngine;

public class Lock : MonoBehaviour
{
    bool moving;
    Quaternion targetRotation;
    Transform doorTransform;
    public int lockI;

    public float rotationAngle = 90f; // Angle d’ouverture
    float smoothSpeed = 1.5f;    // Vitesse de rotation

    private void Start()
    {
        doorTransform = transform.parent; // Le pivot de la porte
        targetRotation = doorTransform.rotation; // rotation de base
    }

    private void Update()
    {
        if (moving)
        {
            doorTransform.rotation = Quaternion.Lerp(doorTransform.rotation, targetRotation, Time.deltaTime * smoothSpeed);

            // Si on est très proche de la rotation finale, on stoppe
            if (Quaternion.Angle(doorTransform.rotation, targetRotation) < 0.1f)
            {
                doorTransform.rotation = targetRotation;
                moving = false;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E) && !moving && hasKey())
        {
            // Ajoute une rotation d’ouverture autour de l’axe Y
            targetRotation = doorTransform.rotation * Quaternion.Euler(0, 0, rotationAngle);
            moving = true;
            Destroy(gameObject, 1.5f);
        }
    }

    bool hasKey()
    {
        switch (lockI)
        {
            case 0:
                if (Player.instance.hasR) return true;
                break;
            case 1:
                if (Player.instance.hasT) return true;
                break;
            case 2:
                if (Player.instance.hasC) return true;
                break;
        }
        return false;
    }
}
