using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float MoveSpeed = 3f;
    [SerializeField] float movement = 5f;

    Paint paint;
    bool isMove = true;
    Vector2 rawInput;

    private void Awake()
    {
        paint = FindObjectOfType<Paint>();
    }
    void Update()
    {
        if (isMove)
            Move();
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    void Move()
    {
        Vector3 delta = rawInput * Time.deltaTime * movement;
        transform.position = new Vector3(transform.position.x + delta.x, transform.position.y, transform.position.z + MoveSpeed * Time.deltaTime);
    }

    public void SetMoveSpeed(float newSpeed)
    {
        MoveSpeed = newSpeed;
    }

    public float GetMoveSpeed()
    {
        return MoveSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("End"))
        {
            isMove = false;
            transform.position = other.GetComponent<Collider>().bounds.center;
            paint.SetEnd(true);
        }
    }
}
