using FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FSM
{
    [CreateAssetMenu(menuName = "FSM/Actions/EAMove")]
    public class EAMove : FSMAction
    {
        public override void Execute(BaseStateMachine stateMachine)
        {
            var walkSpeed = stateMachine.walkSpeed;
            var rb = stateMachine.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(walkSpeed, rb.velocity.y);
        }
    }
}
