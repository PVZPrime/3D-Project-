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
    public void Save()
    {
        //string result = EncryptDecryptData("a");
        //Debug.Log(result);
        BulletSaveData myData = new BulletSaveData();
        myData.x = gameObject.transform.position.x;
        myData.y = gameObject.transform.position.y;
        myData.z = gameObject.transform.position.z;
        myData.xVel = gameObject.GetComponent<Rigidbody>().velocity.x;
        myData.yVel = gameObject.GetComponent<Rigidbody>().velocity.y;
        myData.zVel = gameObject.GetComponent<Rigidbody>().velocity.z;
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
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShoot>().enabled = false;
            Debug.Log($"Loaded Bullet Data: Position ({myData.x}, {myData.y}, {myData.z}), Velocity ({myData.xVel}, {myData.yVel}, {myData.zVel})");

            // Get the Rigidbody
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();

            // Ensure no forces are acting on the Rigidbody during position change
            rb.isKinematic = true; // Temporarily disable physics for manual position change

            // Check for parent object (to avoid issues with localPosition vs worldPosition)
            if (gameObject.transform.parent != null)
            {
                // If the object has a parent, update the local position (relative to parent)
                gameObject.transform.localPosition = new Vector3(myData.x, myData.y, myData.z);
            }
            else
            {
                // Set the world position directly
                gameObject.transform.position = new Vector3(myData.x, myData.y, myData.z);
            }

            // Restore kinematic state of the Rigidbody
            rb.isKinematic = false;
            // Set the velocity only if the Rigidbody is non-kinematic
            if (!rb.isKinematic)
            {
                rb.velocity = new Vector3(myData.xVel, myData.yVel, myData.zVel);
            }


            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShoot>().enabled = true;
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
    public float xVel;
    public float yVel;
    public float zVel;
    public float TimeLeft;
    public bool wasShot;
}