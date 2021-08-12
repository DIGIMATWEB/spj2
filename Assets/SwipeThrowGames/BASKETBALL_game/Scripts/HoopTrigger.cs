using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// RASKALOF, v1.0, Script calculates is ball score a goal
public class HoopTrigger : MonoBehaviour {
    enum TRIGGER_TYPE { TOP, DOWN }

    [SerializeField] TRIGGER_TYPE trigger_type = TRIGGER_TYPE.DOWN;
    [SerializeField] string ball_tag = "Player";
    [SerializeField] HoopTrigger down_trigger = null; // If this is down trigger leave it null
    [HideInInspector] public bool top_hitted = false;

    void Start() {
        top_hitted = false; // Initialize
    }

    private void OnTriggerEnter(Collider other) {
        if(trigger_type == TRIGGER_TYPE.TOP) { // If this is top trigger
            if (other.tag == ball_tag) { // If ball enters
                down_trigger.top_hitted = true; // Send info to down trigger what this trigger is hitted by ball
                GetComponent<BoxCollider>().enabled = false; // Disable this collider
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if(trigger_type == TRIGGER_TYPE.DOWN) { // If this is down trigger
            if(other.tag == ball_tag) { // If ball exit this thigger
                if (top_hitted) GetComponentInParent<HoopScript>().GotScore(); // If top triggerwas hitted - It means what we score!
            }
        }
    }

    private void OnEnable() {
        GetComponent<BoxCollider>().enabled = true; // Initialize
        top_hitted = false; // Initialize
    }
}
