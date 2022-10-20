using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State<T>
{
    protected StateMachine<T> stateMachine; //state관리하는 머신 클래스
    protected T stateMachineClass; //gameobject 클래스

    public State() { }
    internal void SetMachineWithClass(StateMachine<T> stateMachine, T stateMachineClass)
    {
        this.stateMachine = stateMachine;
        this.stateMachineClass = stateMachineClass;

        OnAwake();
    }
    public virtual void OnAwake() { }
    public virtual void OnStart() { }
    public abstract void OnUpdate(float deltaTime);
    public virtual void OnEnd() { }
    public virtual void OnHitEvent() { }

}
