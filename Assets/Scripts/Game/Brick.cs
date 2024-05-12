using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public ChimneyCreator creator;
    [SerializeField] ParticleSystem particle;
    Rigidbody rb;
    LayerMask brickLayer;
    bool isFree = false;
    // public bool debug = false;
    public bool bottomCheck => A.Player.transform.position.y < transform.position.y;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        brickLayer = LayerMask.GetMask("Brick");
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Sum") && GetComponent<Rigidbody>().isKinematic)
        {
            SetFRee();
        }
    }
    float timer = 10;
    // private void LateUpdate()
    // {
    //     // timer -= Time.deltaTime;
    //     // // A.Player.transform.position.y < transform.position.y
    //     // if (bottomCheck && !isFree && timer < 0)
    //     // {
    //     //     timer = Random.Range(1f, 2f);
    //     //     gameObject.layer = 2;
    //     //     if (!Physics.CheckSphere(transform.position + new Vector3(0, -0.5f, 0), 0.25f, brickLayer))
    //     //     {
    //     //         // foreach (var item in Physics.OverlapBox(transform.position, new Vector3(2, 2, 2), transform.rotation, brickLayer))
    //     //         // {
    //     //         //     if (item.GetComponent<Brick>())
    //     //         //     {
    //     //         //         SetFRee();
    //     //         //     }
    //     //         // }
    //     //         SetFRee();
    //     //     }
    //     //     gameObject.layer = 6;
    //     //     // foreach (var item in Physics.OverlapSphere(transform.position + new Vector3(0, -0.5f, 0), 0.25f, brickLayer))
    //     //     // {
    //     //     //     if (!item.GetComponent<Brick>() && item.gameObject != this.gameObject)
    //     //     //     {
    //     //     //         SetFRee();
    //     //     //     }
    //     //     // }
    //     // }
    // }
    // private void OnDrawGizmos()
    // {
    //     if (debug)
    //     {
    //         Gizmos.DrawSphere(transform.position + new Vector3(0, -0.5f, 0), 0.25f);
    //     }
    // }

    public void SetFRee()
    {
        if (!isFree)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
            creator?.RemoveBrickFromList(this);
            if (particle != null)
            {
                particle.Play();
            }
            isFree = true;
        }
    }
}
