using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using player;

public class SaveBullet : MonoBehaviour
{
    string password = "1234567890";
    DestroyBullet DB;
    void Start()
    {
        DB = GetComponent<DestroyBullet>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Save();
        }
        if (Input.GetKeyUp(KeyCode.Alpha5))
        {
            Load();
        }
    }
    public void Save()
    {
        //string result = EncryptDecryptData("a");
        //Debug.Log(result);
        BulletSaveData myData = new BulletSaveData();
        myData.x = transform.position.x;
        myData.y = transform.position.y;
        myData.z = transform.position.z;
        myData.wasShot = DB.Active;
        myData.TimeLeft = DB.Timer;

        string myDataString = JsonUtility.ToJson(myData);
        myDataString = EncryptDecryptData(myDataString);
        //Debug.Log(myDataString);
        string file = Application.persistentDataPath + "/" + gameObject.GetInstanceID() + ".json";
        System.IO.File.WriteAllText(file, myDataString);
        //Debug.Log(file);
    }
    public void Load()
    {
        string file = Application.persistentDataPath + "/" + gameObject.GetInstanceID() + ".json";
        if (File.Exists(file))
        {
            string jsonData = File.ReadAllText(file);
            jsonData = EncryptDecryptData(jsonData);
            BulletSaveData myData = JsonUtility.FromJson<BulletSaveData>(jsonData);
            transform.position = new Vector3(myData.x, myData.y, myData.z);
            DB.Active = myData.wasShot;
            DB.Timer = myData.TimeLeft;

            //string myData = File.ReadAllText(file);
            //myData = EncryptDecryptData(myData);
            ////Debug.Log(myData);
        }
    }
    public string EncryptDecryptData(string data)
    {
        string result = "";
        for (int i = 0; i < data.Length; i++)
        {
            result += (char)(data[i] ^ password[i % password.Length]);
        }
        return result;
    }
}

[System.Serializable]
public class BulletSaveData
{
    public float x;
    public float y;
    public float z;
    public float TimeLeft;
    public bool wasShot;
}