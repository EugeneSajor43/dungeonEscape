using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using System;
using UnityEngine.Events;


public class Hero3Agent : Agent
{

    [SerializeField] private Transform targetEnemy;

    [SerializeField] private Transform targetExit;

    private const int width = 16;
    private const int height = 9;

    private int[,] EnemyGrid;
    private int[,] ExitGrid;

    //public VoidEventChannelSO RequestMoveChannel;

    private bool is_UP_valid = false;
    private bool is_DOWN_valid = false;
    private bool is_RIGHT_valid = false;
    private bool is_LEFT_valid = false;

    //BaseUnit Current_Hero = UnitManager.Instance.SelectedHeroes[2];
    

    // Start is called before the first frame update


    public override void OnEpisodeBegin()
    {
        Create_One_Hot_Grid();
        Create_One_Hot_Grid2();

    }


    public override void Initialize()
    {
        
    }

    private void Start()
    {
        print("Start");
        Academy.Instance.AutomaticSteppingEnabled = false;
    }

    private void FixedUpdate()
    {
        Academy.Instance.EnvironmentStep();
        if(GameManager.Instance.GameState == GameState.Heroes3Turn)
        {
            MakeMove();
        }
    }


    void OnDestroy()
    {
            AddReward(-2f);
            EndEpisode();
            UnitManager.Instance.ResetGame();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        //print("CollectObservations");

        BaseUnit Current_Hero = UnitManager.Instance.SelectedHeroes[2];

        sensor.AddObservation(Current_Hero.transform.position);
        //sensor.AddOneHotObservation(EnemyGrid);

        if(UnitManager.Instance.SelectedEnemy != null)
        {
            sensor.AddObservation(targetEnemy.transform.position);
        }

        if(UnitManager.Instance.CanEscape)
        {
            //sensor.AddObservation(UnitManager.Instance.EscapeExit);
            sensor.AddObservation(targetExit.transform.position);
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                sensor.AddOneHotObservation(EnemyGrid[x, y], 1);
            }
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                sensor.AddOneHotObservation(ExitGrid[x, y], 1);
            }
        }
    }

    public override void WriteDiscreteActionMask(IDiscreteActionMask actionMask)
    {
        //actionMask.SetActionEnabled(branch, actionIndex, isEnabled);
        
        //BaseUnit Current_Hero = UnitManager.Instance.SelectedHeroes[2];

        //print("WriteDiscreteActionMask");
        Vector3 dummy_location = new Vector3(-1f, -1f, -1f);
        Vector3 hero1_Location = (UnitManager.Instance.SelectedHeroes[0] != null) ? (UnitManager.Instance.SelectedHeroes[0].transform.position) : (dummy_location);
        Vector3 hero2_Location = (UnitManager.Instance.SelectedHeroes[1] != null) ? (UnitManager.Instance.SelectedHeroes[1].transform.position) : (dummy_location);

        float currentY = this.transform.position.y;
        float currentX = this.transform.position.x;
        //print($"{currentX}, {currentY}");
        //print("MASK CALLED");

        is_UP_valid = false;
        is_DOWN_valid = false;
        is_RIGHT_valid = false;
        is_LEFT_valid = false;

        float Y_up = currentY + 1f;
        float Y_down = currentY - 1f;
        float X_right = currentX + 1f;
        float X_left = currentX - 1f;

        Vector3 UP_pos = new Vector3(currentX, Y_up, 0f);
        Vector3 DOWN_pos = new Vector3(currentX, Y_down, 0f);
        Vector3 RIGHT_pos = new Vector3(X_right, currentY, 0f);
        Vector3 LEFT_pos = new Vector3(X_left, currentY, 0f);

        //Tile tile_UP = GridManager.Instance.GetTileAtPosition(new Vector2((int)currentX, (int)Y_up));
        //Tile tile_DOWN = GridManager.Instance.GetTileAtPosition(new Vector2((int)currentX, (int)Y_down));
        //Tile tile_RIGHT = GridManager.Instance.GetTileAtPosition(new Vector2((int)X_right, (int)currentY));
        //Tile tile_LEFT = GridManager.Instance.GetTileAtPosition(new Vector2((int)X_left, (int)currentY));

        //bool tile_UP_valid = GridManager.Instance.GetTileAtPosition(new Vector2((int)currentX, (int)Y_up))._isWalkable;
        //bool tile_DOWN_valid = GridManager.Instance.GetTileAtPosition(new Vector2((int)currentX, (int)Y_down))._isWalkable;
        //bool tile_RIGHT_valid = GridManager.Instance.GetTileAtPosition(new Vector2((int)X_right, (int)currentY))._isWalkable;
        //bool tile_LEFT_valid = GridManager.Instance.GetTileAtPosition(new Vector2((int)X_left, (int)currentY))._isWalkable;

        
        //(tile_UP.TileName != "Mountain") &&
        //(tile_DOWN.TileName != "Mountain") &&
        //(tile_RIGHT.TileName != "Mountain") &&
        //(tile_LEFT.TileName != "Mountain") &&
        

        if((Y_up <= 8) && (UP_pos != hero1_Location) && (UP_pos != hero2_Location) && (Y_up >= 0))
        {
            if(GridManager.Instance.GetTileAtPosition(new Vector2((int)currentX, (int)Y_up))._isWalkable)
            {
                is_UP_valid = true;
            }
        }
        if((Y_down <= 8) && (DOWN_pos != hero1_Location) && (DOWN_pos != hero2_Location) &&  (Y_down >= 0))
        {
            if(GridManager.Instance.GetTileAtPosition(new Vector2((int)currentX, (int)Y_down))._isWalkable)
            {
                is_DOWN_valid = true;
            }
        }
        if((X_right <= 15) && (RIGHT_pos != hero1_Location) && (RIGHT_pos != hero2_Location) &&  (X_right >= 0))
        {
            if(GridManager.Instance.GetTileAtPosition(new Vector2((int)X_right, (int)currentY))._isWalkable)
            {
                is_RIGHT_valid = true;
            }
        }
        if((X_left <= 15) && (LEFT_pos != hero1_Location) && (LEFT_pos != hero2_Location) &&  (X_left >= 0))
        {
            if(GridManager.Instance.GetTileAtPosition(new Vector2((int)X_left, (int)currentY))._isWalkable)
            {
                is_LEFT_valid = true;
            }
        }

        if(!is_UP_valid)
        {
            //print("up false");
            actionMask.SetActionEnabled(0, 0, false);
        }
        if(!is_DOWN_valid)
        {
            //print("down false");
            actionMask.SetActionEnabled(0, 1, false);
        }
        if(!is_RIGHT_valid)
        {
            //print("right false");
            actionMask.SetActionEnabled(0, 2, false);
        }
        if(!is_LEFT_valid)
        {
            //print("left false");
            actionMask.SetActionEnabled(0, 3, false);
        }

    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        //print("OnActionReceived");
        int direction = actions.DiscreteActions[0];


        BaseUnit Current_Hero = UnitManager.Instance.SelectedHeroes[2];

        if((direction == 0))
        {
            Current_Hero.transform.position += new Vector3(0f, 1f, 0f);
        }
        if((direction == 1))
        {
            Current_Hero.transform.position += new Vector3(0f, -1f, 0f);
        }
        if((direction == 2))
        {
            Current_Hero.transform.position += new Vector3(1f, 0f, 0f);
        }
        if((direction == 3))
        {
            Current_Hero.transform.position += new Vector3(-1f, 0f, 0f);
        }
        if((direction == 4))
        {
            this.transform.position = this.transform.position;
        }
        
        if(UnitManager.Instance.SelectedEnemy != null)
        {
            if(UnitManager.Instance.SelectedEnemy.transform.position == Current_Hero.transform.position) 
            {
                AddReward(10f);
                print("Player kills");
                Destroy(UnitManager.Instance.SelectedEnemy.gameObject);
                UnitManager.Instance.CanEscape = true;
                UnitManager.Instance.SpawnExit();
                Set_One_Hot_Grid_To_False();
            }
        }

        if((UnitManager.Instance.SelectedEnemy != null))
        {
            BaseEnemy Current_Enemy = UnitManager.Instance.SelectedEnemy;
            float enemyX = Current_Enemy.transform.position.x;
            float enemyY = Current_Enemy.transform.position.y;
            Set_One_Hot_Grid_To_True((int)enemyX, (int)enemyY);
        }

        float currentX = Current_Hero.transform.position.x;
        float currentY = Current_Hero.transform.position.y;

        print($"ACTION: {currentX}, {currentY}");
        print($"THIS.TRANSFORM: {this.transform.position}");
        Set_One_Hot_Grid_To_False();
        Set_One_Hot_Grid_To_True((int)currentX, (int)currentY); //Hero


        if(UnitManager.Instance.CanEscape)
        {
            print($"ESCAPE EXIT: {UnitManager.Instance.EscapeExit}");
            float ExitX = UnitManager.Instance.EscapeExit.x;
            float ExitY = UnitManager.Instance.EscapeExit.y;
            Set_One_Hot_Grid_To_False2();
            Set_One_Hot_Grid_To_True2((int)ExitX, (int)ExitY);
            Set_One_Hot_Grid_To_True2((int)currentX, (int)currentY);

            //BaseUnit Current_Hero = UnitManager.Instance.SelectedHeroes[2];
            print($"ExitPortal: {UnitManager.Instance.portal.transform.position}");
        }

        if((UnitManager.Instance.CanEscape) && (Current_Hero.transform.position == UnitManager.Instance.EscapeExit)) 
        {
            AddReward(20f);
            print(Current_Hero.transform.position);
            Destroy(Current_Hero.gameObject);
            print("I Escaped");
            UnitManager.Instance.EscapeCount += 1;
            if (UnitManager.Instance.EscapeCount + UnitManager.Instance.DeadHeroes == 3) 
            {
                GameManager.Instance.ChangeState(GameState.EnemiesTurn);
            }
            this.EndEpisode();
            UnitManager.Instance.ResetGame();
        }

        AddReward(-0.05f);

        //GameManager.Instance.ChangeState(GameState.EnemiesTurn);
        

    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsOut = actionsOut.DiscreteActions;
        if (Input.GetKeyDown(KeyCode.W))
        {
            discreteActionsOut[0] = 0;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            discreteActionsOut[0] = 1;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            discreteActionsOut[0] = 2;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            discreteActionsOut[0] = 3;
        }
    }

    public void Create_One_Hot_Grid()
    {
        // Create a 2D array of boolean values
        EnemyGrid = new int[width, height];

        // Populate the grid with boolean values (for demonstration, we'll set all values to true)
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                EnemyGrid[x, y] = 0; // Set all values to true (you can modify this as needed)
            }
        }
    }

    public void Set_One_Hot_Grid_To_False()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                EnemyGrid[x, y] = 0;
            }
        }
    }

    public void Set_One_Hot_Grid_To_True(int xIndex, int yIndex)
    {
        EnemyGrid[xIndex, yIndex] = 1;
    }

    public void Create_One_Hot_Grid2()
    {
        // Create a 2D array of boolean values
        ExitGrid = new int[width, height];

        // Populate the grid with boolean values (for demonstration, we'll set all values to true)
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                ExitGrid[x, y] = 0; // Set all values to true (you can modify this as needed)
            }
        }
    }

    public void Set_One_Hot_Grid_To_False2()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                ExitGrid[x, y] = 0;
            }
        }
    }

    public void Set_One_Hot_Grid_To_True2(int xIndex, int yIndex)
    {
        ExitGrid[xIndex, yIndex] = 1;
    }

    public void MakeMove()
    {
        RequestDecision();
        GameManager.Instance.ChangeState(GameState.EnemiesTurn);
    }
}