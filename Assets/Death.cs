using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
  
    // For dying by falling or by health/bites 

    //[SerializeField] Transform player;
    private Vector3 originalPos;
    [SerializeField] public int health = 100;
     
    //[SerializeField] private int bites = 0;
    public Text healthText;
    const string preText1 = "HEALTH: ";

    // Losing by timer

    public float timeValue = 180;
    public float penalty = 30f;
    public float bonus = 20f;
    public Text timerText;

    private void Start()
    {
        RefreshDisplay();
    }

    void Awake()
    {
        originalPos = transform.position;
    }

    void Update()
    {
        if (timeValue > 0)
        {
            timeValue -= Time.deltaTime;
        }
        else
        {
            timeValue = 0;
            SceneManager.LoadScene("lostMenu");
            Debug.Log("YOU LOST!");
        }
      
        DisplayTime(timeValue);
    }

    void FixedUpdate()
    {
        Health();
    }

    public void RefreshDisplay()
    {
        healthText.text = preText1 + health.ToString();
    }
    public void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public void ResetPosition()
    {
        transform.position = originalPos;
        health = 100;
    }

    public void Health()
    {
        if (health <= 0)
        {

            ResetPosition();           
            RefreshDisplay();
            timeValue -= penalty;
        }
             

    }

    public void TimeModifier()
    {

        if (gameObject.GetComponent<ThirdPersonMoving>().kills % bonus == 0 && gameObject.GetComponent<ThirdPersonMoving>().kills != 0)
        {
            timeValue += bonus;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Death"))
        {             
            health = 0;            
        }
        
        if (other.CompareTag("CheckPoint"))
        {
            Debug.Log("CHECKPOINT REACHED");
            originalPos = transform.position;
        }
         
    }

    
    
}
