using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PlayerSceneTransfer : MonoBehaviour
{
    GameObject SpawnPos;
    GameObject menu;
    GameObject player;

    public bool OriginalPlayer = false;

    // Start is called before the first frame update
    void Start()
    {

        menu = GameObject.FindGameObjectWithTag("menu");
        player = GameObject.FindGameObjectWithTag("Player2");
        SpawnPos = GameObject.FindGameObjectWithTag("Scene Start Point");
        if (player != null)
        {
            OriginalPlayer = true;
        }
        if (menu != null)
        {
            GameObject.FindGameObjectWithTag("Player").SetActive(false);
            GetComponentInChildren<Canvas>().enabled = false;
        }
        if (menu == null)
        {
            GameObject.FindGameObjectWithTag("Player").SetActive(true);
            GetComponentInChildren<Canvas>().enabled = true;
        }
    }

    void Awake()
    {
            if (CompareTag("Player2"))
            {
                DontDestroyOnLoad(this.gameObject);
            }
        SpawnPos = GameObject.FindGameObjectWithTag("Scene Start Point");
        if (SpawnPos != null)gameObject.transform.position = SpawnPos.transform.position;
    }
    private void OnLevelWasLoaded(int level)
    {

        menu = GameObject.FindGameObjectWithTag("menu");
        if (menu != null)
        {
            GameObject.FindGameObjectWithTag("Player").SetActive(false);
            GetComponentInChildren<Canvas>().enabled = false;
        }
        if (menu == null)
        {
            GameObject.FindGameObjectWithTag("Player").SetActive(true);
            GetComponentInChildren<Canvas>().enabled = true;
        }
        if (OriginalPlayer == false)
        {
            Destroy(gameObject);
        }
        SpawnPos = GameObject.FindGameObjectWithTag("Scene Start Point 1");
        gameObject.transform.position = SpawnPos.transform.position;
        menu = GameObject.FindGameObjectWithTag("menu");
    }
}
