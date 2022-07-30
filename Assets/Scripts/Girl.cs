using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Girl : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject endPoint;
    [SerializeField] Collider PlatformCollider;

    [Header("Path Finding")]
    [SerializeField] float targetVelocity = 5f;
    [SerializeField] float numberOfRays = 21;
    [SerializeField] float angle = 90;
    [SerializeField] float RayRange = 5f;
    [SerializeField] GameObject wayPoint;

    Animator anim;
    Vector3 initPos,goal;
    float MaxX, MinX,size;
    bool stop = false;
    void Start()
    {
        anim = GetComponent<Animator>();
        goal = wayPoint.transform.position;
        initPos = transform.position;
        size = GetComponent<BoxCollider>().size.y;
        SetBoundries();
    }

    // Update is called once per frame
    void Update()
    {
        if(!stop)
        {
            Move();
            CheckBoundries();
            CheckGoal();
            anim.SetBool("isRunning", true);
        }
        else
            anim.SetBool("isRunning", false);

    }

    private void CheckGoal()
    {
        if (transform.position == goal)
            goal = endPoint.transform.position;
        if (transform.position == endPoint.transform.position)
        {
            /*gameObject.SetActive(false);
            Destroy(gameObject);*/
            stop = true;
        }
            
    }

    private void SetBoundries()
    {
        MaxX = PlatformCollider.bounds.max.x;
        MinX = PlatformCollider.bounds.min.x;
    }

    private void CheckBoundries()
    {
        if (transform.position.x > MaxX)
            transform.position = new Vector3( MaxX,transform.position.y,transform.position.z);
        else if(transform.position.x<MinX)
            transform.position = new Vector3(MinX, transform.position.y, transform.position.z);

    }

    private void Move()
    {
        var deltaPos = Vector3.zero;
        for (float j = 0.2f; j < size; j += 0.4f)
        {
            for (int i = 0; i < numberOfRays; i++)
            {
                //creating direction for each ray , and calculating angle for rays.
                var rotationMod = Quaternion.AngleAxis((i / (numberOfRays - 1)) * 2 * angle - angle, transform.up);
                var direction = rotationMod * Vector3.forward;
                Vector3 pos = transform.position;
                pos.y += j;

                var ray = new Ray(pos, direction);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, RayRange))//means there is an object in our ways we need to run away
                {
                    RunAway(direction);
                }
                else //path is clear
                {
                    GoToGoal();
                }

            }
        }
    }

    private void GoToGoal()
    {
        transform.position = Vector3.MoveTowards(transform.position, goal, targetVelocity * Time.deltaTime);
    }

    private void RunAway(Vector3 direction)
    {
        transform.position -= (1 / numberOfRays) * targetVelocity * direction;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            transform.position = initPos;
        }
    }

    /*private void OnDrawGizmos()
    {
        for (float j = 0; j < size; j += 0.4f)
         {
            for (int i = 0; i < numberOfRays; i++)
            {
                //var rotation = this.transform.rotation;
                var rotationMod = Quaternion.AngleAxis((i / (numberOfRays - 1)) * 2 * angle - angle, transform.up);
                var direction = rotationMod * Vector3.forward;
                Vector3 pos = transform.position;
                pos.y += j;
                Gizmos.DrawRay(pos, direction * RayRange);

            }
        }
    }*/
}
