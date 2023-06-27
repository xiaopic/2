using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderOnTrigger : MonoBehaviour
{
public string LevelName;

    void OnTriggerEnter(Collider col) {
        if(col.CompareTag("Player")) {
            SceneManager.LoadScene(LevelName);
        }
    }
}
