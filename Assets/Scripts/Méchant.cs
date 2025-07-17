using UnityEngine;
using UnityEngine.AI;

public class Méchant : MonoBehaviour
{
    public Transform[] points;
    NavMeshAgent agent;

    Transform player;
    public float detectRange;
    Vector3 destination;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        NewGoal();
    }

    public void Update()
    {
        bool playerInRange = Vector3.Distance(transform.position, player.position) < detectRange;
        if(Vector3.Distance(transform.position, destination) <= 0.1f && !playerInRange)
        {
            NewGoal();
        }
        if (playerInRange)
        {
            agent.SetDestination(player.position);
            // Poignardage
            StartCoroutine(player.GetComponent<Player>().Stun(transform.position));
            detectRange = 0;
            GetComponent<Animator>().SetTrigger("Attack");
            Debug.Log("Hagraa");
        }
    }


    void NewGoal()
    {
        int rand = Random.Range(0, points.Length);
        destination = points[rand].position;
        agent.SetDestination(destination);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }
}
