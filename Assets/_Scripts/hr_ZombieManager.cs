using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class hr_ZombieManager : MonoBehaviour
{
    [SerializeField] private bool isAware = false;
    [SerializeField] private float fov = 120.0f;
    [SerializeField] private float viewDistance = 10.0f;
    [SerializeField] private LayerMask allMasks;
    [SerializeField] private int health = 100;
    [SerializeField] private float wanderRadius = 7.0f;

    private GameObject player;
    private NavMeshAgent agent;
    private Renderer renderer;

    private Vector3 wanderPoint = Vector3.zero;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        renderer = this.GetComponent<Renderer>();
        player = GameObject.Find("Player");
        wanderPoint = RandomWanderPoint();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        if (isAware)
        {
            agent.SetDestination(player.transform.position);
            renderer.material.color = Color.red;
        }
        else
        {
            SearchForPlayer();
            Wander();
            renderer.material.color = Color.blue;
        }
    }

    private void SearchForPlayer()
    {
        if (Vector3.Angle(Vector3.forward, transform.InverseTransformPoint(player.transform.position)) < fov / 2.0f)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < viewDistance)
            {
                RaycastHit hit;
                if (Physics.Linecast(transform.position, player.transform.position, out hit, allMasks))
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        OnAware();
                    }
                }
            }
        }
    }

    public void OnAware()
    {
        isAware = true;
        FindObjectOfType<AudioManager>().PlayScaredShout();
    }

    public void TakeDamage()
    {
        health -= 10;

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public Vector3 RandomWanderPoint()
    {
        Vector3 randomPoint = (Random.insideUnitSphere * wanderRadius) + transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomPoint, out hit, wanderRadius, allMasks);
        return new Vector3(hit.position.x, transform.position.y, hit.position.z);
    }

    public void Wander()
    {
        if (Vector3.Distance(transform.position, wanderPoint) < 1.5f)
        {
            wanderPoint = RandomWanderPoint();
        }
        else
        {
            agent.SetDestination(wanderPoint);
        }
    }
}
