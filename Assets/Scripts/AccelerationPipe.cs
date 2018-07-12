using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationPipe : MonoBehaviour {

    public float Force;
    private GameObject gm;
    private Vector3 start;
    private Vector3 end;

    void setStart(Vector3 startoum) {
        start = startoum;
    }

    void setEnd(Vector3 endoum) {
        end = endoum;
    }
    
    private void Start() {
        gm = GameObject.FindGameObjectWithTag("GameController");
    }
    
    private void OnCollisionStay(Collision other) {
        Debug.Log("Stay");
        Vector3 heading = end - start;
        float distance = heading.magnitude;
        Vector3 direction = heading / distance;
        other.rigidbody.AddForce(direction * Force, ForceMode.Acceleration);
    }
}
