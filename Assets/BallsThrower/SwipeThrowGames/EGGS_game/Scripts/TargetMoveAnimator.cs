using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// RASKALOF, v1.0, Script animates targets
public class TargetMoveAnimator : MonoBehaviour {
    [SerializeField] AnimationClass[] procedural_animations = null; // Array of animations for each axis
    [SerializeField] bool randomize_animation = false; // Should randomize animation every time target shows?
    byte current_animation;
    Vector3 start_pos;
    bool end;

    private void Start() {
        start_pos = transform.position; // Store start pos
    }

    private void OnEnable() {
        current_animation = 0; // When target show up - animation 0
        if (randomize_animation) current_animation = (byte)Random.Range(0, procedural_animations.Length); // If need to randomize - randomize current animation
        end = false; // Target just showed
    }

    void Update() {
        if (!end) { // While target visible
            transform.position = new Vector3(start_pos.x + procedural_animations[current_animation].x.Evaluate((Time.time % procedural_animations[current_animation].x.length)), 
                start_pos.y + procedural_animations[current_animation].y.Evaluate((Time.time % procedural_animations[current_animation].y.length)), 
                start_pos.z + procedural_animations[current_animation].z.Evaluate((Time.time % procedural_animations[current_animation].z.length))); // Animate position depends on animation curves
        }
    }

    private void OnDisable() {
        end = true; // Flag to stop position changing
        transform.position = start_pos; // Return object coordinates to origin
    }

}
