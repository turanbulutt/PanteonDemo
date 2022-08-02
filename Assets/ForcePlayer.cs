using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcePlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var directionVector = Vector3.Normalize(other.transform.position - transform.position);
        Debug.Log(directionVector);
        other.GetComponent<Rigidbody>().AddForce(directionVector * 10, ForceMode.Impulse);
    }
}
