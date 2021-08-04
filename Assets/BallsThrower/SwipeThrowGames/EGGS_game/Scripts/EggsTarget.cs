using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// RASKALOF, v1.0, Script handles target's logic
public class EggsTarget : MonoBehaviour {
    [SerializeField] byte myscore = 1; // Target score in case of hit
    [SerializeField] string egg_tag = "Respawn"; // Set here tag what ball have
    [SerializeField] AudioClip score_sound = null; // Sound to play if ball hit this target

    private void OnTriggerEnter(Collider collision) {
        if(collision.transform.tag == egg_tag) {
            collision.transform.gameObject.GetComponent<Cracker>()?.SetHittedStatus(); // Set what ball hit target during this throw
            GameObject sound = new GameObject("sound"); // Creates new GO
            sound.AddComponent<AudioSource>().PlayOneShot(score_sound); // Play sound
            Destroy(sound, score_sound.length); // Display after play ends
            EggsGameManager.Instance.AddScore(myscore); // Add score
            EggsGameManager.Instance.SpawnTarget(); // Spawn new target
        }
    }

    public byte GetTargetScore() {
        return myscore;
    }
}
