using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class MiniManager : MonoBehaviour
{
    public static MiniManager instance = null; // singleton 

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


}
