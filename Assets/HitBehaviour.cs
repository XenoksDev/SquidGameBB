using UnityEngine;

public class HitBehaviour : MonoBehaviour
{
    public float hitRange = 1f;

    Animator anim;

    public LayerMask enemyLayer;

    GiHun enemy;
    bool hitting;

    public GameObject bar;
    public GameObject barPrefab;


    void Start()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    void Update()
    {
        RaycastHit hit;

        bool raycast = Physics.Raycast(transform.position, transform.forward, out hit, hitRange, enemyLayer);

        if (Input.GetKeyDown(KeyCode.Mouse0) && !hitting && SquidGameManager.instance.gameStarted)
        {
            hitting = true;

            anim.SetTrigger("Hit");


            if(raycast)
            {
                if(hit.transform.TryGetComponent<GiHun>(out enemy))
                {
                    Invoke("Hit", 0.4f);
                }
                
            }
            Invoke("Cooldown", 0.8f);

        }
        if (raycast && hit.transform.TryGetComponent<BarToPick>(out BarToPick b) && Input.GetKeyDown(KeyCode.E))
        {
            Destroy(b.gameObject);
            GetBar();
        }
    }

    void Cooldown()
    {
        hitting = false;
    }

    void Hit()
    {
        enemy.GetHit(transform.position, bar.activeSelf);

    }

    public void GetBar()
    {
        if (bar.activeSelf) return;

        hitRange *= 2;
        anim.SetLayerWeight(1, 1f); // 1f = fully active
        bar.SetActive(true);
    }

    public void LooseBar()
    {
        if (!bar.activeSelf) return;
        hitRange /= 2;

        bar.SetActive(false);
        GameObject newBar = Instantiate(barPrefab);
        newBar.transform.position = transform.position; 
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, transform.forward * hitRange);
    }
}
