using DKH.StateMachineSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DKH.Debugging
{
    public class UnitAI : StateMachineMono<UnitAI>
    {
        Unit unit;
        public override UnitAI data => this;
    }
}