using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class DoorToSpiderLevel : MonoBehaviour
{

    [SerializeField] private Canvas canvas;

    private void Start()
    {
        canvas.enabled = false;
    }
    private void OnTriggerStay(Collider other)
    {
        canvas.enabled = true; 

        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("player is touching the door");
            SceneManager.LoadScene("SpiderRoom");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canvas.enabled = false;
    }
}
