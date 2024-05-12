using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MB
{
    [SerializeField] Rigidbody OlsEhlel;
    [SerializeField] Transform Bombog, projectile;
    [SerializeField] LineRenderer Ols;
    [SerializeField] int projectCount = 20;
    Vector3 FirePos => transform.position + transform.forward * 15;
    [SerializeField] ParticleSystem bigger, longer;
    Camera cam;
    Vector3 targetScale = Vector3.one;
    SpringJoint joint;
    bool isHanging = false;
    LayerMask HanaLayer;

    // Start is called before the first frame update
    void Start()
    {
        A.Player = this;
        cam = FindObjectOfType<Camera>();
        GameController.Instance.OnGamePlay += OnGamePlay;
        HanaLayer = LayerMask.GetMask("Hana");
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
                // projectile.gameObject.SetActive(true);
                if (Physics.Raycast(transform.position, new Vector3(0, 0.5f, 0.4f), out RaycastHit hit, 20, HanaLayer))
                {
                    Debug.DrawRay(transform.position, new Vector3(0, 0.5f, 0.4f) * 5, Color.red, 2);
                    OlsEhlel.transform.position = hit.point;
                    joint = gameObject.AddComponent<SpringJoint>();
                    joint.connectedBody = OlsEhlel;
                    joint.autoConfigureConnectedAnchor = false;
                    // joint.xMotion = ConfigurableJointMotion.Locked;
                    // joint.yMotion = ConfigurableJointMotion.Locked;
                    // joint.zMotion = ConfigurableJointMotion.Locked;
                    // float distance = Vector3.Distance(transform.position, OlsEhlel.position);
                    // joint.anchor = new Vector3(0, 5, 0);
                    // joint.connectedAnchor = new Vector3(0, 2, 0);
                    joint.spring = 100;
                    joint.damper = 50f;
                    isHanging = true;
                }
            }
            else
            if (IsClick)
            {
                if (OlsEhlel.position.z > transform.position.z)
                {
                    rb.AddForce(new Vector3(0, 0, 1).normalized * 5, ForceMode.Acceleration);
                }
                // Vector3 mousePos = Input.mousePosition;
                // mousePos.z = 5;
                // Vector3 worldPosition = cam.ScreenToWorldPoint(mousePos);
                // // Debug.DrawLine(cam.transform.position, worldPosition, Color.red, 1);
                // // Vector3 dir = transform.position - Bombog.transform.position;
                // Vector3 dirRaw = worldPosition - Bombog.transform.position;
                // rb.velocity = dirRaw;
            }
            else if (IsUp)
            {
                Destroy(joint);
                // joint.xMotion = ConfigurableJointMotion.Free;
                // joint.yMotion = ConfigurableJointMotion.Free;
                // joint.zMotion = ConfigurableJointMotion.Free;
                isHanging = false;
                rb.AddForce(new Vector3(0, 0.5f, 0.5f) * 100, ForceMode.Force);
            }
            Bombog.transform.localScale = Vector3.Lerp(Bombog.transform.localScale, targetScale, 0.3f);
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