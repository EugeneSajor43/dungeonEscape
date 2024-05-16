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

    //public VoidEventChannelSO RequestMoveChannel;

    private bool is_UP_valid = false;
    private bool is_DOWN_valid = false;
    private bool is_RIGHT_valid = false;
    private bool is_LEFT_valid = false;

    //BaseUnit Current_Hero = UnitManager.Instance.SelectedHeroes[2];
    

    // Start is called before the first frame update


    public override void OnEpisodeBegin()
    {
        //RequestMoveChannel.OnEventRaised += MakeMove;

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
            AddReward(-5f);
            EndEpisode();
            UnitManager.Instance.ResetGame();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        print("CollectObservations");

        BaseUnit Current_Hero = UnitManager.Instance.SelectedHeroes[2];

        sensor.AddObservation(Current_Hero.transform.position);

        if(UnitManager.Instance.SelectedEnemy != null)
        {
            sensor.AddObservation(targetEnemy.transform.position);
        }

        if(UnitManager.Instance.CanEscape)
        {
            //sensor.AddObservation(UnitManager.Instance.EscapeExit);
            sensor.AddObservation(targetExit.transform.position);
        }
    }

    public override void WriteDiscreteActionMask(IDiscreteActionMask actionMask)
    {
        //actionMask.SetActionEnabled(branch, actionIndex, isEnabled);
        
        //BaseUnit Current_Hero = UnitManager.Instance.SelectedHeroes[2];

        print("WriteDiscreteActionMask");
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
        print("OnActionReceived");
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
                AddReward(1f);
                print("Player kills");
                Destroy(UnitManager.Instance.SelectedEnemy.gameObject);
                UnitManager.Instance.CanEscape = true;
                UnitManager.Instance.SpawnExit();
            }
        }

        if(UnitManager.Instance.CanEscape)
        {
            float currentY = Current_Hero.transform.position.y;
            float currentX = Current_Hero.transform.position.x;
            print($"ACTION: {currentX}, {currentY}");
            print($"THIS.TRANSFORM: {this.transform.position}");
            print($"ESCAPE EXIT: {UnitManager.Instance.EscapeExit}");
            //BaseUnit Current_Hero = UnitManager.Instance.SelectedHeroes[2];

            print($"ExitPortal: {UnitManager.Instance.portal.transform.position}");
        }
        if((UnitManager.Instance.CanEscape) && (this.transform.position == UnitManager.Instance.EscapeExit)) 
        {
            AddReward(3f);
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

        AddReward(-0.02f);

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

    public Vector2 CreateBooleanGrid()
    {
        // Define the size of the grid
        int width = 16;
        int height = 9;

        // Create a 2D array of boolean values
        bool[,] grid = new bool[width, height];

        // Populate the grid with boolean values (for demonstration, we'll set all values to true)
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                grid[x, y] = true; // Set all values to true (you can modify this as needed)
            }
        }

        // Create a Vector2 to represent the size of the grid
        Vector2 gridSize = new Vector2(width, height);

        return gridSize;
    }

    public void MakeMove()
    {
        RequestDecision();
        GameManager.Instance.ChangeState(GameState.EnemiesTurn);
    }
}
