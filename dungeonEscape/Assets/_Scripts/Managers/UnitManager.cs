using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnitManager : MonoBehaviour {
    public static UnitManager Instance;

    private List<ScriptableUnit> _units;
    public BaseHero SelectedHero;
    public BaseUnit[] SelectedHeroes = new BaseHero[3];
    public Vector3 EscapeExit;
    public bool CanEscape = false;           /// <summary>
    /// change this to false!!!!!!1
    /// </summary>
    public int EscapeCount = 0;
    public int DeadHeroes = 0;

    public BaseEnemy SelectedEnemy;

    void Awake() {
        Instance = this;

        _units = Resources.LoadAll<ScriptableUnit>("Units").ToList();

    }

    public void SpawnHeroes() {
        var heroes = _units.Where(u => u.Faction == Faction.Hero);
        int trackHero = 0;

        foreach(var hero in heroes) {
            var heroPrefab = (BaseUnit)hero.UnitPrefab;
            var spawnedHero = Instantiate(heroPrefab);
            var randomSpawnTile = GridManager.Instance.GetHeroSpawnTile();

            SelectedHeroes[trackHero] = spawnedHero;
            randomSpawnTile.SetUnit(spawnedHero);
            trackHero += 1;
        }


        GameManager.Instance.ChangeState(GameState.SpawnEnemies);
    }

    public void SpawnEnemies()
    {
        var enemyCount = 1;

        for (int i = 0; i < enemyCount; i++)
        {
            var randomPrefab = GetRandomUnit<BaseEnemy>(Faction.Enemy);
            var spawnedEnemy = Instantiate(randomPrefab);
            var randomSpawnTile = GridManager.Instance.GetEnemySpawnTile();

            SelectedEnemy = spawnedEnemy;
            randomSpawnTile.SetUnit(spawnedEnemy);
            print(SelectedEnemy.transform.position);
        }

        GameManager.Instance.ChangeState(GameState.HeroesTurn);
    }

    private T GetRandomUnit<T>(Faction faction) where T : BaseUnit {
        return (T)_units.Where(u => u.Faction == faction).OrderBy(o => Random.value).First().UnitPrefab;
    }

    public void SetSelectedHero(BaseHero hero) {
        SelectedHero = hero;
        MenuManager.Instance.ShowSelectedHero(hero);
    }

    public void PlayerMove() {
        //print("cehck");
        if((GameManager.Instance.GameState != GameState.HeroesTurn) &&
            (GameManager.Instance.GameState != GameState.Heroes2Turn) &&
            (GameManager.Instance.GameState != GameState.Heroes3Turn)) return;
        
        //print("passed");

        BaseUnit currentHero = SelectedHeroes[GameManager.heroTurn-1];
        Vector3 dummy_location = new Vector3(-1f, -1f, -1f);
        Vector3 hero_Location = (SelectedHeroes[0] != null) ? (SelectedHeroes[0].transform.position) : (dummy_location);
        Vector3 hero2_Location = (SelectedHeroes[1] != null) ? (SelectedHeroes[1].transform.position) : (dummy_location);
        Vector3 hero3_Location = (SelectedHeroes[2] != null) ? (SelectedHeroes[2].transform.position) : (dummy_location);
        BaseHero currentHeroBH = (currentHero != null) ? ((BaseHero)currentHero) : (null);

        var random = new System.Random();
        bool inBounds = false;

        while (!inBounds && currentHero != null) {
            // Generate a random integer: 0 (move in x) or 1 (move in y)
            int direction = random.Next(0, 2);

            // Generate a random integer: 0 (move in down or right) or 1 (move up or left)
            int randomNum = random.Next(0, 2);
            float progress = (randomNum == 1) ? 1f : -1f;

            float currentY = currentHeroBH.transform.position.y;
            float currentX = currentHeroBH.transform.position.x;

            float tmp_y = currentHeroBH.transform.position.y + progress;
            float tmp_x = currentHeroBH.transform.position.x + progress;

            Vector3 tmp_final1 = new Vector3(currentX, tmp_y, 0f);
            Vector3 tmp_final2 = new Vector3(tmp_x, currentY, 0f);

            Tile tile1 = GridManager.Instance.GetTileAtPosition(new Vector2(currentX, tmp_y));
            Tile tile2 = GridManager.Instance.GetTileAtPosition(new Vector2(tmp_x, currentY));

            if (direction == 1) {
                if ((0 <= tmp_y && tmp_y <= 8) && 
                    (tmp_final1 != hero_Location) &&
                    (tmp_final1 != hero2_Location) &&
                    (tmp_final1 != hero3_Location) &&
                    (tile1.TileName != "Mountain")) {
                    currentHeroBH.transform.position += new Vector3(0f, progress, 0f);
                    inBounds = true;
                }
            } 
            else {
                if (0 <= tmp_x && tmp_x <= 15 && 
                    (tmp_final2 != hero_Location) &&
                    (tmp_final2 != hero2_Location) &&
                    (tmp_final2 != hero3_Location) &&
                    (tile2.TileName != "Mountain")) {
                    currentHeroBH.transform.position += new Vector3(progress, 0f, 0f);
                    inBounds = true;
                }
            }    
        }

        int currentTurn = GameManager.heroTurn;
        //print(currentTurn);
        GameManager.heroTurn = (GameManager.heroTurn < 3) ? (GameManager.heroTurn + 1) : (1);
        //GameManager.Instance.ChangeState(GameState.EnemiesTurn);

        if (SelectedEnemy != null && currentHeroBH != null && SelectedEnemy.transform.position == currentHeroBH.transform.position) {
            print("Player kills");
            Destroy(SelectedEnemy.gameObject);
            CanEscape = true;
            SpawnExit();
        }

        if (currentHeroBH != null && CanEscape && currentHeroBH.transform.position == EscapeExit) {
            print(currentHeroBH.transform.position);
            Destroy(currentHeroBH.gameObject);
            print("Escaped");
            EscapeCount += 1;
            if (EscapeCount + DeadHeroes == 3) {
                GameManager.Instance.ChangeState(GameState.Heroes2Turn);
            }
        }

        if (currentTurn == 1) {
            GameManager.Instance.ChangeState(GameState.Heroes2Turn);
        } 
        else if (currentTurn == 2) {
            GameManager.Instance.ChangeState(GameState.Heroes3Turn);
        } else {
            GameManager.Instance.ChangeState(GameState.EnemiesTurn);
        }

        //SelectedEnemy.OccupiedTile = this;


        /*if (OccupiedUnit != null) {
            
            if(OccupiedUnit.Faction == Faction.Hero) UnitManager.Instance.SetSelectedHero((BaseHero)OccupiedUnit);
            else {
                if (UnitManager.Instance.SelectedHero != null) {
                    var enemy = (BaseEnemy) OccupiedUnit;
                    Destroy(enemy.gameObject);
                    UnitManager.Instance.SetSelectedHero(null);
                    if (GameManager.heroTurn == 3) {
                        GameManager.Instance.ChangeState(GameState.EnemiesTurn);
                    }
                    GameManager.heroTurn = (GameManager.heroTurn < 3) ? (GameManager.heroTurn + 1) : (0);
                }
            }*/
            
            /*print(GameManager.heroTurn);
            if (GameManager.heroType == 1) {
                if(OccupiedUnit.heroType == 1 && OccupiedUnit.Faction == Faction.Hero) UnitManager.Instance.SetSelectedHero((BaseHero)OccupiedUnit);
                else {
                    if (UnitManager.Instance.SelectedHero != null) {
                        var enemy = (BaseEnemy) OccupiedUnit;
                        Destroy(enemy.gameObject);
                        UnitManager.Instance.SetSelectedHero(null);
                        //GameManager.Instance.ChangeState(GameState.Heroes2Turn);
                        print(GameManager.Instance.GameState);
                    }
                }
            }  
            else if (GameManager.heroType == 2) {
                if(OccupiedUnit.heroType == 2 && OccupiedUnit.Faction == Faction.Hero) UnitManager.Instance.SetSelectedHero((BaseHero)OccupiedUnit);
                else {
                    if (UnitManager.Instance.SelectedHero != null) {
                        var enemy = (BaseEnemy) OccupiedUnit;
                        Destroy(enemy.gameObject);
                        UnitManager.Instance.SetSelectedHero(null);
                        //GameManager.Instance.ChangeState(GameState.Heroes3Turn);
                    }
                }
            }   
            else {
                if(OccupiedUnit.heroType == 3 && OccupiedUnit.Faction == Faction.Hero) UnitManager.Instance.SetSelectedHero((BaseHero)OccupiedUnit);
                else {
                    if (UnitManager.Instance.SelectedHero != null) {
                        var enemy = (BaseEnemy) OccupiedUnit;
                        Destroy(enemy.gameObject);
                        UnitManager.Instance.SetSelectedHero(null);
                        //GameManager.Instance.ChangeState(GameState.EnemiesTurn);
                    }
                }
            }
        }
        else {
            if (UnitManager.Instance.SelectedHero != null ) {

                int currentTurn = UnitManager.Instance.SelectedHero.heroType;

                

                print(GameManager.heroTurn);

                if (currentTurn == GameManager.heroTurn) {
                    SetUnit(UnitManager.Instance.SelectedHero);

                    if (GameManager.heroTurn == 3) {
                        GameManager.Instance.ChangeState(GameState.EnemiesTurn);
                    }

                    GameManager.heroTurn = (GameManager.heroTurn < 3) ? (GameManager.heroTurn + 1) : (0);
                }
                
                
                
                UnitManager.Instance.SetSelectedHero(null); 
            
            }
        }*/
    }

    public void SpawnExit() {
        var random = new System.Random();
        bool goodExit = false;

        while (!goodExit) {
            // Generate a random integer: 0 (left/right) or 1 (up/bottom)
            int side = random.Next(0, 2);
            // Generate a random integer: 0 (left or down) or 1 (right or up)
            int select = random.Next(0, 2);

            if (side == 1) {
                float randomNum = (float)Random.Range(0, 16);
                if (select == 1) {
                    EscapeExit = new Vector3(randomNum, 8f, 0f);
                } else {
                    EscapeExit = new Vector3(randomNum, 0f, 0f);
                }
            } else {
                float randomNum = (float)Random.Range(0, 9);
                if (select == 1) {
                    EscapeExit = new Vector3(15f, randomNum, 0f);
                } else {
                    EscapeExit = new Vector3(0f, randomNum, 0f);
                }
            }
            float newX = EscapeExit.x;
            float newY = EscapeExit.y;

            Tile tile1 = GridManager.Instance.GetTileAtPosition(new Vector2(newX, newY));
            if (tile1.TileName != "Mountain") {
                goodExit = true;
                tile1._isPortalSpawned = true;
                tile1.ColorPortal();
            }

        }
        
        print(EscapeExit);
    }

    public void EnemyMove() {  
        //print(GameManager.Instance.GameState);
        var random = new System.Random();
        bool inBounds = false;

        Vector3 dummy_location = new Vector3(-1f, -1f, -1f);
        Vector3 hero_Location = (SelectedHeroes[0] != null) ? (SelectedHeroes[0].transform.position) : (dummy_location);
        Vector3 hero2_Location = (SelectedHeroes[1] != null) ? (SelectedHeroes[1].transform.position) : (dummy_location);
        Vector3 hero3_Location = (SelectedHeroes[2] != null) ? (SelectedHeroes[2].transform.position) : (dummy_location);

        if (hero_Location == dummy_location && hero2_Location == dummy_location && hero3_Location == dummy_location) {
            GameManager.Instance.ChangeState(GameState.LostGame);
        }

        while (!inBounds && SelectedEnemy != null) {
            // Generate a random integer: 0 (move in x) or 1 (move in y)
            int direction = random.Next(0, 2);

            // Generate a random integer: 0 (move in down or right) or 1 (move up or left)
            int randomNum = random.Next(0, 2);
            float progress = (randomNum == 1) ? 1f : -1f;
            float tmp_y = SelectedEnemy.transform.position.y + progress;
            float tmp_x = SelectedEnemy.transform.position.x + progress;

            if (direction == 1) {
                if (0 <= tmp_y && tmp_y <= 8) {
                    float currentX = SelectedEnemy.transform.position.x;
                    float currentY = SelectedEnemy.transform.position.y + progress;
                    Tile tile1 = GridManager.Instance.GetTileAtPosition(new Vector2(currentX, currentY));

                    if (tile1.TileName != "Mountain") {
                        SelectedEnemy.transform.position += new Vector3(0f, progress, 0f);
                        inBounds = true;
                    }
                }
            } 
            else {
                if (0 <= tmp_x && tmp_x <= 15) {
                    float currentX = SelectedEnemy.transform.position.x + progress;
                    float currentY = SelectedEnemy.transform.position.y;
                    Tile tile1 = GridManager.Instance.GetTileAtPosition(new Vector2(currentX, currentY));

                    if (tile1.TileName != "Mountain") {
                        SelectedEnemy.transform.position += new Vector3(progress, 0f, 0f);
                        inBounds = true;
                    }
                }
            }    
        }

        if (SelectedEnemy != null) {
            if (SelectedEnemy.transform.position == hero_Location) {
                print("Enemy kills");
                Destroy(SelectedHeroes[0].gameObject);
                DeadHeroes += 1;
                print(SelectedHeroes[0]);
            } 
            else if (SelectedEnemy.transform.position == hero2_Location) {
                print("Enemy kills");
                Destroy(SelectedHeroes[1].gameObject);
                DeadHeroes += 1;
                print(SelectedHeroes[1]);
            } 
            else if (SelectedEnemy.transform.position == hero3_Location) {
                print("Enemy kills");
                Destroy(SelectedHeroes[2].gameObject);
                DeadHeroes += 1;
                print(SelectedHeroes[2]);
            } 
        }

        //SelectedEnemy.OccupiedTile = this;
        GameManager.Instance.ChangeState(GameState.HeroesTurn);
    }

    public void ResetGame() {
        if (SelectedHeroes[0] != null) {
            Destroy(SelectedHeroes[0].gameObject);
        } 
        else if (SelectedHeroes[1] != null) {
            Destroy(SelectedHeroes[1].gameObject);
        } 
        else if (SelectedHeroes[2] != null) {
            Destroy(SelectedHeroes[2].gameObject);
        } 

        if (SelectedEnemy != null) {
            Destroy(SelectedEnemy.gameObject);        
        } 
        EscapeExit = new Vector3(-1f, -1f, -1f);
        EscapeCount = 0;
        DeadHeroes = 0;
        CanEscape = false;
        SceneManager.LoadScene("Scenes/SampleScene");
    }

}
