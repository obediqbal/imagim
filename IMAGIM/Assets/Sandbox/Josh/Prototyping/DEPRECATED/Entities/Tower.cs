using DKH.InstancePooling;
using System;
using System.Collections;
using UnityEngine;

namespace DKH.DeprecatedEntitySystem
{
    [Obsolete("The class is deprecated.", true)]
    public class Tower : Entity
    {
        protected override void OnDeath()
        {
            base.OnDeath();
            //InstancePool.Despawn(this);
        }
    }
}