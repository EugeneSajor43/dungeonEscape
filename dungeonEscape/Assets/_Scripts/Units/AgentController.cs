using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using System;


public class AgentController : Agent
{

    [SerializeField] private Transform targetEnemy;
    

    // Start is called before the first frame update


    public override void OnEpisodeBegin()
    {
        base.OnEpisodeBegin();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(UnitManager.Instance.SelectedHeroes[0].transform.position);
        sensor.AddObservation(UnitManager.Instance.SelectedHeroes[1].transform.position);
        sensor.AddObservation(targetEnemy.transform.position);
        sensor.AddObservation(UnitManager.Instance.EscapeExit);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        // Get the actions from each branch
        int upAction = actions.DiscreteActions[0];
        int downAction = actions.DiscreteActions[1];
        int leftAction = actions.DiscreteActions[2];
        int rightAction = actions.DiscreteActions[3];

        float current_X = transform.position.x;
        float current_Y = transform.position.y;

        Vector2 UP_pos = new Vector2(current_X, current_Y + 1f);
        Vector2 DOWN_pos = new Vector2(current_X, current_Y - 1f);
        Vector2 LEFT_pos = new Vector2(current_X - 1f, current_Y);
        Vector2 RIGHT_pos = new Vector2(current_X + 1f, current_Y);

        Tile UP_tile = GridManager.Instance.GetTileAtPosition(UP_pos);
        Tile DOWN_tile = GridManager.Instance.GetTileAtPosition(DOWN_pos);
        Tile LEFT_tile = GridManager.Instance.GetTileAtPosition(LEFT_pos);
        Tile RIGHT_tile = GridManager.Instance.GetTileAtPosition(RIGHT_pos);

        // Perform the corresponding action based on the branch
        if ((upAction == 1) && (UP_tile.TileName != "Mountain") && (GridManager.Instance.IsTileInBounds(UP_pos)))
        {
            transform.position += new Vector3(0f, 1f, 0f);
        }
        else if ((downAction == 1) && (DOWN_tile.TileName != "Mountain") && (GridManager.Instance.IsTileInBounds(DOWN_pos)))
        {
            transform.position += new Vector3(0f, -1f, 0f);
        }
        else if ((leftAction == 1) && (LEFT_tile.TileName != "Mountain") && (GridManager.Instance.IsTileInBounds(LEFT_pos)))
        {
            transform.position += new Vector3(-1f, 0f, 0f);
        }
        else if ((rightAction == 1) && (RIGHT_tile.TileName != "Mountain") && (GridManager.Instance.IsTileInBounds(RIGHT_pos)))
        {
            transform.position += new Vector3(1f, 0f, 0f);
        }
    

        if ((targetEnemy != null) && (targetEnemy.transform.position == transform.position)) 
        {
            print("Player kills");
            Destroy(UnitManager.Instance.SelectedEnemy.gameObject);
            UnitManager.Instance.CanEscape = true;
            UnitManager.Instance.SpawnExit();
            AddReward(10f);
        }

        if ((UnitManager.Instance.CanEscape) && (transform.position == UnitManager.Instance.EscapeExit)) 
        {
            AddReward(20f);
            //UnitManager.Instance.Escaped();
            EndEpisode();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        base.Heuristic(actionsOut);
    }

}
