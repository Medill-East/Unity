﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManeger : MonoBehaviour
{

    private Maze mazeInstance;
    private Player playerInstance;

    public Maze mazePrefab;
    public Player playerPrefab;
    
    // Use this for initialization
    void Start()
    {
        StartCoroutine( BeginGame());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RestartGame();
        }
    }

    private IEnumerator BeginGame()
    {
        mazeInstance = Instantiate(mazePrefab) as Maze;
        yield return StartCoroutine(mazeInstance.Generate());
        playerInstance = Instantiate(playerPrefab) as Player;
        playerInstance.SetLocation(mazeInstance.GetCell(mazeInstance.RandomCoordinates));
    }

    private void RestartGame()
    {
        StopAllCoroutines();
        Destroy(mazeInstance.gameObject);
        if (playerInstance != null)
        {
            Destroy(playerInstance.gameObject);
        }
        StartCoroutine(BeginGame());
    }
}
