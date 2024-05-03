using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState GameState;
    public static int heroTurn;

    public Hero3Agent myAgent;

    public academy myAcedemy;

    

    void Awake()
    {
        Instance = this;
        heroTurn = 1;
        
    }

    void Start()
    {
        ChangeState(GameState.GenerateGrid);
        myAgent = new Hero3Agent();
        myAcedemy = new academy();
    }
    public void Hero2Moves()
    {
        UnitManager.Instance.Player_Move();
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
                Invoke("HeroMoveset", 0);
                break;
            case GameState.Heroes2Turn:
                Invoke("Hero2Moveset", 0);
                break;
            case GameState.Heroes3Turn:
                //myAgent.RequestDecision();
                Invoke("Hero3Moveset", 0);
                break;
            case GameState.EnemiesTurn:
                Invoke("EnemyMoveset", 1);
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

    public void HeroMoveset() {
        UnitManager.Instance.AStarSearch();
        UnitManager.Instance.HeroMove();
    }

    public void Hero2Moveset() {
        MiniMax.PlayerMove();
    }

    public void Hero3Moveset() {
        //UnitManager.Instance.Player_Move();
        myAgent.RequestDecision();
        myAcedemy.EnvironmentStep();
    }

    public void EnemyMoveset() {
        UnitManager.Instance.EnemyMove();
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