using Pathfinding;
using UnityEngine;

public class EnemyPathingAI : MonoBehaviour
{
    [Header("Pathfinding")]
    [SerializeField]
    private Transform target;

    [SerializeField]
    private float activeDistance = 50f;

    [SerializeField]
    private float pathUpdateTime = 0.5f;

    [SerializeField]
    private float nextWaypointDistance = 3f;

    [SerializeField]
    private bool canFollow = true;

    private Path path;
    private int currentWaypoint = 0;
    private bool reacheadEndOfPath = false;
    private Seeker seeker;
    private float speed;
    private Rigidbody2D rb;

    private void Awake()
    {
        seeker = GetComponent<Seeker>();
        speed = GetComponent<Enemy>().MovementSpeed;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(UpdatePath), 0f, pathUpdateTime);
    }

    private void FixedUpdate()
    {
        if (IsTargetInDistance() && canFollow)
        {
            FollowPath();
        }
    }

    private void FollowPath()
    {
        if (path == null)
        {
            return;
        }
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reacheadEndOfPath = true;
            return;
        }
        else
        {
            reacheadEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;

        rb.velocity = new Vector2(direction.x * speed, direction.y * speed);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

    private void UpdatePath()
    {
        if (IsTargetInDistance() && canFollow && seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    private void OnPathComplete(Path path)
    {
        if (!path.error)
        {
            this.path = path;
            currentWaypoint = 0;
        }
    }

    private bool IsTargetInDistance()
    {
        if (target)
        {
            return Vector2.Distance(transform.position, target.position) < activeDistance;
        }
        return false;
    }
}
