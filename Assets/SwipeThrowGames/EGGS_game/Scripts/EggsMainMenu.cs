using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// RASKALOF, v1.0, For main menu purposes
public class EggsMainMenu : MonoBehaviour {
    [SerializeField] byte start_level = 0; // Number of start level

    public void StartGame() {
        SceneManager.LoadScene(start_level);
    }
    
}
