using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using player;
using Enemy;

public class EnemySave : MonoBehaviour
{
    string password = "1234567890";
    EnemyHP EH;
    EnemyAi EA;
    EnemyAiMelee EAM;
    Vector3 walkEA;
    Vector3 walkEAM;
    void Start()
    {
        EA = GetComponent<EnemyAi>();
        EAM = GetComponent<EnemyAiMelee>();
        EH = GetComponent<EnemyHP>();
    }
    public void Save()
    {
        //string result = EncryptDecryptData("a");
        //Debug.Log(result);
        EnemySaveData myData = new EnemySaveData();
        myData.x = transform.position.x;
        myData.y = transform.position.y;
        myData.z = transform.position.z;
        if(EA != null) myData.walkxEA = EA.walkPoint.x;
        if(EA != null) myData.walkyEA = EA.walkPoint.y;
        if(EA != null) myData.walkzEA = EA.walkPoint.z;
        if(EAM != null) myData.walkx = EAM.walkPoint.x;
        if(EAM != null) myData.walky = EAM.walkPoint.y;
        if(EAM != null) myData.walkz = EAM.walkPoint.z;
        myData.health = EH.health;
        myData.active = EH.EnemyDead;
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
        if (File.Exists(file))
        {
            string jsonData = File.ReadAllText(file);
            jsonData = EncryptDecryptData(jsonData);
            EnemySaveData myData = JsonUtility.FromJson<EnemySaveData>(jsonData);
            transform.position = new Vector3(myData.x, myData.y, myData.z);
            if (EA != null) EA.walkPoint = new Vector3(myData.walkxEA, myData.walkyEA, myData.walkzEA);
            if (EAM != null) EAM.walkPoint = new Vector3(myData.walkx, myData.walky, myData.walkz);
            EH.health = myData.health;
            EH.EnemyDead = myData.active;

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
public class EnemySaveData
{
    public float x;
    public float y;
    public float z;
    public float walkx;
    public float walky;
    public float walkz;
    public float walkxEA;
    public float walkyEA;
    public float walkzEA;
    public float health;
    public bool active;
}