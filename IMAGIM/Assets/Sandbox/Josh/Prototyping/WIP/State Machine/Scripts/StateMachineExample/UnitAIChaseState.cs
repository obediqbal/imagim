using DKH.StateMachineSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKH.Debugging
{
    [CreateAssetMenu(fileName = "Unit Chase State", menuName = "SO/States/Unit/Chase")]
    public class UnitAIChaseState : State<UnitAI>
    {
        public override string Name => "Chase";
    }
}