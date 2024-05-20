using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lvlSegment : MonoBehaviour
{
    [SerializeField] Material mat1;
    [SerializeField] Material mat2;
    public SegType Type;
    [SerializeField] GameObject Deed;
    [SerializeField] GameObject Dood;
    public Transform Start1;
    public Transform End;

}
public enum SegType
{
    None, Brick, Finish, Obs, Booster
}