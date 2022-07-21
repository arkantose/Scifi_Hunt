using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private enum AIState
    {
        Running,
        Hiding,
        Death
    }


    //Control Animator
    [SerializeField]
    Animator _anim;
    //Store NavMesh navigation
    [SerializeField]
    private NavMeshAgent _agent;
    //Store all way points and track
    [SerializeField]
    private List<GameObject> _wayPoints = new List<GameObject>();
    int _destination = 0;
    //Enum State Manual Switch
    [SerializeField]
    private AIState _currentState;
    // Start is called before the first frame update

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        _agent.destination = _wayPoints[0].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveAI();
    }

    private void MoveAI()
    {

        if (_agent.remainingDistance < .5f)
        {
            _destination++;
            _agent.destination = _wayPoints[_destination].transform.position;
          
        }
    }
}
