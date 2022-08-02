using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] float RotationSpeed = 5f;
    void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        var rotationVector = transform.rotation.eulerAngles;
        rotationVector.y -= RotationSpeed;
        transform.rotation = Quaternion.Euler(rotationVector);
    }

    public float GetRotationSpeed()
    {
        return RotationSpeed;
    }
}
