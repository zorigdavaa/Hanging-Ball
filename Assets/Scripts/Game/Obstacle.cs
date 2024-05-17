using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] Transform MoveObj;
    [SerializeField] Vector3 Axis;
    [SerializeField] float Speed;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void Update()
    {
        MoveObj.RotateAround(transform.position, Axis, Speed);
    }

}
