using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class State
{
    public Action ActiveAction, OnEnterAction, OnExitAction;

    //Constructor which takes methods for three types
    public State(Action active, Action onEnter, Action onExit)
    {
        ActiveAction = active;
        OnEnterAction = onEnter;
        OnExitAction = onExit;
    }

    /// <summary>
    /// Execute each state individually
    /// </summary>
    public void Execute()
    {
        /*if (ActiveAction != null) 
            ActiveAction.Invoke();*/
        // ? if the object is not null use it
        ActiveAction?.Invoke();
    }

    public void OnEnter() { OnEnterAction?.Invoke(); }

    public void OnExit() { OnExitAction?.Invoke(); }

    //public void OnExit() => OnExitAction?.Invoke();
}
