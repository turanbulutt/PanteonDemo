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
        //getting rotation info of our object then rotating on Z axis 
        var rotationVector = transform.rotation.eulerAngles;
        if (isLeft)
            rotationVector.z += RotationSpeed;
        else
            rotationVector.z -= RotationSpeed;
        transform.rotation = Quaternion.Euler(rotationVector);
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player") || collider.gameObject.CompareTag("Opponent"))
        {

            //giving speed to player or enemy 
            float speed;
            if (isLeft)
                speed = -RotationSpeed * Time.deltaTime * 500;
            else
                speed = RotationSpeed * Time.deltaTime * 500;
            collider.GetComponent<Rigidbody>().velocity = new Vector2(speed, 0);


        }
    }
}
