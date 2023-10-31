using MoreMountains.Feedbacks;
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
        public float currenthp;
        private MMFeedbacks _feedbacks;
        private void Awake()
        {
            CurrentState = _initialState;
        }

        private void Start()
        {
            currenthp = hp;
            _feedbacks = GetComponent<MMFeedbacks>();
        }
        public BaseState CurrentState { get; set; }

        private void Update()
        {
            CurrentState.Execute(this);

            if(currenthp <= 0)
            {
                Destroy(gameObject);
            }
            Debug.Log(currenthp);
        }

        public void BloodMinus()
        {
            currenthp -= 1;
            _feedbacks.PlayFeedbacks();
        }
    }
}
