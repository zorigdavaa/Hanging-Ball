using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lvlSegment : MonoBehaviour
{
    public SegType Type;
    [SerializeField] GameObject Deed;
    [SerializeField] GameObject Dood;
    public Transform Start1;
    public Transform End;

}
public enum SegType
{
    None, Brick, Finish, Obs
}