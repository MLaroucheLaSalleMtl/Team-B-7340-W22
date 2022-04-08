using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MiniManager : MonoBehaviour
{
    public static MiniManager instance = null; // singleton 

    public AudioSource collectSound;
    public Text trophyText;
    public int score = 0;

    private void Start()
    {
        RefreshScore(); 
    }

    public void CompleteLevel()
    {
        Debug.Log("LEVEL COMPLETE");
        //SceneManager.LoadScene("6thFloor"); 
    }

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetButtonUp("Cancel"))
            Application.Quit();

    }

    public void AddPoints()
    {
        score += 1;
        collectSound.Play(); 
        RefreshScore();
    }

    void RefreshScore()
    {
        trophyText.text = score.ToString(); 
    }


    //private void OnTriggerEnter(Collider other)
    //{
    //    collectSound.Play();
    //    score++;
    //    trophyText.GetComponent<Text>().text = score.ToString();
    //    Destroy(gameObject);
    //}
    public void CompleteSpiders()
    {
        SceneManager.LoadScene("LoadingScene"); 
    }
}
