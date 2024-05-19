using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownMove : MonoBehaviour
{
    [SerializeField] Transform moveObj;
    [SerializeField] Vector3 Pos1;
    [SerializeField] Vector3 Pos2;
    [SerializeField] float speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        Pos1 += moveObj.position;
        Pos2 += moveObj.position;
    }
    float t;
    // Update is called once per frame
    void Update()
    {
        t = Mathf.PingPong(Time.time * speed, 1);
        moveObj.position = Vector3.Lerp(Pos1, Pos2, t);
    }
}
