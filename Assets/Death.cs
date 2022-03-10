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
    [SerializeField] private int health = 100;
    [SerializeField] private int damage = 2;
    //[SerializeField] private int bites = 0;
    public Text healthText;
    const string preText1 = "HEALTH: "; 

    // Losing by timer

    public float timeValue = 180;
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

    void RefreshDisplay()
    {
        healthText.text = preText1 + health.ToString();     
    }

    void FixedUpdate()
    {
        Debug.Log("Original position" + originalPos);
        // Debug.Log("Current position" +transform.position);
        // ResetPosition();
        //   Bites();
        Health();
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
    }

    public void Health()
    {
        if (health <= 0)
        {
            ResetPosition();
            health = 100;
        }
        
    }
    //public void Bites()
    //{
    //    if (bites == 50)
    //    {
    //        ResetPosition();           
    //        bites = 0;
    //    }
    //}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Death"))
        {
            Debug.Log("DEATHZONE");
            health = 0;
            //bites = 50;
        }

        else if (other.CompareTag("Spider1") || other.CompareTag("Spider2") || other.CompareTag("Spider3"))
        {
            Debug.Log("BITES+1");
            //bites++;
            health -= damage;
            RefreshDisplay(); 
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Spider1") || other.CompareTag("Spider2") || other.CompareTag("Spider3"))
        {
            Debug.Log("BITES+1");
            // bites = bites++;
            health = health - ((int)(damage * Time.deltaTime));
            RefreshDisplay(); 
        }
    }
}
