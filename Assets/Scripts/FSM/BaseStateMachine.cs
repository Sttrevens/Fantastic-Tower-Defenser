using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public class BaseStateMachine : MonoBehaviour
    {
        [SerializeField] private BaseState _initialState;

        public Rigidbody2D playerRb;
        private void Awake()
        {
            CurrentState = _initialState;
        }

        public BaseState CurrentState { get; set; }

        private void Update()
        {
            CurrentState.Execute(this);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                // 获取玩家的Rigidbody2D
                playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

            }
        }
    }
}
