using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseShooter : MB, IFireAble
{
    public abstract void Fire();
    public abstract void Rotate(Vector3 rotation);
}
