using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState GameState;
    public static int heroTurn;

    void Awake()
    {
        Instance = this;
        heroTurn = 1;
    }

    void Start()
    {
        ChangeState(GameState.GenerateGrid);
    }

    public void Hero1_move_call()
    {
        UnitManager.Instance.Hero1Move();
    }

    public void ChangeState(GameState newState)
    {
        GameState = newState;
        switch (newState)
        {
            case GameState.GenerateGrid:
                GridManager.Instance.GenerateGrid();
                break;
            case GameState.SpawnHeroes:
                UnitManager.Instance.SpawnHeroes();
                break;
            case GameState.SpawnEnemies:
                UnitManager.Instance.SpawnEnemies();
                break;
            case GameState.HeroesTurn:
                //UnitManager.Instance.Hero1Move();
                Invoke("Hero1_move_call", 1);
                break;
            case GameState.Heroes2Turn:
                UnitManager.Instance.Hero2Move();
                break;
            case GameState.Heroes3Turn:
                UnitManager.Instance.Hero3Move();
                break;
            case GameState.EnemiesTurn:
                UnitManager.Instance.EnemyMove();
                break;
            case GameState.LostGame:
                UnitManager.Instance.ResetGame();
                GameManager.Instance.ChangeState(GameState.GenerateGrid);
                break;    
            case GameState.WonGame:
                UnitManager.Instance.ResetGame();
                GameManager.Instance.ChangeState(GameState.GenerateGrid);
                break; 
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }
}

public enum GameState
{
    GenerateGrid = 0,
    SpawnHeroes = 1,
    SpawnEnemies = 2,
    HeroesTurn = 3,
    Heroes2Turn = 4,
    Heroes3Turn = 5,
    EnemiesTurn = 6,
    LostGame = 7,
    WonGame = 8
}