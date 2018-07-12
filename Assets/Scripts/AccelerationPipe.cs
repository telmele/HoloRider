using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationPipe : MonoBehaviour {

    public float Force;

    private GameObject gm;
    private void Start() {
        gm = GameObject.FindGameObjectWithTag("GameController");
    }
    private void OnCollisionStay(Collision other) {
        Debug.Log("Stay");
        other.rigidbody.AddForce(gm.GetComponent<GestureManager>().Direction() * Force, ForceMode.Acceleration);
    }
}
