using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaves : MonoBehaviour
{
    public Transform spiderPrefab;

    public Transform spawnPoint;
    public Transform spawnPoint1;
    public Transform spawnPoint2;

    public float timeBetweenWaves = 5.0f;
    private float countdown = 2.0f;

    private int waveIndex = 0; 

    private void Update()
    {
        if (countdown <= 0.0f)
        {
            StartCoroutine(SpawnWave()); 
            countdown = timeBetweenWaves;              
        }
        countdown -= Time.deltaTime; 
    }

    IEnumerator SpawnWave()
    {
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnSpiders();
            yield return new WaitForSeconds(1.0f); 
        }
        waveIndex++; 
    }

    void SpawnSpiders()
    {
        Instantiate(spiderPrefab, spawnPoint.position, spawnPoint.rotation);
        Instantiate(spiderPrefab, spawnPoint1.position, spawnPoint1.rotation);
        Instantiate(spiderPrefab, spawnPoint2.position, spawnPoint2.rotation);
    }


    //void OnGUI()
    //{
    //    //https://docs.unity3d.com/ScriptReference/Random.Range.html
    //    if (GUI.Button(new Rect(10, 10, 100, 50), "Instantiate!"))
    //    {
    //        var position = new Vector3(Random.Range(-10.0f, 10.0f), 0, Random.Range(-10.0f, 10.0f));
    //        Instantiate(spiderPrefab, position, Quaternion.identity); 
    //    }
    //    //Instantiate(spiderPrefab, spawnPoint.position, spawnPoint.rotation); 
    //}


}
