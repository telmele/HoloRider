using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationPipe : MonoBehaviour {

    public float Force;
    
    private void OnCollisionStay(Collision other) {
        Debug.Log("Stay");
        other.rigidbody.AddForce(Vector3.forward * Force, ForceMode.Acceleration);
    }
}
