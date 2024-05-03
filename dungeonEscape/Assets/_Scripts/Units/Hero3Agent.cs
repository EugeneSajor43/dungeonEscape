using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using System;


public class Hero3Agent : Agent
{

    [SerializeField] private Transform targetEnemy;
    

    // Start is called before the first frame update


    public override void OnEpisodeBegin()
    {
        base.OnEpisodeBegin();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(this.transform.position);
        sensor.AddObservation(targetEnemy.transform.position);
        sensor.AddObservation(UnitManager.Instance.EscapeExit);
        //sensor.AddObservation(UnitManager.Instance.SelectedHeroes[0].transform.position);
        //sensor.AddObservation(UnitManager.Instance.SelectedHeroes[1].transform.position);
    }

    public override void WriteDiscreteActionMask(IDiscreteActionMask actionMask)
    {
        //actionMask.SetActionEnabled(branch, actionIndex, isEnabled);

        BaseUnit Current_Hero = UnitManager.Instance.SelectedHeroes[2];
        Vector3 dummy_location = new Vector3(-1f, -1f, -1f);
        Vector3 hero1_Location = (UnitManager.Instance.SelectedHeroes[0] != null) ? (UnitManager.Instance.SelectedHeroes[0].transform.position) : (dummy_location);
        Vector3 hero2_Location = (UnitManager.Instance.SelectedHeroes[1] != null) ? (UnitManager.Instance.SelectedHeroes[1].transform.position) : (dummy_location);
        float currentY = Current_Hero.transform.position.y;
        float currentX = Current_Hero.transform.position.x;

        bool is_UP_valid = false;
        bool is_DOWN_valid = false;
        bool is_RIGHT_valid = false;
        bool is_LEFT_valid = false;

        if((currentY + 1f) < 9)
        {
            is_UP_valid = true;
            float Y = currentY + 1f;
            Vector3 UP_pos = new Vector3(currentX, Y, 0f);
            Tile tile0 = GridManager.Instance.GetTileAtPosition(new Vector2(currentX, Y));
            if(tile0.TileName == "Mountain")
            {
                is_UP_valid = false;
            }
            if((UP_pos == hero1_Location) || (UP_pos == hero2_Location))
            {
                is_UP_valid = false;
            }
        }
        if((currentY - 1f) >= 0)
        {
            is_DOWN_valid = true;
            float Y = currentY - 1f;
            Vector3 DOWN_pos = new Vector3(currentX, Y, 0f);
            Tile tile1 = GridManager.Instance.GetTileAtPosition(new Vector2(currentX, Y));
            if(tile1.TileName == "Mountain")
            {
                is_DOWN_valid = false;
            }
            if((DOWN_pos == hero1_Location) || (DOWN_pos == hero2_Location))
            {
                is_DOWN_valid = false;
            }
        }
        if((currentX + 1f) < 16)
        {
            is_RIGHT_valid = true;
            float X = currentX + 1f;
            Vector3 RIGHT_pos = new Vector3(X, currentY, 0f);
            Tile tile2 = GridManager.Instance.GetTileAtPosition(new Vector2(X, currentY));
            if(tile2.TileName == "Mountain")
            {
                is_RIGHT_valid = false;
            }
            if((RIGHT_pos == hero1_Location) || (RIGHT_pos == hero2_Location))
            {
                is_RIGHT_valid = false;
            }
        }
        if((currentX - 1f) >= 0)
        {
            is_LEFT_valid = true;
            float X = currentX - 1f;
            Vector3 LEFT_pos = new Vector3(X, currentY, 0f);
            Tile tile3 = GridManager.Instance.GetTileAtPosition(new Vector2(X, currentY));
            if(tile3.TileName == "Mountain")
            {
                is_LEFT_valid = false;
            }
            if((LEFT_pos == hero1_Location) || (LEFT_pos == hero2_Location))
            {
                is_LEFT_valid = false;
            }
        }

        if(!is_UP_valid)
        {
            actionMask.SetActionEnabled(0, 0, false);
        }
        if(!is_DOWN_valid)
        {
            actionMask.SetActionEnabled(0, 1, false);
        }
        if(!is_RIGHT_valid)
        {
            actionMask.SetActionEnabled(0, 2, false);
        }
        if(!is_LEFT_valid)
        {
            actionMask.SetActionEnabled(0, 3, false);
        }

    }

    public override void OnActionReceived(ActionBuffers actions)
    {
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
        

        if(UnitManager.Instance.SelectedEnemy.transform.position == Current_Hero.transform.position) 
        {
            AddReward(5f);
            print("Player kills");
            Destroy(UnitManager.Instance.SelectedEnemy.gameObject);
            UnitManager.Instance.CanEscape = true;
            UnitManager.Instance.SpawnExit();
        }

        if(UnitManager.Instance.CanEscape && Current_Hero.transform.position == UnitManager.Instance.EscapeExit) 
        {
            AddReward(5f);
            print(Current_Hero.transform.position);
            Destroy(Current_Hero.gameObject);
            print("Escaped");
            UnitManager.Instance.EscapeCount += 1;
            if (UnitManager.Instance.EscapeCount + UnitManager.Instance.DeadHeroes == 3) {
                GameManager.Instance.ChangeState(GameState.EnemiesTurn);
            }
        }

        GameManager.Instance.ChangeState(GameState.EnemiesTurn);
        

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


    public void MakeMove()
    {
        RequestDecision();
        GameManager.Instance.ChangeState(GameState.Heroes3Turn);
    }
}
