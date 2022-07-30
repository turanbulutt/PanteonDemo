using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CalculateRanking : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    GameObject[] opponents;
    GameObject player;
    bool isEnd=false;

    private void Awake()
    {
        //getting all enemies and player
        opponents = GameObject.FindGameObjectsWithTag("Opponent");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(!isEnd)
            GetRanking();
    }

    private void GetRanking()
    {
        int ranking = 1;
        float playerPosZ = player.transform.position.z;

        //checking if any enemy is front of player if so increasing ranking
        foreach(GameObject opponent in opponents)
        {
            float opponentPosZ = opponent.transform.position.z;
            if ( opponentPosZ> playerPosZ)
                ranking++;
        }
        ChangeText(ranking);
    }

    private void ChangeText(int ranking)
    {
        string newText = ("Rank: " + ranking.ToString());
        text.SetText(newText);
    }

    public void ChangeIsEnd(bool newCond)
    {
        isEnd = newCond;
    }
}
