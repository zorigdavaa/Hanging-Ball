using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemolishBall : MonoBehaviour
{
    LayerMask brickLayer;
    private void Start()
    {
        brickLayer = LayerMask.GetMask("Brick");
    }
    float expForce = 100;
    float impactRadius => transform.localScale.z + transform.localScale.z * 0.1f;
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
        // Debug.DrawLine(transform.position, transform.position + -other.GetContact(0).normal * impactRadius * 0.5f, Color.cyan, 10);

        // foreach (var item in Physics.OverlapSphere(transform.position, impactRadius, brickLayer))
        // foreach (var item in Physics.OverlapSphere(transform.position /*+ other.GetContact(0).normal * 0.5f*/, impactRadius, brickLayer))
        foreach (var item in Physics.SphereCastAll(transform.position /*+ other.GetContact(0).normal * 0.5f*/, impactRadius, -other.GetContact(0).normal, impactRadius * 0.4f, brickLayer))
        {
            // item.GetComponent<Rigidbody>().isKinematic = false;
            if (item.rigidbody)
            {
                item.rigidbody.AddExplosionForce(expForce, transform.position, impactRadius);
                Brick brick = item.rigidbody.GetComponent<Brick>();
                if (brick)
                {
                    brick.SetFRee();
                }
            }
        }
        // if (other.gameObject.name == "Shal")
        // {
        //     FindObjectOfType<ChimneyCreator>().DestroyAbovePos(Vector3.zero);
        // }
    }
}
