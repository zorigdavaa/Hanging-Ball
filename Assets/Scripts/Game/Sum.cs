using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sum : MonoBehaviour
{
    [SerializeField] ParticleSystem explosion;
    float impactRadius = 2, expForce = 500;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision other)
    {
        // Vector3 dir = transform.position - other.transform.position;
        // foreach (var item in Physics.SphereCastAll(transform.position + dir, impactRadius, dir, 0.1f))
        // {
        //     // item.GetComponent<Rigidbody>().isKinematic = false;
        //     if (item.rigidbody)
        //     {
        //         item.rigidbody.AddExplosionForce(5f, transform.position + dir, impactRadius);
        //         Brick brick = item.rigidbody.GetComponent<Brick>();
        //         if (brick)
        //         {
        //             brick.SetFRee();
        //         }
        //     }
        // }
        // Debug.DrawLine(transform.position, transform.position + dir * 2, Color.cyan, 10);

        foreach (var item in Physics.OverlapSphere(transform.position /*+ other.GetContact(0).normal * 0.5f*/, impactRadius))
        {
            // item.GetComponent<Rigidbody>().isKinematic = false;
            if (item.attachedRigidbody)
            {
                item.attachedRigidbody.AddExplosionForce(expForce, transform.position, impactRadius);
                Brick brick = item.attachedRigidbody.GetComponent<Brick>();
                if (brick)
                {
                    brick.SetFRee();
                }
            }
        }
        Debug.DrawLine(transform.position, transform.position + other.GetContact(0).normal * 2, Color.cyan, 10);
        explosion.Play();
        explosion.transform.SetParent(null);
        Destroy(explosion.gameObject, 2);
        Destroy(gameObject);
    }
}
