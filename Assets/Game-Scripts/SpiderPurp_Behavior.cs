using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(BoxCollider))]
public class SpiderPurp_Behavior : MonoBehaviour
{
    // TIMER
 
    public float timeValue = 5;
    [SerializeField] private GameObject spiderPurp;
    

    Animator anim;
    private BoxCollider boxColliderTrigger;
    private SphereCollider sphereCollider;
    private NavMeshAgent navSpider;
    private bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        // Setting components with desire values 
        // boxColliderTrigger is for recognize the player 
        boxColliderTrigger = GetComponent<BoxCollider>();
        boxColliderTrigger.isTrigger = true;
        boxColliderTrigger.size = new Vector3(1.5f, 1.5f, 1.5f);
        boxColliderTrigger.center = new Vector3(0f, 0.5f, 0f);

        // sphereCollider is to push the player 
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.radius = 0.4f;
        sphereCollider.center = new Vector3(0f, 0.5f, 0f);

        // anim control

        anim = GetComponent<Animator>();

        //Nav

        navSpider = GetComponent<NavMeshAgent>();

        // reduce agent radius to prevent clogging.

        navSpider.radius = 0.35f;

        navSpider.avoidancePriority = 2;



    }

    private void Update()
    {
        DeadSpiderTimer();
    }

    private void DeadSpiderTimer()
    {
        // Is dead becomes true when the spider is hit and checks if the timeValue >0
        // then subtracts one unit every frame and destroy the object from the heirarchy
        if (isDead && timeValue > 0)
        {
            timeValue -= Time.deltaTime;
            Debug.Log(timeValue);
            
        }
        else if (isDead && timeValue <= 0)
        {
            timeValue = 0;
            Debug.Log(timeValue);            
            Destroy(spiderPurp);
        }        

    }


    public void SpiderHit()
    {
        anim.SetTrigger("SpiderHit");
        navSpider.isStopped = true;
        sphereCollider.enabled = false;
        boxColliderTrigger.enabled = false;
        isDead = true;
     



    }

    private void OnTriggerEnter(Collider other)
    {
        // recognize if the player is touch 
        if (other.CompareTag("Player"))
        {
            anim.SetTrigger("Bite");
            Debug.Log("BITE ATTACK!");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetTrigger("Bite");
            Debug.Log("KEEP BITING!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // recognize if the player is running away 
        if (other.CompareTag("Player"))
        {
            Debug.Log("FOLLOW PLAYER!");
        }
    }

}

//https://gamedevbeginner.com/how-to-make-countdown-timer-in-unity-minutes-seconds/#:~:text=Making%20a%20countdown%20timer%20in,need%20to%20be%20calculated%20individually.
