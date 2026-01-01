using System.Collections;
using UnityEngine;

public class InitBattleState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        // BattleController ChangeState<InitBattleState>
        // StateMachine::CurrentState::set(a instance of InitBattleState)
        // StateMachine::Transition(a instance of InitBattleState)
        // StateMachine::_in_transition = true
        // (a instance of InitBattleState).Enter()
        // ==> This function is called
        // which calls InitBattleState::Init()
        // owner[BattleController] ChangeState<MoveTargetState>
        // in the same frame Transition would fail
        StartCoroutine(Init());
        // yeid return null would tell Unity to pause execution
        // of the current function until the next frame
        // and continue the rest
    }
    IEnumerator Init()
    {
        board.Load(owner.levelData);
        Point p = new Point(
            (int)owner.levelData.tiles[0].x, 
            (int)owner.levelData.tiles[0].z
        );
        SelectTile(p);
        SpawnTestUnits();
        yield return null;
        owner.ChangeState<MoveTargetState>();
    }

    // Testing ONLY!!
    void SpawnTestUnits()
    {
        System.Type[] components = new System.Type[]
        {
            typeof(WalkMovement),
            typeof(FlyMovement),
            typeof(TeleportMovement)
        };
        for (int i = 0; i < 3; ++i)
        {
            GameObject instance = Instantiate(owner.heroPrefab) as GameObject;
            Point p = new Point(
                (int)owner.levelData.tiles[i].x,
                (int)owner.levelData.tiles[i].z
            );

            Unit unit = instance.GetComponent<Unit>();
            unit.Place(board.GetTile(p));
            unit.Match();

            Movement m = instance.AddComponent(components[i]) as Movement;
            m.range = 5;
            m.jumpHeight = 1;
        }
    }
}
