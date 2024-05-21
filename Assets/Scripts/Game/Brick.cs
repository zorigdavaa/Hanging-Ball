using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZPackage;

public class Brick : MonoBehaviour
{
    // public ChimneyCreator creator;
    [SerializeField] ParticleSystem particle;
    [SerializeField] Collider coll;
    public Rigidbody rb;
    // LayerMask brickLayer;
    bool isFree = false;
    Camera cam;
    // public bool debug = false;
    public bool bottomCheck => Z.Player.transform.position.y < transform.position.y;
    private void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
        // brickLayer = LayerMask.GetMask("Brick");
    }
    // private void OnCollisionEnter(Collision other)
    // {
    //     if (other.gameObject.CompareTag("Player") && GetComponent<Rigidbody>().isKinematic)
    //     {
    //         SetFRee();
    //     }
    // }
    // float timer = 10;
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
        if (!isFree )
        {
            // rb.isKinematic = false;
            rb.useGravity = true;
            rb.drag = 0;
            rb.angularDrag = 0.05f;
            // creator?.RemoveBrickFromList(this);
            if (particle != null)
            {
                particle.Play();
            }
            isFree = true;
            InsMoneyAndGotoTop();
        }
    }
    private void InsMoneyAndGotoTop()
    {
        StartCoroutine(localCoroutine());
        IEnumerator localCoroutine()
        {
            float time = 0;
            float duration = 0.8f;
            float t;
            // yield return new WaitForSeconds(Random.Range(3f, 5f));
            float startTime = Time.time;
            yield return new WaitUntil(() => transform.position.z < Z.Ball.transform.position.z - 5.5f || Time.time - startTime > 4);
            rb.isKinematic = true;
            coll.enabled = false;
            while (time < duration)
            {
                time += Time.deltaTime;
                t = time / duration;
                Vector3 toPoint = cam.ScreenToWorldPoint(Z.CanM.Brick.transform.position + new Vector3(0, 0, 8f));
                transform.position = Vector3.Lerp(transform.position, toPoint, t);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, t);
                transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * 0.3f, t);
                // Mathf.Lerp(0, 1, t);
                yield return null;
            }
            Z.GM.BrickCount++;
            Destroy(gameObject);
        }
    }
}
