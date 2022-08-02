using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    Player player;
    Vector3 initPos;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        initPos = player.transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        other.transform.position = initPos;
    }
}
