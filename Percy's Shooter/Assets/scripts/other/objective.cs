using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Objective : MonoBehaviour
{
    public TextMeshProUGUI ObjectiveText;
    public string Level1;
    public string Level2;
    public string Level3;
    public string Level3Boss;
    public string Level1Objective;
    public string Level2Objective;
    public string Level3Objective;
    public string Level3BossObjective;
    private void OnLevelWasLoaded(int level)
    {
        // Create a temporary reference to the current scene.
        Scene currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of this scene.
        string sceneName = currentScene.name;

        if (sceneName == Level1)
        {
            if (ObjectiveText != null)
                ObjectiveText.SetText(Level1Objective);
        }
        else if (sceneName == Level2)
        {
            if (ObjectiveText != null)
                ObjectiveText.SetText(Level2Objective);
        }
        else if (sceneName == Level3)
        {
            if (ObjectiveText != null)
                ObjectiveText.SetText(Level3Objective);
        }
        else if (sceneName == Level3Boss)
        {
            if (ObjectiveText != null)
                ObjectiveText.SetText(Level3BossObjective);
        }
    }
    private void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("end"))
            SceneManager.LoadScene(Level2);
        if (coll.CompareTag("end2"))
            SceneManager.LoadScene(Level3);
        if (coll.CompareTag("end3"))
            SceneManager.LoadScene(Level3Boss);
    }
}
