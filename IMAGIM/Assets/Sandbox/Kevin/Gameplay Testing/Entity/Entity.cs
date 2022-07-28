using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu(menuName:"Game Units/Entity")]
public abstract class Entity : MonoBehaviour
{
    public abstract void OnCreate();
    public abstract void IsDead();
    public abstract void Team();
}
