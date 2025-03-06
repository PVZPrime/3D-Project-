using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using player;
using UnityEngine.SceneManagement;

public class SaveScript : MonoBehaviour
{
    string password = "1234567890";
    //CharacterController CC;
    // add ammo left
    PlayerShoot PS;
    PlayerHealth PH;
    ability2 A2;
    void Start()
    {
        //CC = GetComponent<CharacterController>();
        PS = GetComponent<PlayerShoot>();
        PH = GetComponent<PlayerHealth>();
        A2 = GetComponent<ability2>();
    }
    public void Save()
    {
        //string result = EncryptDecryptData("a");
        //Debug.Log(result);
        PlayerSaveData myData = new PlayerSaveData();
        myData.x = transform.position.x;
        myData.y = transform.position.y;
        myData.z = transform.position.z;
        myData.health = PH.health;
        myData.Ab1Timer = PS.saveCoolDown;
        myData.Ammo = PS.BulletsLeft;
        myData.Ab2Timer = A2.time;
        myData.Ab2TimeLeft = A2.Length;
        myData.Ab1TimerActive = PS.SaveCoolDownActive;
        myData.PSReload = PS.Reloading;
        myData.SceneName = SceneManager.GetActiveScene().name;
        string myDataString = JsonUtility.ToJson(myData);
        myDataString = EncryptDecryptData(myDataString);
        //Debug.Log(myDataString);
        string file = Application.persistentDataPath + "/" + gameObject.name + ".json";
        System.IO.File.WriteAllText(file, myDataString);
        //Debug.Log(file);
    }
    public void Load()
    {
        string file = Application.persistentDataPath + "/" + gameObject.name + ".json";
        if(File.Exists(file))
        {
            string jsonData = File.ReadAllText(file);
            jsonData = EncryptDecryptData(jsonData);
            PlayerSaveData myData = JsonUtility.FromJson<PlayerSaveData>(jsonData);
            //CC.enabled = false;
            transform.position = new Vector3(myData.x, myData.y, myData.z);
            //CC.enabled = true;
            PH.health = myData.health;
            PS.saveCoolDown = myData.Ab1Timer;
            PS.BulletsLeft = myData.Ammo;
            A2.time = myData.Ab2Timer;
            A2.Length = myData.Ab2TimeLeft;
            PS.SaveCoolDownActive = myData.Ab1TimerActive;
            PS.Reloading = myData.PSReload;
            SceneManager.LoadScene(myData.SceneName);

            //string myData = File.ReadAllText(file);
            //myData = EncryptDecryptData(myData);
            ////Debug.Log(myData);
        }
    }
    public string EncryptDecryptData(string data)
    {
        string result = "";
        for(int i = 0; i < data.Length; i++)
        {
            result += (char)(data[i] ^ password[i % password.Length]);
        }
        return result;
    }
}

[System.Serializable]
public class PlayerSaveData
{
    public float x;
    public float y;
    public float z;
    public int Ammo;
    public int ammoLeft;
    public float health;
    public float Ab1Timer;
    public float Ab2Timer;
    public float Ab2TimeLeft;
    public bool Ab1TimerActive;
    public bool PSReload;
    public string SceneName;
}