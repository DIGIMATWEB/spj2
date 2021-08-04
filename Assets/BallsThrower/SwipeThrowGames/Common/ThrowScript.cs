using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// RASKALOF, v1.0, Script for throwing something

public class ThrowScript : MonoBehaviour {
    public enum PLATFORM { PC, MOBILE };
    [SerializeField] PLATFORM platform = PLATFORM.PC; // Controll type

	[SerializeField] float force_xy = 1f; // Force applied to ball by X and Y axises
	[SerializeField] float force_z = 50f; // Force applied to ball by Z axis

    [SerializeField] bool auto_destroy = true; // If needed for some reasons destroy throwed object after some time (fe missed etc)
    [SerializeField] int auto_destroy_timer = 5; // After this time object will be destroyed

    // Private Vars
    Vector2 pointer_start_pos, pointer_end_pos, pointer_direction; // Positions
	float pointer_start_time, pointer_end_time, pointer_hold_duration; // Times
    bool drag_active; // Defines is we throwing ball?
	Rigidbody my_rigidbody; // Link to rb to optimize calcs
    Vector3 cursor_pos = new Vector3(); // Position of touch or mouse pointer on screen


    void Start() {
		my_rigidbody = GetComponent<Rigidbody>(); // Get link to rb
        my_rigidbody.isKinematic = true; // Force kinematic status to prevent object falling
    }

	void Update () {
        if(platform == PLATFORM.PC) {
            cursor_pos = Camera.main.ScreenToViewportPoint(Input.mousePosition) * 1000; // Instead of getting pixels, we are getting viewport coordinates which is resolution independent
        } else {
            if(Input.touchCount > 0) cursor_pos = Camera.main.ScreenToViewportPoint(Input.GetTouch(0).position) * 1000; // Instead of getting pixels, we are getting viewport coordinates which is resolution independent
        }

        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0)) { // This is actions when finger/cursor hit screen
            OnScreen(cursor_pos);
        }
        if((Input.touchCount > 0 && (Input.GetTouch(0).phase == TouchPhase.Stationary || Input.GetTouch(0).phase == TouchPhase.Moved)) || Input.GetMouseButton(0)) { // This is actions when finger/cursor pressed on screen
            OnHold(cursor_pos);
        }
		if (((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0)) && drag_active) { // This is actions when finger/cursor get out from screen
            OffScreen(cursor_pos);
        }
	}

    void OnScreen(Vector3 cursor_pos) {
        drag_active = true; // Activates drag flag
        pointer_start_time = Time.time; // Writing time when we hit screen
        pointer_start_pos = cursor_pos; // Writing position where we hit screen
    }

    void OnHold(Vector3 cursor_pos) {
        if(drag_active) { // If we throwing something
            if((Time.time - pointer_start_time) > 0.3f) { // If finger holding something to long
                OffScreen(cursor_pos); // Throw ball automatically
            }
        }
    }

    void OffScreen(Vector3 cursor_pos) {
        drag_active = false; // Deactivate drag flag
        pointer_end_time = Time.time; // Writing time when we release finger
        pointer_end_pos = cursor_pos; // Writing position where we release finger
        pointer_direction = pointer_start_pos - pointer_end_pos; // Calculating throw direction vector
        pointer_hold_duration = pointer_end_time - pointer_start_time; // Calc how long we holding finger
        if(pointer_hold_duration > 0.05f) { // Protect from infinite speed throw
            my_rigidbody.isKinematic = false; // Object now affected by gravity and appliend forces
            transform.SetParent(null); // Dechild this object
            Vector3 vector = new Vector3(-pointer_direction.x * force_xy, -pointer_direction.y * force_xy, force_z / pointer_hold_duration); // Calc throw vector
            my_rigidbody.AddForce(vector, ForceMode.Impulse); // Apply force to ball
            GetComponent<Rotator>()?.StartRotation(vector); // Add visual rotation to object
            EggsGameManager.Instance?.Throw(); // Decrease amounts of throws
            if(auto_destroy) Destroy(gameObject, auto_destroy_timer); // Destroy this object after "auto_destroy_timer" secs
            Destroy(this); // Destroy this script to prevent controlling etc.
        }
    }

}
