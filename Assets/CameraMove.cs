using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] GameObject Character;
    Vector3 different;
    private void Awake()
    {
        SetDifferent();
    }

    void Update()
    {
        MoveCamera();
    }

    private void SetDifferent()
    {
        if (Character != null)
        {
            different = Character.transform.position - transform.position;
        }
    }

    private void MoveCamera()
    {
        if (Character != null)
        {
            Vector3 newPos = Character.transform.position - different;
            transform.position = newPos;
        }
    }
}
