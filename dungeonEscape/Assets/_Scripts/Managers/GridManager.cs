using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GridManager : MonoBehaviour {
    public static GridManager Instance;
    [SerializeField] private int _width, _height;

    [SerializeField] private Tile _grassTile, _mountainTile;

    [SerializeField] private Transform _cam;

    private Dictionary<Vector2, Tile> _tiles;

<<<<<<< Updated upstream
    void Awake() {
        Instance = this;
=======
    public int limit = 9;

    public int[,] MountainGrid; //Needed for RL

    void Awake() {
        Instance = this;
        Create_One_Hot_Grid2();

>>>>>>> Stashed changes
    }

    public void GenerateGrid()
    {
<<<<<<< Updated upstream
=======
        //Set_One_Hot_Grid_To_False2();
        int lesson_num = 9; //(int)Academy.Instance.EnvironmentParameters.GetWithDefault("lesson_number", limit);
>>>>>>> Stashed changes
        _tiles = new Dictionary<Vector2, Tile>();
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++) {
<<<<<<< Updated upstream
                var randomTile = Random.Range(0, 6) == 3 ? _mountainTile : _grassTile;
=======
                //var randomTile = Random.Range(0, 6) == 3 ? _mountainTile : _grassTile;
                var randomTile = _grassTile;

                if(lesson_num == 2)
                {                                       
                    if(random_orientation == 0)
                    {
                        if(y > 5)
                        {
                            randomTile = _mountainTile;
                            //Set_One_Hot_Grid_To_True2(x, y);
                        }
                        else if(y < 3)
                        {
                            randomTile = _mountainTile;
                            //Set_One_Hot_Grid_To_True2(x, y);
                        }
                        else if((x == 2) && (y == 4))
                        {
                            randomTile = _mountainTile;
                        }                        
                        else if((x == 3) && (y == 4))
                        {
                            randomTile = _mountainTile;
                        }                        
                        else if((x == 4) && (y == 4))
                        {
                            randomTile = _mountainTile;
                        }
                        else if((x == 5) && (y == 3))
                        {
                            randomTile = _mountainTile;
                        }
                        else if((x == 7) && (y == 4))
                        {
                            randomTile = _mountainTile;
                        }
                        else if((x == 8) && (y == 4))
                        {
                            randomTile = _mountainTile;
                        }
                        else if((x == 9) && (y == 5))
                        {
                            randomTile = _mountainTile;
                        }
                        else if((x == 11) && (y == 3))
                        {
                            randomTile = _mountainTile;
                        }
                        else if((x == 11) && (y == 4))
                        {
                            randomTile = _mountainTile;
                        }
                        else if((x == 13) && (y == 4))
                        {
                            randomTile = _mountainTile;
                        }
                        else if((x == 14) && (y == 3))
                        {
                            randomTile = _mountainTile;
                        }
/*                        else if((y > 0) && (y < 8) && (x > 0) && (x < 15))
                        {
                            randomTile = Random.Range(0, 36) == 3 ? _mountainTile : _grassTile;
                            if(randomTile == _mountainTile)
                            {
                                Set_One_Hot_Grid_To_True2(x, y);
                            } 
                        }    */                      
                    }
                    else
                    {        
                        if(x > 9)
                        {
                            randomTile = _mountainTile;
                            //Set_One_Hot_Grid_To_True2(x, y);
                        }
                        else if(x < 7)
                        {
                            randomTile = _mountainTile;
                            //Set_One_Hot_Grid_To_True2(x, y);
                        }
                        else if((x == 7) && (y == 3))
                        {
                            randomTile = _mountainTile;
                        }
                        else if((x == 7) && (y == 7))
                        {
                            randomTile = _mountainTile;
                        }
                        else if((x == 8) && (y == 1))
                        {
                            randomTile = _mountainTile;
                        }                        
                        else if((x == 8) && (y == 2))
                        {
                            randomTile = _mountainTile;
                        }
                        else if((x == 8) && (y == 5))
                        {
                            randomTile = _mountainTile;
                        }
                        else if((x == 9) && (y == 6))
                        {
                            randomTile = _mountainTile;
                        }                        
                        /*else if((y > 0) && (y < 8) && (x > 0) && (x < 15))
                        {
                            randomTile = Random.Range(0, 36) == 3 ? _mountainTile : _grassTile;
                             if(randomTile == _mountainTile)
                            {
                                Set_One_Hot_Grid_To_True2(x, y);
                            } 
                        }       */                   
                    }
                }
                else if(lesson_num == 3)
                {
                    if(random_orientation == 0)
                    {
                        if(y > 6)
                        {
                            randomTile = _mountainTile;
                            //Set_One_Hot_Grid_To_True2(x, y);
                        }
                        else if(y < 3)
                        {
                            randomTile = _mountainTile;
                            //Set_One_Hot_Grid_To_True2(x, y);
                        }
                        else if((x == 1) && (y == 4))
                        {
                            randomTile = _mountainTile;
                        }
                        else if((x == 2) && (y == 4))
                        {
                            randomTile = _mountainTile;
                        }
                        else if((x == 3) && (y == 3))
                        {
                            randomTile = _mountainTile;
                        }
                        else if((x == 4) && (y == 5))
                        {
                            randomTile = _mountainTile;
                        }
                        else if((x == 5) && (y == 3))
                        {
                            randomTile = _mountainTile;
                        }
                        else if((x == 5) && (y == 6))
                        {
                            randomTile = _mountainTile;
                        }
                        else if((x == 7) && (y == 4))
                        {
                            randomTile = _mountainTile;
                        }
                        else if((x == 7) && (y == 6))
                        {
                            randomTile = _mountainTile;
                        }
                        else if((x == 8) && (y == 5))
                        {
                            randomTile = _mountainTile;
                        }
                        else if((x == 10) && (y == 3))
                        {
                            randomTile = _mountainTile;
                        }
                        else if((x == 10) && (y == 5))
                        {
                            randomTile = _mountainTile;
                        }                         
                        else if((x == 11) && (y == 4))
                        {
                            randomTile = _mountainTile;
                        }
                        else if((x == 13) && (y == 4))
                        {
                            randomTile = _mountainTile;
                        }                        
                        else if((x == 13) && (y == 6))
                        {
                            randomTile = _mountainTile;
                        }     
                        else if((x == 14) && (y == 5))
                        {
                            randomTile = _mountainTile;
                        }                             
/*                         else if((y > 0) && (y < 8) && (x > 0) && (x < 15))
                        {
                            randomTile = Random.Range(0, 24) == 3 ? _mountainTile : _grassTile;
                            if(randomTile == _mountainTile)
                            {
                                Set_One_Hot_Grid_To_True2(x, y);
                            }
                        }    */                     
                    }
                    else
                    {        
                        if(x > 9)
                        {
                            randomTile = _mountainTile;
                            //Set_One_Hot_Grid_To_True2(x, y);
                        }
                        else if(x < 6)
                        {
                            randomTile = _mountainTile;
                            //Set_One_Hot_Grid_To_True2(x, y);
                        }
                        else if((x == 6) && (y == 3))
                        {
                            randomTile = _mountainTile;
                        }
                        else if((x == 6) && (y == 7))
                        {
                            randomTile = _mountainTile;
                        }
                        else if((x == 7) && (y == 3))
                        {
                            randomTile = _mountainTile;
                        }
                        else if((x == 7) && (y == 5))
                        {
                            randomTile = _mountainTile;
                        }
                        else if((x == 8) && (y == 1))
                        {
                            randomTile = _mountainTile;
                        }                        
                        else if((x == 8) && (y == 2))
                        {
                            randomTile = _mountainTile;
                        }
                        else if((x == 8) && (y == 6))
                        {
                            randomTile = _mountainTile;
                        }
                        else if((x == 8) && (y == 7))
                        {
                            randomTile = _mountainTile;
                        }
                        else if((x == 9) && (y == 4))
                        {
                            randomTile = _mountainTile;
                        }                         
                        else if((x == 9) && (y == 6))
                        {
                            randomTile = _mountainTile;
                        }                                      
/*                         else if((y > 0) && (y < 8) && (x > 0) && (x < 15))
                        {
                            randomTile = Random.Range(0, 27) == 3 ? _mountainTile : _grassTile;
                           if(randomTile == _mountainTile)
                            {
                                Set_One_Hot_Grid_To_True2(x, y);
                            }
                        }   */                      
                    }
                }
                else if(lesson_num == 4)
                {
                    if(random_orientation == 0)
                    {
                        if(y > 5)
                        {
                            randomTile = _mountainTile;
                            //Set_One_Hot_Grid_To_True2(x, y);
                        }
                        else if(y < 2)
                        {
                            randomTile = _mountainTile;
                            //Set_One_Hot_Grid_To_True2(x, y);
                        }
                        else if((y > 0) && (y < 8) && (x > 0) && (x < 15))
                        {
                            randomTile = Random.Range(0, 18) == 3 ? _mountainTile : _grassTile;
/*                             if(randomTile == _mountainTile)
                            {
                                Set_One_Hot_Grid_To_True2(x, y);
                            } */
                        }
                    }
                    else
                    {        
                        if(x > 10)
                        {
                            randomTile = _mountainTile;
                            //Set_One_Hot_Grid_To_True2(x, y);
                        }
                        else if(x < 5)
                        {
                            randomTile = _mountainTile;
                            //Set_One_Hot_Grid_To_True2(x, y);
                        }
                        else if((y > 0) && (y < 8) && (x > 0) && (x < 15))
                        {
                            randomTile = Random.Range(0, 21) == 3 ? _mountainTile : _grassTile;
/*                             if(randomTile == _mountainTile)
                            {
                                Set_One_Hot_Grid_To_True2(x, y);
                            } */
                        }
                    }
                }
                else if(lesson_num == 5)
                {
                    if(random_orientation == 0)
                    {
                        if(y > 6)
                        {
                            randomTile = _mountainTile;
                            //Set_One_Hot_Grid_To_True2(x, y);
                        }
                        else if(y < 2)
                        {
                            randomTile = _mountainTile;
                            //Set_One_Hot_Grid_To_True2(x, y);
                        }
                        else if((y > 0) && (y < 8) && (x > 0) && (x < 15))
                        {
                            randomTile = Random.Range(0, 15) == 3 ? _mountainTile : _grassTile;
/*                             if(randomTile == _mountainTile)
                            {
                                Set_One_Hot_Grid_To_True2(x, y);
                            } */
                        }
                    }
                    else
                    {        
                        if(x > 11)
                        {
                            randomTile = _mountainTile;
                            //Set_One_Hot_Grid_To_True2(x, y);
                        }
                        else if(x < 4)
                        {
                            randomTile = _mountainTile;
                            //Set_One_Hot_Grid_To_True2(x, y);
                        }
                        else if((y > 0) && (y < 8) && (x > 0) && (x < 15))
                        {
                            randomTile = Random.Range(0, 20) == 3 ? _mountainTile : _grassTile;
/*                             if(randomTile == _mountainTile)
                            {
                                Set_One_Hot_Grid_To_True2(x, y);
                            } */
                        }
                    }
                }
                else if(lesson_num == 6)
                {
                    if(random_orientation == 0)
                    {
                        if(y > 7)
                        {
                            randomTile = _mountainTile;
                            //Set_One_Hot_Grid_To_True2(x, y);
                        }
                        else if(y < 2)
                        {
                            randomTile = _mountainTile;
                            //Set_One_Hot_Grid_To_True2(x, y);
                        }
                        else if((y > 0) && (y < 8) && (x > 0) && (x < 15))
                        {
                            randomTile = Random.Range(0, 10) == 3 ? _mountainTile : _grassTile;
/*                             if(randomTile == _mountainTile)
                            {
                                Set_One_Hot_Grid_To_True2(x, y);
                            } */
                        }
                    }
                    else
                    {        
                        if(x > 12)
                        {
                            randomTile = _mountainTile;
                            //Set_One_Hot_Grid_To_True2(x, y);
                        }
                        else if(x < 3)
                        {
                            randomTile = _mountainTile;
                            //Set_One_Hot_Grid_To_True2(x, y);
                        }
                        else if((y > 0) && (y < 8) && (x > 0) && (x < 15))
                        {
                            randomTile = Random.Range(0, 16) == 3 ? _mountainTile : _grassTile;
/*                             if(randomTile == _mountainTile)
                            {
                                Set_One_Hot_Grid_To_True2(x, y);
                            } */
                        }
                    }
                }
                else if(lesson_num == 7)
                {
                    if(random_orientation == 0)
                    {
                        if(y > 7)
                        {
                            randomTile = _mountainTile;
                            //Set_One_Hot_Grid_To_True2(x, y);
                        }
                        else if(y < 1)
                        {
                            randomTile = _mountainTile;
                            //Set_One_Hot_Grid_To_True2(x, y);
                        }
                        else if((y > 0) && (y < 8) && (x > 0) && (x < 15))
                        {
                            randomTile = Random.Range(0, 6) == 3 ? _mountainTile : _grassTile;
/*                             if(randomTile == _mountainTile)
                            {
                                Set_One_Hot_Grid_To_True2(x, y);
                            } */
                        }
                    }
                    else
                    {        
                        if(x > 13)
                        {
                            randomTile = _mountainTile;
                            //Set_One_Hot_Grid_To_True2(x, y);
                        }
                        else if(x < 2)
                        {
                            randomTile = _mountainTile;
                            //Set_One_Hot_Grid_To_True2(x, y);
                        }
                        else if((y > 0) && (y < 8) && (x > 0) && (x < 15))
                        {
                            randomTile = Random.Range(0, 5) == 3 ? _mountainTile : _grassTile;
/*                             if(randomTile == _mountainTile)
                            {
                                Set_One_Hot_Grid_To_True2(x, y);
                            } */
                        }
                    }
                }
                else if(lesson_num == 8)
                {
                    if(random_orientation == 0)
                    {
                        if(y > 7)
                        {
                            randomTile = _mountainTile;
                            //Set_One_Hot_Grid_To_True2(x, y);
                        }
                        else if(y < 1)
                        {
                            randomTile = _mountainTile;
                            //Set_One_Hot_Grid_To_True2(x, y);
                        }
                        else if((y > 0) && (y < 8) && (x > 0) && (x < 15))
                        {
                            randomTile = Random.Range(0, 7) == 3 ? _mountainTile : _grassTile;
/*                             if(randomTile == _mountainTile)
                            {
                                Set_One_Hot_Grid_To_True2(x, y);
                            } */
                        }
                    }
                    else
                    {        
                        if(x > 14)
                        {
                            randomTile = _mountainTile;
                            //Set_One_Hot_Grid_To_True2(x, y);
                        }
                        else if(x < 1)
                        {
                            randomTile = _mountainTile;
                            //Set_One_Hot_Grid_To_True2(x, y);
                        }
                        else if((y > 0) && (y < 8) && (x > 0) && (x < 15))
                        {
                            randomTile = Random.Range(0, 9) == 3 ? _mountainTile : _grassTile;
/*                             if(randomTile == _mountainTile)
                            {
                                Set_One_Hot_Grid_To_True2(x, y);
                            } */
                        }
                    }
                }
                else if(lesson_num == 9)
                {
                    if((y > 0) && (y < 8) && (x > 0) && (x < 15))
                    {
                        randomTile = Random.Range(0, 5) == 3 ? _mountainTile : _grassTile;

                    }
                    else if((x == 7) && (y == 0))
                    {
                        randomTile = _mountainTile;
                    }
                    else if((x == 7) && (y == 8))
                    {
                        randomTile = _mountainTile;
                    }             
/*                     if(randomTile == _mountainTile)
                    {
                        Set_One_Hot_Grid_To_True2(x, y);
                    } */
                }

/*                 else if(lesson_num == 10)
                {
                    randomTile = Random.Range(0, 9) == 3 ? _mountainTile : _grassTile;
                }
                else if(lesson_num == 11)
                {
                    randomTile = Random.Range(0, 6) == 3 ? _mountainTile : _grassTile;
                } */

>>>>>>> Stashed changes
                var spawnedTile = Instantiate(randomTile, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";

              
                spawnedTile.Init(x,y);


                _tiles[new Vector2(x, y)] = spawnedTile;
            }
        }

        _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);

        GameManager.Instance.ChangeState(GameState.SpawnHeroes);
<<<<<<< Updated upstream
=======
        //Destroy(UnitManager.Instance.SelectedHeroes[0].gameObject);
       //UnitManager.Instance.DeadHeroes += 1;
       // Destroy(UnitManager.Instance.SelectedHeroes[1].gameObject);
       // UnitManager.Instance.DeadHeroes += 1;
>>>>>>> Stashed changes
    }

    public Tile GetHeroSpawnTile() {
        return _tiles.Where(t => t.Key.x < _width / 2 && t.Value.Walkable).OrderBy(t => Random.value).First().Value;
    }

    public Tile GetEnemySpawnTile()
    {
        return _tiles.Where(t => t.Key.x > _width / 2 && t.Value.Walkable).OrderBy(t => Random.value).First().Value;
    }

    public Tile GetTileAtPosition(Vector2 pos)
    {
        if (_tiles.TryGetValue(pos, out var tile)) return tile;
        return null;
    }
<<<<<<< Updated upstream
=======

    public Dictionary<Vector2, Tile> GetGrid()
    {
        return _tiles;
    }

    public void Create_One_Hot_Grid2()
    {
        // Create a 2D array of boolean values
        MountainGrid = new int[_width, _height];

        // Populate the grid with boolean values (for demonstration, we'll set all values to true)
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                MountainGrid[x, y] = 0; // Set all values to true (you can modify this as needed)
            }
        }
    }

    public void Set_One_Hot_Grid_To_False2()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                MountainGrid[x, y] = 0;
            }
        }
    }

    public void Set_One_Hot_Grid_To_True2(int xIndex, int yIndex)
    {
        MountainGrid[xIndex, yIndex] = 1;
    }
>>>>>>> Stashed changes
}