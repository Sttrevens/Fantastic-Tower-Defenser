using FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FSM
{
    [CreateAssetMenu(menuName = "FSM/Actions/EACrazyMove")]
    public class EACrazyMove : FSMAction
    {
        public override void Execute(BaseStateMachine stateMachine)
        {
            var runSpeed = stateMachine.runSpeed;
            var rb = stateMachine.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(runSpeed, rb.velocity.y);
        }
    }
}

