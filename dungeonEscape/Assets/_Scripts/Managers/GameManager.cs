using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< Updated upstream
=======
using UnityEngine.Events;
using System.IO;
>>>>>>> Stashed changes

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState GameState;
    public static int heroTurn;

<<<<<<< Updated upstream
=======
    private int Astar_total_moves;
    private int Minimax_total_moves;
    public int RL_total_moves;

    public int Astar_total_deaths;
    public int Minimax_total_deaths;
    public int RL_total_deaths;

    public int Astar_total_kills;
    public int Minimax_total_kills;
    public int RL_total_kills;

    public int win;
    public int loss;




    

>>>>>>> Stashed changes
    void Awake()
    {
        Instance = this;
        heroTurn = 1;
    }

    void Start()
    {
        ChangeState(GameState.GenerateGrid);
        Astar_total_moves = 0;
        Minimax_total_moves = 0;
        RL_total_moves = 0;
        Astar_total_deaths = 0;
        Minimax_total_deaths = 0;
        RL_total_deaths = 0;
        Astar_total_kills = 0;
        Minimax_total_kills = 0;
        RL_total_kills = 0;
        win = 0;
        loss = 0;
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
<<<<<<< Updated upstream
                //UnitManager.Instance.PlayerMove();
                break;
            case GameState.Heroes2Turn:
                UnitManager.Instance.PlayerMove();
=======
                print("Hero 1 Turn");
                Invoke("HeroMoveset", 0);
                Astar_total_moves++;
                break;
            case GameState.Heroes2Turn:
                print("Hero 2 Turn");
                Invoke("Hero2Moveset", 0);
                Minimax_total_moves++;
>>>>>>> Stashed changes
                break;
            case GameState.Heroes3Turn:
                UnitManager.Instance.PlayerMove();
                break;
            case GameState.EnemiesTurn:
<<<<<<< Updated upstream
                UnitManager.Instance.EnemyMove();
=======
                Invoke("EnemyMoveset", 0.6f);
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
=======

    public void HeroMoveset() {
        UnitManager.Instance.AStarSearch();
        UnitManager.Instance.HeroMove();
    }

    public void Hero2Moveset() {
        MiniMax.PlayerMove();
    }

    public void Hero3Moveset() {
        UnitManager.Instance.Player_Move();
        //myAgent.RequestDecision();
        //myAcedemy.EnvironmentStep();
    }

    public void EnemyMoveset() {
        UnitManager.Instance.EnemyMove();
    }

    public void WriteCSV() {
        TextWriter tw = new StreamWriter("/Volumes/Main/Unity/1s DungeonEscape/newD/dungeonEscapeAMR.csv", true);
        tw.WriteLine(Astar_total_moves + "," + Minimax_total_moves + "," + RL_total_moves + ","  
        + Astar_total_deaths + "," + Minimax_total_deaths + "," + RL_total_deaths + ","
        + win + "," + loss + ","
        + Astar_total_kills + "," + Minimax_total_kills + "," + RL_total_kills);
        tw.Close();
    }
>>>>>>> Stashed changes
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