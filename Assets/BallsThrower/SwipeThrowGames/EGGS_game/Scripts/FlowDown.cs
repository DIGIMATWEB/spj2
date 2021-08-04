using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// RASKALOF, v1.0, Script used for flowdown effect
public class FlowDown : MonoBehaviour {
    [SerializeField] AnimationCurve animation_curve = null;
    [SerializeField] AxisClass axis = AxisClass.Z;

    void Awake() {
        StartCoroutine(flowdown());
    }

    IEnumerator flowdown() {
        float timePassed = 0;
        while(timePassed < animation_curve.keys[animation_curve.keys.Length - 1].time) {
            Vector3 calc_vect = new Vector3();
            if (axis == AxisClass.X) calc_vect.x += animation_curve.Evaluate(timePassed);
            if (axis == AxisClass.Y) calc_vect.y += animation_curve.Evaluate(timePassed);
            if (axis == AxisClass.Z) calc_vect.z += animation_curve.Evaluate(timePassed);
            transform.localScale += calc_vect;
            timePassed += Time.deltaTime;
            yield return null;
        }
    }
}
