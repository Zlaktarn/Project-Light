using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMachine_AI : MonoBehaviour
{
    private Dictionary<Type, BaseState> availableStates;

    public BaseState CurrentState {get; private set; }
    public event Action<BaseState> OnStateChanged;

    public void SetStates(Dictionary<Type, BaseState> states)
    {
        availableStates = states;
    }

    private void Update()
    {
        if(CurrentState == null)
        {
            CurrentState = availableStates.Values.First();
        }

        var nextState = CurrentState?.Tick();

        if(nextState != null && nextState != CurrentState?.GetType())
        {
            SwitchState(nextState);
        }
    }

    private void SwitchState(Type nextState)
    {
        CurrentState = availableStates[nextState];
        OnStateChanged?.Invoke(CurrentState);
    }

    public Type GetType(int type)
    {
        return availableStates.ElementAt(type).Key;
    }
}
