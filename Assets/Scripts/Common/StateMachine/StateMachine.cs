using UnityEngine;
using System.Collections;
using UnityEditorInternal;

public class StateMachine : MonoBehaviour
{
    protected State _currentState;
    protected bool _inTransition;

    public virtual State CurrentState
    {
        get { return _currentState; }
        set { Transition(value); }
    }

    public virtual T GetState<T>() where T : State
    {
        // Look at the GameObject this StateMachine is attached to
        // check if a component of type T is attached to it
        T target = GetComponent<T>();

        if (target == null)
            target = gameObject.AddComponent<T>();
        return target;
    }
    public virtual void ChangeState<T>() where T : State
    {
        CurrentState = GetState<T>();
    }
    protected virtual void Transition(State value)
    {
        if (_currentState == value || _inTransition)
            return;
        _inTransition = true;
        if (_currentState != null)
            _currentState.Exit();
        _currentState = value;
        if (_currentState != null)
            _currentState.Enter();
        _inTransition = false;
    }
}
