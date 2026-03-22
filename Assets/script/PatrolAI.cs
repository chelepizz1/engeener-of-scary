using UnityEngine;
using UnityEngine.AI;

public class PatrolAI : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float waitTime = 2f;
    public bool randomOrder = false;

    private NavMeshAgent agent;
    private Animator animator;

    private int currentPoint = 0;
    private float waitTimer;
    private bool isWaiting = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        GoToNextPoint();
    }

    void Update()
    {
        // 🔹 ОБНОВЛЯЕМ АНИМАЦИЮ
        float speed = agent.velocity.magnitude;
        animator.SetFloat("Speed", speed);

        // 🔹 ПРОВЕРКА ДОШЁЛ ЛИ ДО ТОЧКИ
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!isWaiting)
            {
                isWaiting = true;
                waitTimer = waitTime;
            }

            waitTimer -= Time.deltaTime;

            if (waitTimer <= 0f)
            {
                isWaiting = false;
                GoToNextPoint();
            }
        }
    }

    void GoToNextPoint()
    {
        if (patrolPoints.Length == 0) return;

        if (randomOrder)
        {
            int newPoint;
            do
            {
                newPoint = Random.Range(0, patrolPoints.Length);
            }
            while (newPoint == currentPoint);

            currentPoint = newPoint;
        }
        else
        {
            currentPoint = (currentPoint + 1) % patrolPoints.Length;
        }

        agent.SetDestination(patrolPoints[currentPoint].position);
    }
}