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
        yield return null;
        owner.ChangeState<MoveTargetState>();
    }
}
