using UnityEngine.AI;
using UnityEngine;

// This integrate the desire component to the obj 
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(BoxCollider))]

public class FollowTarget : MonoBehaviour
{
    // Variables 

    // To follow 
    [SerializeField] private Transform target;
    private Vector3 destination;
    private NavMeshAgent agent;

    // For components 
    private BoxCollider boxColliderTrigger;
    private SphereCollider sphereCollider;

    // For Moving 
    Vector3 lastPosition;
    Transform myTransform;
    bool isMoving;
    Animator anim;

    //public GameObject Player; 


    void Start()
    {
        //Player = GameObject.Find("New Player");
        //target = Player.transform; 


        //// Prefab 
        //Instantiate(Player, new Vector3(0, 0, 0), Quaternion.identity); 
        
        // 
        myTransform = transform;
        lastPosition = myTransform.position;
        isMoving = false;

        anim = GetComponent<Animator>();

        // Cach agent component and destination
        agent = GetComponent<NavMeshAgent>();
        destination = agent.destination;
        
        // Setting components with desire values 
        // boxColliderTrigger is for recognize the player 
        boxColliderTrigger = GetComponent<BoxCollider>();
        boxColliderTrigger.isTrigger = true;
        boxColliderTrigger.size = new Vector3(1.5f, 1.5f, 1.5f); 
        boxColliderTrigger.center = new Vector3(0f, 0.5f, 0f); 

        // sphereCollider is to push the player 
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.radius = 0.8f;
        sphereCollider.center = new Vector3(0f, 0.5f, 0f);

    }

    public void Update()
    {
        Follow();
        IsMoving(); 
    }

    private void IsMoving()
    {
        if (myTransform.position != lastPosition)
        {
            isMoving = true;
            if (isMoving)
            {
                anim.SetFloat("Speed", 1.0f);
            }
        }
        else
        {
            isMoving = false;
            if (!isMoving)
            {
                anim.SetFloat("Speed", 0.0f); 
            }
        }
        lastPosition = myTransform.position; 
    }

    private void Follow()
    {
        if (Vector3.Distance(destination, target.position) > 0.1f)
        {
            destination = target.position;
            agent.destination = destination; 
        }
    }

    //void FixedUpdate()
    //{
    //    // Identified the distance between the player and the spiders 
    //    if (Vector3.Distance(destination, target.position) > 0.1f)
    //    {
    //        destination = target.position;
    //        agent.destination = destination;
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    // recognize if the player is touch 
    //    if (other.CompareTag("Player"))
    //    {
    //        Debug.Log("Player Reached!"); 
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    // recognize if the player is running away 
    //    if (other.CompareTag("Player"))
    //    {
    //        Debug.Log("Player Got Away!"); 
    //    }
    //}
}
