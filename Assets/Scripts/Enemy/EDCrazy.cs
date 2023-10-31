using FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    [CreateAssetMenu(menuName = "FSM/Decisions/EDCrazy")]
    public class EDCrazy : Decision
    {
        public override bool Decide(BaseStateMachine stateMachine)
        {
            var hp = stateMachine.hp;
            var chp = stateMachine.currenthp;

            if (chp <= hp / 2)
            { return true; }
            else
            { return false; }
            
        }

    }
}
