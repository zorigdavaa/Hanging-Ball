using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZPackage;

public abstract class BaseShooter : Mb, IFireAble
{
    public abstract void Fire();
    public abstract void Rotate(Vector3 rotation);
}
