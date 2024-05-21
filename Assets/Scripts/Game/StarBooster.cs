using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBooster : MonoBehaviour
{
    bool Protect = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Protect)
        {
            foreach (var item in Physics.OverlapSphere(transform.position, 3))
            {
                if (item.attachedRigidbody != null)
                {
                    item.attachedRigidbody.isKinematic = false;
                }
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        DemolishBall ball = other.attachedRigidbody?.GetComponent<DemolishBall>();
        if (ball)
        {
            GameObject parent = transform.parent.gameObject;
            transform.SetParent(null);
            Destroy(parent);
            ball.Invincible(5);
            transform.SetParent(ball.transform);
            Destroy(gameObject, 5);
            foreach (Transform item in transform)
            {
                item.gameObject.SetActive(false);
            }
            Protect = true;
        }
    }
}
