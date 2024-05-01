using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour {
    public string TileName;
    [SerializeField] protected SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    [SerializeField] private bool _isWalkable;

    [SerializeField] public bool _isPortalSpawned;
    [SerializeField] private Color _PortalColor;

    public BaseUnit OccupiedUnit;
    public bool Walkable => _isWalkable && OccupiedUnit == null;

    //private IEnumerator moveCoroutine;
    public virtual void Init(int x, int y)
    {
      
    }

    void OnMouseEnter()
    {
        _highlight.SetActive(true);
        MenuManager.Instance.ShowTileInfo(this);
    }

    void OnMouseExit()
    {
        _highlight.SetActive(false);
        MenuManager.Instance.ShowTileInfo(null);
    }

/*      private IEnumerator MoveCoroutine()
    {
        while (GameManager.Instance.GameState != GameState.LostGame || GameManager.Instance.GameState != GameState.WonGame)
        {
            // Call PlayerMove() function here
            UnitManager.Instance.PlayerMove();

            // Wait for 100 milliseconds
            yield return new WaitForSeconds(0.05f);
        }
    }

    void OnMouseDown()
    {
        moveCoroutine = MoveCoroutine();
        StartCoroutine(moveCoroutine);
    }  */



    void OnMouseDown() {
       UnitManager.Instance.PlayerMove();
    }


    public void SetUnit(BaseUnit unit) {
        if (unit.OccupiedTile != null) unit.OccupiedTile.OccupiedUnit = null;
        unit.transform.position = transform.position;
        OccupiedUnit = unit;
        unit.OccupiedTile = this;
    }

    public virtual void ColorPortal()
    {

        if (_isPortalSpawned == true)
        {
            _renderer.color = _PortalColor;
        }
    }

}