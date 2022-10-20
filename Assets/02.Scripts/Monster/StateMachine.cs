using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class StateMachine<T>
{
    private T stateMachineClass;
    private Dictionary<Type, State<T>> stateLists = new Dictionary<Type, State<T>>();

    private State<T> nowState;
    public State<T> getNowState => nowState;
    private State<T> beforeState;
    public State<T> getBeforeState => beforeState;

    private float stateDurationTime = 0.0f;
    public float getStateDurationTime => stateDurationTime;

    public StateMachine(T stateMachineClass, State<T> initState)
    {
        this.stateMachineClass = stateMachineClass;

        AddStateList(initState);
        nowState = initState;
        nowState.OnStart();
    }

    public void AddStateList(State<T> state)
    {
        state.SetMachineWithClass(this, stateMachineClass);
        stateLists[state.GetType()] = state;
    }

    public Q ChangeState<Q>() where Q : State<T>
    {
        var newType = typeof(Q);
        if (nowState.GetType() == newType) return nowState as Q;

        if (nowState != null)
            nowState.OnEnd();
        beforeState = nowState;
        nowState = stateLists[newType];

        nowState.OnStart();
        stateDurationTime = 0.0f;

        return nowState as Q;
    }

    public void OnUpdate(float deltaTime)
    {
        stateDurationTime += deltaTime;
        nowState.OnUpdate(deltaTime);
    }

    public void OnHitEvent()
    {
        nowState.OnHitEvent();
    }
}
