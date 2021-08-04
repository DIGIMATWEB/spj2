using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// RASKALOF, v1.0, Script plays sound when ball hit something
[RequireComponent(typeof(AudioSource))]
public class Ball_hit_sound : MonoBehaviour {
    AudioSource a_s = null;
    Rigidbody r_b = null;
    [SerializeField] AudioClip[] hit_sounds = null;
    float last_hit_time;

    private void OnEnable() {
        a_s = GetComponent<AudioSource>(); // Linking component to get acess
        r_b = GetComponent<Rigidbody>(); // Linking component to get acess
    }

    private void OnCollisionEnter(Collision collision) {
        a_s.volume = Mathf.Clamp(-r_b.velocity.normalized.z, 0.1f, 1f); // Calculate AS volume depends on hit velocity

        if (Time.time - last_hit_time > 0.1) a_s.PlayOneShot(hit_sounds[Random.Range(0, hit_sounds.Length)]); // Prevent sound hihat trap effect and play sound
        last_hit_time = Time.time; // Calculate time of last hit for preventing trap effect
    }


}
