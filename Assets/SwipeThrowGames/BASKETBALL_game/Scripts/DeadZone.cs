using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// RASKALOF, v1.0, Script destroy ball if it out of game zone

public class DeadZone : MonoBehaviour {
    [SerializeField] string ball_tag = "Player"; // Set here in editor ball tag, for default its "Player"

    private void OnTriggerEnter(Collider other) {
        if(other.tag == ball_tag) { // If in this zone enters ball
            Basketball_game_manager.Instance.CheckGameStatus(); // Destroy ball and restart situation (see CheckGameStatus())
        }
    }
}
