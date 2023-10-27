using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public class BaseStateMachine : MonoBehaviour
    {
        [SerializeField] private BaseState _initialState;

        public float walkSpeed = -1.0f;
        public float runSpeed = -2.0f;
        public float hp = 4f;
        private void Awake()
        {
            CurrentState = _initialState;
        }

        public BaseState CurrentState { get; set; }

        private void Update()
        {
            CurrentState.Execute(this);
        }

        private void BloodMinus()
        {

        }
    }
}
