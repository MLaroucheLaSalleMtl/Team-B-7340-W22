using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateZone : MonoBehaviour
{
    private EnemyWaves spiders;

    //private BoxCollider box;

    public void Start()
    {
        //spiders = GetComponent<EnemyWaves>();
        //box = GetComponent<BoxCollider>(); 
        //spiders = (EnemyWaves)GetComponent<EnemyWaves>().SpawnWave();
        spiders = GameObject.Find("MiniManager").GetComponent<EnemyWaves>(); 
    }

    //public void Awake()
    //{
    //    stopSpiders = true;
    //    activeSpiders = false; 
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Enter Zone");
            //spiders.spawnPoint.gameObject.SetActive(true);
            //spiders.enabled = true; 
            //stopSpiders = false;
            //activeSpiders = true;
            //spiders.gameObject.SetActive(true);
            spiders.SpawnSpider(true); 

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("ExitZone");
            //spiders.spawnPoint.gameObject.SetActive(false);
            //spiders.enabled = false; 
            //spiders.gameObject.SetActive(false);  
            spiders.SpawnSpider(false);
        }
    }
}


///////////////////////////////////*using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ActivateZone : MonoBehaviour
//{
//    private EnemyWaves spiders;

//    public void Start()
//    {
//        spiders = GetComponent<EnemyWaves>();
//        //spiders = (EnemyWaves)GetComponent<EnemyWaves>().SpawnWave();
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag("Player"))
//        {
//            Debug.Log("Enter Zone");
//            //spiders.spawnPoint.gameObject.SetActive(true);
//            spiders.enabled = true;

//        }
//    }

//    //private void OnTriggerStay(Collider other)
//    //{
//    //    if (other.gameObject.tag == "Player")
//    //    {
//    //        Debug.Log("Stay Zone");
//    //        //spiders.spawnPoint.gameObject.SetActive(true);
//    //        spiders.enabled = true;
//    //    }
//    //}

//    private void OnTriggerExit(Collider other)
//    {
//        if (other.CompareTag("Player"))
//        {
//            Debug.Log("ExitZone");
//            //spiders.spawnPoint.gameObject.SetActive(false);
//            spiders.enabled = false;
//        }
//    }
//}
//*//