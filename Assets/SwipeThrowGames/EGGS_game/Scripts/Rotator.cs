using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// RASKALOF, v1.0, Script rotates ball
public class Rotator : MonoBehaviour {
    Vector3 rotation_vector;
    bool rotate;
    Rigidbody m_Rigidbody;
    
    public void StartRotation(Vector3 rot_vect) {
        m_Rigidbody = GetComponent<Rigidbody>();
        Vector3 newrot = new Vector3(Mathf.Abs(rot_vect.x) * 5, 0, 0);
        rotation_vector = newrot;
        rotate = true;
    }

    void Update() {
        if(rotate && m_Rigidbody != null) {
            Quaternion deltaRotation = Quaternion.Euler(rotation_vector * Time.deltaTime);
            m_Rigidbody.MoveRotation(m_Rigidbody.rotation * deltaRotation);
        }
    }
}
