using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// RASKALOF, v1.0, Script handles all this hoop stuff
[RequireComponent(typeof(AudioSource))]
public class HoopScript : MonoBehaviour {
    [SerializeField] int my_score = 0; // How much add to players score if ball hit this hoop
    [SerializeField] Transform add_score_text_transform = null; // Position where to appear add score text effect
    [SerializeField] GameObject add_score_text_prefab = null; // Add score text effect prefab
    [SerializeField] GameObject particle_effect = null; // Link to particle effect if ball hit this hoop
    [SerializeField] AudioClip goal_sound = null; // Goal sound

    public void GotScore() {
        if(goal_sound != null) {
            GameObject sound = new GameObject("sound"); // Creates new GO
            sound.AddComponent<AudioSource>().PlayOneShot(goal_sound); // Add audio source to it and play sound
            Destroy(sound, goal_sound.length); // Destroy audio GO after sound done playing
        }

        if(add_score_text_prefab != null) {
            GameObject txt = Instantiate(add_score_text_prefab, transform.position, transform.rotation); // Creates add score text effect
            Destroy(txt, txt.GetComponentInChildren<Text>().GetComponent<Animation>().clip.length); // Destroy add score effect GO after animation done playing
            txt.transform.position = add_score_text_transform.position; // Assign position of effect
            txt.GetComponentInChildren<Text>().text = "+" + my_score.ToString(); // Set current hoop score to effect text
            txt.GetComponentInChildren<Text>().enabled = true; // Enable this ad score effect
            txt.GetComponentInChildren<Text>().GetComponent<Animation>().Play(); // Play animation of effect
        }

        if(particle_effect != null) {
            GameObject particles = Instantiate(particle_effect, add_score_text_transform.position, Quaternion.identity); // Play particle effect (particles will auto destroy (cos autodestroy enabled in sample prefab))
        }

        Basketball_game_manager.Instance?.AddScore(my_score); // Add score
        Basketball_game_manager.Instance?.CheckGameStatus(); // Check game status and add new ball or end game depends on current conditions
    }

}
