using UnityEngine;
using System.Collections;

public class BattleController : StateMachine
{
    public CameraRig cameraRig;
    public Board board;
    public LevelData levelData;
    public Transform tileSelectionIndicator;
    // position of the currently selected tile
    public Point pos;

    public GameObject heroPrefab;
    public Unit currentUnit;
    public Tile currentTile
    {
        get { return board.GetTile(pos); }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ChangeState<InitBattleState>();
    }
}
