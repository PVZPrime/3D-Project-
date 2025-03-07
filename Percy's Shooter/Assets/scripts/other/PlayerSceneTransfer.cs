using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PlayerSceneTransfer : MonoBehaviour
{
    public GameObject SpawnPos;
    public GameObject menu;
    public GameObject player;
    public GameObject player2;

    public bool OriginalPlayer = false;
    public int count;
    float time;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        menu = GameObject.FindGameObjectWithTag("menu");
        SpawnPos = GameObject.FindGameObjectWithTag("Scene Start Point");
        if (OriginalPlayer == false)
        {
            OriginalPlayer = true;
        }
        if (menu != null)
        {
            player.SetActive(false);
            GetComponentInChildren<Canvas>().enabled = false;
        }
        if (menu == null)
        {
            player.SetActive(true);
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
    private void Update()
    {
        time += Time.deltaTime;

        if(count == 1)
        {
            player.transform.localPosition = new Vector3(0, 0, 0);
            count = 0;
            gameObject.transform.position = SpawnPos.transform.position;
            
        }
    }
    private void OnLevelWasLoaded(int level)
    {

        menu = GameObject.FindGameObjectWithTag("menu");

        if (menu != null)
        {
            player.SetActive(false);
            GetComponentInChildren<Canvas>().enabled = false;
        }
        if (menu == null)
        {
            player.SetActive(true);
            GetComponentInChildren<Canvas>().enabled = true;
        }
        if (OriginalPlayer == false)
        {
            Destroy(gameObject);
        }
        SpawnPos = GameObject.FindGameObjectWithTag("Scene Start Point");
        count = 1;
    }
}
