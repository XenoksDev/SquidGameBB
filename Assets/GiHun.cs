using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GiHun : MonoBehaviour
{
    public Player player;

    public float minDistance = 1.5f;
    public float speed = 0.1f;

    bool attacking;

    Animator anim;

    public float kbForce;
    bool canMove = true;

    Rigidbody rb;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        if (!canMove || !SquidGameManager.instance.gameStarted) return;
        anim.SetBool("Walk", true);
        Vector3 PlayerTarget = player.transform.position;
        PlayerTarget.y = transform.position.y;
        transform.LookAt(PlayerTarget);



        if(Vector3.Distance(transform.position, PlayerTarget) > minDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, PlayerTarget, speed * Time.deltaTime);
        }
        else if (!attacking)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        attacking = true;
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(0.7f);

        if(attacking) StartCoroutine(player.GetAttacked(transform.position));
        yield return new WaitForSeconds(2f);
        attacking = false;
    }

    public void GetHit(Vector3 pos, bool bar = false)
    {
        if (!canMove) return;
        attacking = false;
        Vector3 dir = (transform.position - pos).normalized;

        if (bar)
        {
            rb.AddForce(dir * kbForce * 1.4f, ForceMode.Impulse);
        }else
        {
            rb.AddForce(dir * kbForce, ForceMode.Impulse);

        }
        StartCoroutine(Stun());
    }

    IEnumerator Stun()
    {
        anim.SetTrigger("Stun");
        canMove = false;
        yield return new WaitForSeconds(1.4f);
        canMove = true;

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, minDistance);
    }
}
