using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour {
    public string TileName;
    [SerializeField] protected SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    [SerializeField] private bool _isWalkable;
    [SerializeField] private Color _Portalcolor;
    [SerializeField] public bool _isPortalSpawned;
    public int G;
    public int H;
    public int F { get { return G + H; } }
    public Tile previousTile;
    public Vector2Int gridLocation;
    public BaseUnit OccupiedUnit;
    public bool Walkable => _isWalkable && OccupiedUnit == null;
    

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
    public virtual void ColorPortal()
    {

        if (_isPortalSpawned == true)
        {
            _renderer.color = _Portalcolor;
        }
    }

    void OnMouseDown() {
        //UnitManager.Instance.PlayerMove();
    }

    public void SetUnit(BaseUnit unit) {
        if (unit.OccupiedTile != null) unit.OccupiedTile.OccupiedUnit = null;
        unit.transform.position = transform.position;
        OccupiedUnit = unit;
        unit.OccupiedTile = this;
    }
}