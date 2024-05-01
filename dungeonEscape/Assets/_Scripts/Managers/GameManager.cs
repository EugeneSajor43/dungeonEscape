using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState GameState;
    public static int heroTurn;

    private IEnumerator moveCoroutine;

    void Awake()
    {
        Instance = this;
        heroTurn = 1;
    }

    public void Start()
    {
        ChangeState(GameState.GenerateGrid);
        moveCoroutine = MoveCoroutine();
        StartCoroutine(moveCoroutine);
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
                UnitManager.Instance.PlayerMove();
                break;
            case GameState.Heroes2Turn:
                UnitManager.Instance.PlayerMove();
                break;
            case GameState.Heroes3Turn:
                //UnitManager.Instance.PlayerMove();
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


    private IEnumerator MoveCoroutine()
    {
        while (true)
        {
            // Call PlayerMove() function here
            UnitManager.Instance.PlayerMove();

            // Wait for 50 milliseconds
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void OnDestroy()
    {
        // Stop the coroutine when the object is destroyed
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
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