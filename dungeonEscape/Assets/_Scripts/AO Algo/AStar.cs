using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{
    Vector3 dummy_location;
    public Vector3 hero_Location;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Takes a grid and returns solved new grid.
    /*public Dictionary<(int, int), int> backtracking() {
        // Key (x,y) coordinate, Value: (1) hero1, (2) hero2, (3) hero3
        Dictionary<(int, int), int> grid = new Dictionary<(int, int), int>();

        BaseUnit[] SelectedHeroes = UnitManager.Instance.SelectedHeroes;
        Vector3 dummy_location = new Vector3(-1f, -1f, -1f);

        Vector3 hero_Location = (SelectedHeroes[0] != null) ? (SelectedHeroes[0].transform.position) : (dummy_location);



    }

    public Dictionary<(int, int), int> backtrackingSearch() {

    }*/
}
