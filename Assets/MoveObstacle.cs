using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacle : MonoBehaviour
{
    [Header("General")]
    [SerializeField] Collider PlatformCollider;
    [SerializeField] float MoveSpeed;
    [SerializeField] bool toRight;

    [Header("Half Donut")]
    [SerializeField] bool isDonut;


    float MaxX, MinX;
    void Start()
    {
        GetBoundries();
        if (isDonut)
            StartCoroutine(ChangeDonutDirection());
    }

    void Update()
    {

        if (toRight)
            Move(MaxX);
        else
            Move(MinX);
        if (!isDonut)
            ChangeDirection();
    }

    private void ChangeDirection()
    {
        if (transform.position.x == MaxX)
            toRight = false;
        else if (transform.position.x == MinX)
            toRight = true;
    }

    private void Move(float Xloc)
    {
        var targetPosition = new Vector3(Xloc, transform.position.y, transform.position.z);
        var movementThisFrame = MoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementThisFrame);
    }

    IEnumerator ChangeDonutDirection()
    {
        while (true)
        {
            toRight = !toRight;
            yield return new WaitForSeconds(1);
        }

    }


    private void GetBoundries()
    {
        MaxX = PlatformCollider.bounds.max.x;
        MinX = PlatformCollider.bounds.min.x;
    }
}
