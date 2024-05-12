using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField] Transform grx;
    [SerializeField] Vector3 speed;
    // Start is called before the first frame update
    void Start()
    {
        grx = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        grx.Rotate(speed);
    }
}
