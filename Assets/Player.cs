using Unity.Cinemachine;
using UnityEngine;

public class Player : MonoBehaviour
{

    public GameObject ragdoll;
    public GameObject Gfx;
    public Transform respawnPoint;

    public Rigidbody rb;
    public ThirdPersonMovement mov;

    public CinemachineCamera cam;
    public void Ragdoll(float force)
    {
        cam.Target.TrackingTarget = rb.transform;
        ragdoll.SetActive(true);
        Gfx.SetActive(false);
        mov.enabled = false;
        rb.AddForce(Vector3.up-Vector3.right * force, ForceMode.Force);
        Invoke("Die", 1f);

    }

    public void Die()
    {
        cam.Target.TrackingTarget = rb.transform;
        ragdoll.SetActive(false);
        Gfx.SetActive(true);
        cam.Target.TrackingTarget = transform;
        rb.transform.localPosition = new Vector3(0, 0, 0);
        transform.position = respawnPoint.position;

        Invoke("EnableMov", 0.2f);

    }

    void EnableMov()
    {
        mov.enabled = true;

    }
}
