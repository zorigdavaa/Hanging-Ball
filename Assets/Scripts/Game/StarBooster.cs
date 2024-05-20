using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBooster : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        DemolishBall ball = other.attachedRigidbody?.GetComponent<DemolishBall>();
        if (ball)
        {
            ball.Invincible(5);
            Destroy(transform.parent.gameObject);
        }
    }
}
