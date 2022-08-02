using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlatform : MonoBehaviour
{
    [SerializeField] bool isLeft;
    [SerializeField] float RotationSpeed;

    void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        var rotationVector = transform.rotation.eulerAngles;
        if (isLeft)
            rotationVector.z += RotationSpeed;
        else
            rotationVector.z -= RotationSpeed;
        transform.rotation = Quaternion.Euler(rotationVector);
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            float speed;
            if (isLeft)
                speed = RotationSpeed * Time.deltaTime * -1000;
            else
                speed = RotationSpeed * Time.deltaTime * 1000;
            collider.GetComponent<Rigidbody>().velocity = new Vector2(speed, 0);
            StayStand(collider);

        }

    }

    private static void StayStand(Collider collider)
    {
        var rotationVector = collider.transform.rotation.eulerAngles;
        rotationVector.z = 0;
        collider.transform.rotation = Quaternion.Euler(rotationVector); ;
    }
}
