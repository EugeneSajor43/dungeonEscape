using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using Unity.MLAgents;

public class GridManager : MonoBehaviour {
    public static GridManager Instance;
    [SerializeField] private int _width, _height;

    [SerializeField] private Tile _grassTile, _mountainTile;

    [SerializeField] private Transform _cam;

    private Dictionary<Vector2, Tile> _tiles;

    public int limit = 9;

    void Awake() {
        Instance = this;

    }

/*     public void GenerateGrid()
    {
        _tiles = new Dictionary<Vector2, Tile>();
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++) {
                var randomTile = Random.Range(0, 56) == 3 ? _mountainTile : _grassTile;
                var spawnedTile = Instantiate(randomTile, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
                spawnedTile.gridLocation = new Vector2Int(x, y);    
              
                spawnedTile.Init(x,y);


                _tiles[new Vector2(x, y)] = spawnedTile;
            }
        }

        _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);

        GameManager.Instance.ChangeState(GameState.SpawnHeroes);
    } */

    public void GenerateGrid()
    {

        int lesson_num = 9;//(int)Academy.Instance.EnvironmentParameters.GetWithDefault("lesson_number", limit);
        _tiles = new Dictionary<Vector2, Tile>();
        int random_orientation = Random.Range(0, 2);

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++) {
                //var randomTile = Random.Range(0, 6) == 3 ? _mountainTile : _grassTile;
                var randomTile = _grassTile;
                if(lesson_num == 2)
                {                                       
                    if(random_orientation == 0)
                    {
                        if(y > 5)
                        {
                            randomTile = _mountainTile;
                        }
                        else if(y < 3)
                        {
                            randomTile = _mountainTile;
                        }
                    }
                    else
                    {        
                        if(x > 9)
                        {
                            randomTile = _mountainTile;
                        }
                        else if(x < 7)
                        {
                            randomTile = _mountainTile;
                        }
                    }
                }
                else if(lesson_num == 3)
                {
                    if(random_orientation == 0)
                    {
                        if(y > 6)
                        {
                            randomTile = _mountainTile;
                        }
                        else if(y < 3)
                        {
                            randomTile = _mountainTile;
                        }
                    }
                    else
                    {        
                        if(x > 10)
                        {
                            randomTile = _mountainTile;
                        }
                        else if(x < 6)
                        {
                            randomTile = _mountainTile;
                        }
                    }
                }
                else if(lesson_num == 4)
                {
                    if(random_orientation == 0)
                    {
                        if(y > 5)
                        {
                            randomTile = _mountainTile;
                        }
                        else if(y < 2)
                        {
                            randomTile = _mountainTile;
                        }
                        else
                        {
                            randomTile = Random.Range(0, 26) == 3 ? _mountainTile : _grassTile;
                        }
                    }
                    else
                    {        
                        if(x > 11)
                        {
                            randomTile = _mountainTile;
                        }
                        else if(x < 5)
                        {
                            randomTile = _mountainTile;
                        }
                        else
                        {
                            randomTile = Random.Range(0, 26) == 3 ? _mountainTile : _grassTile;
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
                        }
                        else if(y < 2)
                        {
                            randomTile = _mountainTile;
                        }
                        else
                        {
                            randomTile = Random.Range(0, 23) == 3 ? _mountainTile : _grassTile;
                        }
                    }
                    else
                    {        
                        if(x > 12)
                        {
                            randomTile = _mountainTile;
                        }
                        else if(x < 4)
                        {
                            randomTile = _mountainTile;
                        }
                        else
                        {
                            randomTile = Random.Range(0, 23) == 3 ? _mountainTile : _grassTile;
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
                        }
                        else if(y < 2)
                        {
                            randomTile = _mountainTile;
                        }
                        else
                        {
                            randomTile = Random.Range(0, 20) == 3 ? _mountainTile : _grassTile;
                        }
                    }
                    else
                    {        
                        if(x > 13)
                        {
                            randomTile = _mountainTile;
                        }
                        else if(x < 3)
                        {
                            randomTile = _mountainTile;
                        }
                        else
                        {
                            randomTile = Random.Range(0, 20) == 3 ? _mountainTile : _grassTile;
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
                        }
                        else if(y < 1)
                        {
                            randomTile = _mountainTile;
                        }
                        else
                        {
                            randomTile = Random.Range(0, 18) == 3 ? _mountainTile : _grassTile;
                        }
                    }
                    else
                    {        
                        if(x > 14)
                        {
                            randomTile = _mountainTile;
                        }
                        else if(x < 2)
                        {
                            randomTile = _mountainTile;
                        }
                        else
                        {
                            randomTile = Random.Range(0, 18) == 3 ? _mountainTile : _grassTile;
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
                        }
                        else if(y < 1)
                        {
                            randomTile = _mountainTile;
                        }
                        else
                        {
                            randomTile = Random.Range(0, 15) == 3 ? _mountainTile : _grassTile;
                        }
                    }
                    else
                    {        
                        if(x > 14)
                        {
                            randomTile = _mountainTile;
                        }
                        else if(x < 1)
                        {
                            randomTile = _mountainTile;
                        }
                        else
                        {
                            randomTile = Random.Range(0, 15) == 3 ? _mountainTile : _grassTile;
                        }
                    }
                }
                else if(lesson_num == 9)
                {
                    randomTile = Random.Range(0, 12) == 3 ? _mountainTile : _grassTile;
                }

/*                 else if(lesson_num == 10)
                {
                    randomTile = Random.Range(0, 9) == 3 ? _mountainTile : _grassTile;
                }
                else if(lesson_num == 11)
                {
                    randomTile = Random.Range(0, 6) == 3 ? _mountainTile : _grassTile;
                } */

                var spawnedTile = Instantiate(randomTile, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
                spawnedTile.gridLocation = new Vector2Int(x, y);    
              
                spawnedTile.Init(x,y);


                _tiles[new Vector2(x, y)] = spawnedTile;
            }
        }

        _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);

        GameManager.Instance.ChangeState(GameState.SpawnHeroes);
        Destroy(UnitManager.Instance.SelectedHeroes[0].gameObject);
        UnitManager.Instance.DeadHeroes += 1;
        Destroy(UnitManager.Instance.SelectedHeroes[1].gameObject);
        UnitManager.Instance.DeadHeroes += 1;
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

    public Dictionary<Vector2, Tile> GetGrid()
    {
        return _tiles;
    }
}