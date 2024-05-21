using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBooster : MonoBehaviour
{
    bool Protect = false;
    [SerializeField] float Duration = 10;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void FixedUpdate()
    {
        if (Protect)
        {
            foreach (var item in Physics.OverlapSphere(transform.position, 2))
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
            ball.Invincible(Duration);
            transform.SetParent(ball.transform);
            Destroy(gameObject, Duration);
            foreach (Transform item in transform)
            {
                item.gameObject.SetActive(false);
            }
            Protect = true;
        }
    }
}
