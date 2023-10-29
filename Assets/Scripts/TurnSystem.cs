using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    //Singleton
    public static TurnSystem Instance { get; private set; }

    //Event
    public event EventHandler OnTurnChanged;

    private bool isPlayerTurn = true;

    private int turnNumber = 1;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void NextTurn()
    {
        turnNumber++;
        isPlayerTurn=!isPlayerTurn;

        OnTurnChanged?.Invoke(this, EventArgs.Empty);
    }


    public int GetTurnNumber()
    {
        return turnNumber;
    }

    public bool IsPlayerTurn()
    {
        return isPlayerTurn;
    }
}
