using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveScript : MonoBehaviour
{
    string password = "1234567890";
    CharacterController CC;
    void Start()
    {
        CC = GetComponent<CharacterController>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            Save();
        }
        if(Input.GetKeyUp(KeyCode.Alpha5))
        {
            Load();
        }
    }
    public void Save()
    {
        //string result = EncryptDecryptData("a");
        //Debug.Log(result);
        SaveData myData = new SaveData();
        myData.x = transform.position.x;
        myData.y = transform.position.y;
        myData.z = transform.position.z;
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
            SaveData myData = JsonUtility.FromJson<SaveData>(jsonData);
            CC.enabled = false;
            transform.position = new Vector3(myData.x, myData.y, myData.z);
            CC.enabled = true;

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
public class SaveData
{
    public float x;
    public float y;
    public float z;
}