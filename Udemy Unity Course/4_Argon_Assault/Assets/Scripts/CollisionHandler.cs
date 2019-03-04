using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour {

   [Tooltip("In seconds")] [SerializeField] float loadLevelDelay = 2f;
   [Tooltip("FX prefab on player")] [SerializeField] GameObject deathFX;
    private void OnTriggerEnter(Collider other)
    {
        StartDeathSequence();
        deathFX.SetActive(true);
        Invoke("ReloadScene",loadLevelDelay);
    }

    private void StartDeathSequence()
    {
        SendMessage("OnePlayerDeath");
    }

    private void ReloadScene() //STring refferenced
    {
        SceneManager.LoadScene(1);
    }
}
