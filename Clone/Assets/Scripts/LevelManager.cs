﻿using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour{

    public GameObject currentCheckpoint;

    public GameObject playerPrefab;
    GameObject currentPlayer;
    GameObject oldCurrentPlayer;
    public int index = 0;


    public List<GameObject> currentPlayers = new List<GameObject>();
    public int maxNumberOfClones = 6;

    public CinemachineVirtualCamera vcam;

    [Space(10)]
    [Header("UI")]
    [Space(10)]

    public TextMeshProUGUI numOfClones;

    public static LevelManager instance;
    private void Awake() {
        if (instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
    }

    private void Update() {
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0) {
            index++;
            if (index > currentPlayers.Count) index = 1;
            SetPlayers();
        }
        if (Input.GetAxisRaw("Mouse ScrollWheel") < 0) {
            index--;
            if (index < 1) index = currentPlayers.Count;
            SetPlayers();
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            if (currentPlayers.Count > 1)
                RemoveSelectedPlayer();
        }
    }

    private void Start() {
        AddNewPlayer(currentCheckpoint.transform.position,true);

        numOfClones.SetText(currentPlayers.Count+"/"+ maxNumberOfClones);
    }

    public void AddNewPlayer(Vector3 pos, bool isFromSelectedPlayer) {
        if (isFromSelectedPlayer) {
            if (currentPlayers.Count < maxNumberOfClones) {
                GameObject newPlayer = Instantiate(playerPrefab, pos, Quaternion.identity);
                index++;
                newPlayer.name = "Player" + index;
                currentPlayers.Add(newPlayer);
                SetPlayers();
            }
        }       
    }

    void SetPlayers() {
        if (currentPlayer)
            oldCurrentPlayer = currentPlayer;
        if (oldCurrentPlayer)
            oldCurrentPlayer.GetComponent<PlayerController2D>().SetCurrentSelected(false);
        currentPlayer = currentPlayers[index - 1];
        currentPlayer.GetComponent<PlayerController2D>().SetCurrentSelected(true);
        vcam.Follow = currentPlayer.transform;
        numOfClones.SetText(currentPlayers.Count + "/" + maxNumberOfClones);
    }

    void RemoveSelectedPlayer() {
         
            currentPlayers.Remove(currentPlayers[index - 1]);
            Destroy(currentPlayer);
            index++;
            if (index > currentPlayers.Count) index = 1;
            SetPlayers();
              
    }
    public void RemovePlayer(GameObject player) {
        if(player == currentPlayer) {
            if (currentPlayers.Count > 1) {
                RemoveSelectedPlayer();
            } else {
                //GameOver
                Debug.Log("Game Over");
            }
        } else {
            currentPlayers.Remove(player);
            Destroy(player);
            index++;
            if (index > currentPlayers.Count) index = 1;
            SetPlayers();
        }
    }
}