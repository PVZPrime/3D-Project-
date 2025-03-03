using Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficulty : MonoBehaviour
{
    public float EasyMultiplier;
    public float MediumMultiplier;
    public float HardMultiplier;
    bool EasyActive;
    bool MediumActive;
    bool HardActive;

    private void Start()
    {
        EasyActive = false;
        MediumActive = false;
        HardActive = false;
    }

    private void OnLevelWasLoaded(int level)
    {
        if (EasyActive) Easy();
        else if (MediumActive) Medium();
        else if (HardActive) Hard();
    }


    public void Easy()
    {
        EasyActive = true;
        MediumActive = false;
        HardActive = false;
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            go.GetComponent<EnemyHP>().health *= EasyMultiplier;
        }
    }

    public void Medium()
    {
        MediumActive = true;
        EasyActive = false;
        HardActive = false;
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            go.GetComponent<EnemyHP>().health *= MediumMultiplier;
        }
    }

    public void Hard()
    {
        HardActive = true;
        EasyActive = false;
        MediumActive = false;
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            go.GetComponent<EnemyHP>().health *= HardMultiplier;
        }
    }
}
