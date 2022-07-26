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

    //Movement Speed
    [SerializeField]
    float _speed;
    //Game is Over
    private bool _gameOver = false;
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
    //Is the enemy hiding
    [SerializeField]
    private int _isHiding;
    //Explosion on death 
    [SerializeField]
    private GameObject _explosion;



    // Start is called before the first frame update

    private void OnEnable()
    {
        _speed = 2f;
        _currentState = AIState.Running;
        _agent.destination = _wayPoints[0].transform.position;
        StartCoroutine(RandomizeMovement());
    }
    private void Awake()
    {
        _speed = 2f;
        _agent = GetComponent<NavMeshAgent>();
        _currentState = AIState.Running;
        _agent.destination = _wayPoints[0].transform.position;
        StartCoroutine(RandomizeMovement());
    }


    // Update is called once per frame
    void Update()
    {
        AIControl();
        
    }


    private void AIControl()
    {
        switch(_currentState)
        {
            case AIState.Running:
                _agent.isStopped = false;
                MoveAI();
                _agent.speed = _speed;
                _anim.SetFloat("Speed", _speed);
                break;
            case AIState.Hiding:
                _speed = 0;
                _anim.SetFloat("Speed", _speed);
                _agent.isStopped = true;
                break;
            case AIState.Death:
                StopAllCoroutines();
                _agent.isStopped = true;
                _speed = 0;
                _anim.SetFloat("Speed", _speed);
                _anim.SetTrigger("Death");
                _explosion.SetActive(true);
                AudioManager.Instance.AiDeathAudio();
                Invoke("SetAiInactive", 3f);
                break;
        }
    }

    private void SetAiInactive()
    {
        _explosion.SetActive(false);
        this.gameObject.SetActive(false);
    }
    IEnumerator StopHiding()
    {
            StopCoroutine(RandomizeMovement());
            yield return new WaitForSeconds(Random.Range(2f, 5f));
            _currentState = AIState.Running;
            StartCoroutine(RandomizeMovement());
    }
    IEnumerator RandomizeMovement()
    {
        while(_gameOver == false)
        {
            yield return new WaitForSeconds(5.0f);
            _speed = Random.Range(1, 7);

        }
        
    }
    private void MoveAI()
    {

        if (_agent.remainingDistance <= 1f && _destination <=3 )
        {
            _destination++;
            _agent.destination = _wayPoints[_destination].transform.position;
          
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "HidingSpot")
        {
            int _isHiding = Random.Range(0, 100);
            if (_isHiding >= 89)
            {
                _currentState = AIState.Hiding;
                StartCoroutine(StopHiding());
            }
        }       
        if (other.tag == "Exit")
            {
                this.gameObject.SetActive(false);
                UIManager.Instance.LivesLeft();
            }

            return;
        
    }

    public void EnemyIsHit()
    {
        _currentState = AIState.Death;
    }
}
