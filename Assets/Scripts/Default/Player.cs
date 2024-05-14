using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZPackage;
using ZPackage.Helper;
using Random = UnityEngine.Random;
using UnityEngine.Pool;
using ZPackage.Utility;
using System.Linq;

public class Player : Mb
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
    LayerMask HanaLayer;

    // Start is called before the first frame update
    void Start()
    {
        cam = FindObjectOfType<Camera>();
        Z.GM.GamePlay += OnGamePlay;
        HanaLayer = LayerMask.GetMask("Hana");
        joint = GetComponent<SpringJoint>();
    }

    private void OnGamePlay(object sender, EventArgs e)
    {
        rb.isKinematic = false;
        rb.AddForce(new Vector3(0, 0.5f, 0.5f) * 5, ForceMode.VelocityChange);
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

    // Update is called once per frame
    void Update()
    {
        DrawRope();
        if (IsPlaying)
        {
            if (IsDown)
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
                joint.connectedBody = null;
                joint.spring = 0;
                joint.damper = 0;
                isHanging = false;
                rb.AddForce(new Vector3(0, 0.5f, 0.5f) * 100, ForceMode.Force);
            }
            Bombog.transform.localScale = Vector3.Lerp(Bombog.transform.localScale, targetScale, 0.3f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            // GameController.
        }
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
