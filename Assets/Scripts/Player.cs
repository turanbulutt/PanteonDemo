using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float MoveSpeed = 7f;
    [SerializeField] float movement = 5f;
    [SerializeField] float rotationSpeed = 20f;

    Animator anim;
    CalculateRanking ranking;
    Vector3 initPos;
    Paint paint;
    bool isMove = true;
    Vector2 rawInput;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        ranking = FindObjectOfType<CalculateRanking>();
        initPos = transform.position;
        paint=FindObjectOfType<Paint>();
    }
    void Update()
    {
        if(isMove)
            Move();
        else
            anim.SetBool("isRunning", false);
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    void Move()
    {
        Vector3 delta = rawInput * Time.deltaTime * movement;
        Vector3 newPos = new Vector3(transform.position.x + delta.x, transform.position.y, transform.position.z + MoveSpeed*Time.deltaTime);
        if (newPos != transform.position)
            anim.SetBool("isRunning", true);
        else
            anim.SetBool("isRunning", false);
        transform.position = newPos;
        ChangeDirection();
    }

    private void ChangeDirection()
    {
        Vector3 movDirection = new Vector3(rawInput.x, 0, 1);
        if (movDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("End"))
        {
            isMove = false;
            transform.position = other.GetComponent<Collider>().bounds.center;
            paint.SetEnd(true);
            ranking.ChangeIsEnd(true);
        }
        if(other.CompareTag("Obstacle"))
        {
            transform.position = initPos;
        }
    }

}
