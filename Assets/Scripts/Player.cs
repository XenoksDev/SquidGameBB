using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public GameObject ragdoll;
    public GameObject Gfx;
    public Transform respawnPoint;

    public Rigidbody rb;
    public ThirdPersonMovement mov;

    public float stunForce = 40f;

    public CinemachineFreeLook cam;

    public LayerMask groundLayer;
    

    public static Player instance;

    public bool hasR;
    public bool hasT;
    public bool hasC;

    RaycastHit rbGround;
    bool rbGrounded;

    bool attacked;

    private void Start()
    {
        instance = this;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Ragdoll(float force)
    {
        cam.Follow = rb.transform;
        cam.LookAt = rb.transform;
        ragdoll.SetActive(true);
        Gfx.SetActive(false);
        mov.enabled = false;
        rb.AddForce(Vector3.up-Vector3.right * force, ForceMode.Force);
        Invoke("Die", 1f);

    }

    public IEnumerator Stun(Vector3 pos)
    {
        yield return new WaitForSeconds(0.8f);
        cam.Follow = rb.transform;
        cam.LookAt = rb.transform;
        ragdoll.SetActive(true);
        Gfx.SetActive(false);
        mov.enabled = false;
        Vector3 dir = (transform.position - pos).normalized;
        rb.AddForce(Vector3.up * stunForce, ForceMode.Force);
        Invoke("DieB", 1f);

    }

    public void Die()
    {
        cam.Follow = transform;
        cam.LookAt = transform;
        ragdoll.SetActive(false);
        Gfx.SetActive(true);
        rb.transform.localPosition = new Vector3(0, 0, 0);
        transform.position = respawnPoint.position;

        Invoke("EnableMov", 0.2f);

    }

    public IEnumerator GetAttacked(Vector3 pos)
    {
        
        if (mov.grounded)
        {
            GetComponent<HitBehaviour>().LooseBar();
            attacked = true;
            ragdoll.SetActive(true);
            Gfx.SetActive(false);
            mov.enabled = false;

            cam.Follow = rb.transform;
            cam.LookAt = rb.transform;
            Vector3 dir = (transform.position - pos).normalized;

            rb.AddForce(dir * stunForce, ForceMode.Force);
            rb.AddForce(Vector3.up * stunForce/3, ForceMode.Force);

            yield return new WaitForSeconds(2f);
            if (rbGrounded) UnStun();
        }
    }

    private void Update()
    {


        rbGrounded = Physics.Raycast(rb.transform.position, Vector3.down, out rbGround, 0.8f, groundLayer);

        if(rbGrounded && !mov.enabled && !attacked)
        {
            UnStun();
        }

    }

    void UnStun()
    {
        cam.Follow = transform;
        cam.LookAt = transform;
        ragdoll.SetActive(false);
        Gfx.SetActive(true);
        transform.position = rb.transform.position;

        rb.transform.localPosition = new Vector3(0, 0.5f, 0);
        attacked = false;
        Invoke("EnableMov", 0.2f);
    }

    public void DieB()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    void EnableMov()
    {
        mov.enabled = true;

    }

    private void OnDrawGizmos()
    {
        rbGrounded = Physics.Raycast(rb.transform.position, Vector3.down, out rbGround, 0.1f, groundLayer);
        Gizmos.DrawRay(ragdoll.transform.position, Vector3.down);
    }

    public void Win()
    {
        SceneManager.LoadScene(0);
    }
}
