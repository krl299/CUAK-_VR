using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StateMachine : MonoBehaviour
{
    //Stack Finite State Machines FSM

    public Stack<State> States { get; set; }

    private void Awake()
    {
        //Initialize the Stack of States
        States = new Stack<State>();
    }

    /// <summary>
    /// Get the state on top of the stack
    /// </summary>
    /// <returns></returns>
    private State GetCurrentState()
    {
        return States.Count > 0 ? States.Peek() : null;
    }

    private void Update()
    {
        /*if (GetCurrentState() != null)
        {
            GetCurrentState().ActiveAction.Invoke();
        }*/
        GetCurrentState()?.Execute();
    }

    /// <summary>
    /// Put on top of the stack a new state
    /// We have to pass the 3 states methods
    /// </summary>
    /// <param name="active">Execute on Update</param>
    /// <param name="onEnter"></param>
    /// <param name="onExit"></param>
    public void PushState(Action active, Action onEnter, Action onExit)
    {
        //Check if the current state is not null and call OnExit 
        GetCurrentState()?.OnExit();
        //Create a new State
        State state = new State(active, onEnter, onExit);
        //Add to the top of the stack
        States.Push(state);
        //Execute  OnEnter method of the current state (new state) 
        GetCurrentState().OnEnter();
    }

    /// <summary>
    /// Eliminate the state on top of the States stack
    /// </summary>
    public void PopState()
    {
        //Check if the current state is not null and call OnExit 
        //1. Exit the state which is going to be eliminated
        GetCurrentState()?.OnExit();
        //To prevent possible problems with executing the current state 
        //asign it to null
        GetCurrentState().ActiveAction = null;
        //Eliminate the top position of the stack (current state)
        States.Pop();
        //Execute the onEnter of the previous state , if exists
        GetCurrentState().OnEnter();
    }
}
