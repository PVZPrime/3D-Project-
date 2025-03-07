using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel1 : MonoBehaviour
{
    GameObject boss;
    public string levelToLoad = "Win";
    void Update()
    {
        boss = GameObject.FindWithTag("Enemy");
        if (boss == null)
        {
            SceneManager.LoadScene(levelToLoad);
        }
    }
    private void OnLevelWasLoaded(int level)
    {
        boss = GameObject.FindWithTag("Enemy");
    }
}