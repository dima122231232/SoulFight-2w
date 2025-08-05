using UnityEngine;
using UnityEngine.AI;
using SoulFight.Utils;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float roamingDistanceMax = 7f;
    [SerializeField] private float roamingDistanceMin = 3f;
    [SerializeField] private float roamingTimerMax = 2f;
    [SerializeField] private State startingState;

    private NavMeshAgent navMeshAgent;
    private State state;
    private float roamingTime;
    private Vector3 roamPos;
    private Vector3 startingPos;

    private enum State
    {
        Idle,
        Roaming
    }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        state = startingState;
    }
    private void Start()
    {
        startingPos = transform.position;
    }

    private void Update()
    {
        switch (state)
        {
            default:
            case State.Idle:
                break;
            case State.Roaming:
                roamingTime -= Time.deltaTime;
                if (roamingTime < 0)
                {
                    Roaming();
                    roamingTime = roamingTimerMax;
                }

                break;
        }
    }

    private void Roaming()
    {
        roamPos = GetRoamingPos();
        navMeshAgent.SetDestination(roamPos);
    }

    private Vector3 GetRoamingPos() 
    {
        return startingPos + Utils.GetRandomDir() * UnityEngine.Random.Range(roamingDistanceMin, roamingDistanceMax);
    }
}
