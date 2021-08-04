using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// RASKALOF, v1.0, Script used for creating crack hit effect
public class Cracker : MonoBehaviour {
    [SerializeField] GameObject[] smack_particles = null; // Particle effect (particle bang)
    [SerializeField] GameObject[] smack_decal = null; // Decal for instantiate on wall
    [SerializeField] AudioClip[] smack_sound = null; // Hit sound
    [SerializeField] float sound_volume = 1f; // Volume controller
    [SerializeField] string tag_to_crack = "Finish"; // Tag defines what should be counted as obstacle to crack
    [SerializeField] bool random_rotation = false; // Option to randomize rotation
    [SerializeField] bool hitted = false; // Defines is ball hit target

    public void SetHittedStatus() { // Called from outside to set what ball hitted target
        hitted = true;
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.transform.tag == tag_to_crack) {
            if(smack_decal.Length > 0) {
                GameObject smack = Instantiate(smack_decal[Random.Range(0, smack_decal.Length)], collision.GetContact(0).point, collision.transform.rotation); // Create decal at hit point
                if(random_rotation) {
                    float randomRot = Random.Range(0f, 360f);
                    smack.transform.Rotate(smack.transform.forward * randomRot); // Add random rotation in case of this option is enabled
                }
                smack.transform.SetParent(collision.transform); // Parenting decal to hit surface
            }
            if(smack_particles.Length > 0) Instantiate(smack_particles[Random.Range(0, smack_particles.Length)], collision.GetContact(0).point, collision.transform.rotation); // Instantiate particle effect

            if(smack_sound.Length > 0) {
                GameObject sound = new GameObject("sound");
                byte i = (byte)Random.Range(0, smack_sound.Length);
                sound.AddComponent<AudioSource>().PlayOneShot(smack_sound[i]);
                sound.GetComponent<AudioSource>().volume = sound_volume;
                Destroy(sound, smack_sound[i].length); // Creates new object, add to it audio source, play sound, destroy this object after playing is done
            }
            if(!hitted) EggsGameManager.Instance?.AddFail(); // If eggs game manager founded and no target hitted - this is fail
            Destroy(gameObject); // Destroy this ball after all done
        }
    }
}
