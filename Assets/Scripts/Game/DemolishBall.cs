using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ZPackage;

public class DemolishBall : Mb
{
    [SerializeField] Rigidbody OlsEhlel;
    [SerializeField] Transform Bombog, projectile;
    [SerializeField] LineRenderer Ols;
    [SerializeField] int projectCount = 20;
    Vector3 FirePos => transform.position + transform.forward * 15;
    [SerializeField] ParticleSystem bigger, longer;
    Camera cam;
    Vector3 targetScale = Vector3.one;
    [SerializeField] SpringJoint joint;
    bool isHanging = false;
    LayerMask brickLayer;
    private void Start()
    {
        Z.Ball = this;
        brickLayer = LayerMask.GetMask("Brick");
        cam = FindObjectOfType<Camera>();
        Z.GM.GamePlay += OnGamePlay;
        joint = GetComponent<SpringJoint>();
    }


    private void OnGamePlay(object sender, EventArgs e)
    {
        rb.isKinematic = false;
        rb.AddForce(new Vector3(0, 0.5f, 0.5f) * 5, ForceMode.VelocityChange);
    }

    // Update is called once per frame
    void Update()
    {
        DrawRope();
        if (IsPlaying)
        {
            if (IsDown)
            {
                HangBall();

            }
            else
            if (IsClick)
            {
                if (OlsEhlel.position.z > transform.position.z)
                {
                    rb.AddForce(new Vector3(0, 0, 1).normalized * 5, ForceMode.Acceleration);
                }

            }
            else if (IsUp)
            {
                ReleaseBall();
            }
            Bombog.transform.localScale = Vector3.Lerp(Bombog.transform.localScale, targetScale, 0.3f);
        }
    }
    private void DrawRope()
    {
        if (isHanging)
        {
            Ols.SetPosition(0, OlsEhlel.position);
            Ols.SetPosition(1, Bombog.position);
        }
        else
        {
            Ols.SetPositions(new Vector3[] { Vector3.zero, Vector3.zero });
        }
    }



    private void HangBall()
    {
        // if (Physics.Raycast(transform.position, new Vector3(0, 0.5f, 0.4f), out RaycastHit hit, 20, HanaLayer))
        // {

        // }
        // Debug.DrawRay(transform.position, new Vector3(0, 0.5f, 0.4f) * 5, Color.red, 2);
        // OlsEhlel.transform.position = hit.point;
        Vector3 OlsPostoin = transform.position + new Vector3(0, 0.6f, 1).normalized * 10;
        OlsEhlel.transform.position = OlsPostoin;
        // joint = gameObject.AddComponent<SpringJoint>();
        joint.connectedBody = OlsEhlel;
        joint.autoConfigureConnectedAnchor = false;
        joint.spring = 400f;
        joint.damper = 200f;
        isHanging = true;
    }

    private void ReleaseBall()
    {
        joint.connectedBody = null;
        joint.spring = 0;
        joint.damper = 0;
        isHanging = false;
        rb.AddForce(new Vector3(0, 0.5f, 0.5f) * 100, ForceMode.Force);
    }
    float expForce = 100;
    float impactRadius => transform.localScale.z + transform.localScale.z * 0.1f;
    private void OnCollisionEnter(Collision other)
    {
        Debug.DrawLine(transform.position, transform.position + -other.GetContact(0).normal * impactRadius, Color.cyan, 10);
        RaycastHit[] bricks = Physics.SphereCastAll(transform.position, impactRadius, -other.GetContact(0).normal, impactRadius, brickLayer);
        foreach (var item in bricks)
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            Z.GM.Wait();
            ReleaseBall();
            StartCoroutine(WaitToStop());
        }
    }
    IEnumerator WaitToStop()
    {
        yield return new WaitUntil(() => rb.velocity.magnitude < 0.5f);
        // Z.GM.LevelComplete(this, 0);
        Z.GM.BuildState();
        Z.Player.ChangeCamera(1);
    }

    internal void Longer()
    {
        // longer.transform.localScale = targetScale;
        longer.Play();
    }

    internal void Bigger()
    {
        print("Bigger");
        targetScale = targetScale * 1.2f;
        // bigger.transform.localScale = targetScale;
        bigger.Play();
        // Bombog.transform.localScale = Bombog.transform.localScale * 1.2f;
    }
}
