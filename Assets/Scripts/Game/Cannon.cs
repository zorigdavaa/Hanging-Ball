using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZPackage;

public class Cannon : Mb
{
    [SerializeField] Sum sumPf;
    [SerializeField] Transform InsPos;
    [SerializeField] Transform Body;
    [SerializeField] LineRenderer projectile;
    [SerializeField] int projectCount = 20;
    [SerializeField] GameObject BackFire;
    Vector3 FirePos => InsPos.position + transform.forward * 50;
    Rigidbody sum;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        if (IsPlaying && sum && FireAble)
        {
            if (IsUp)
            {
                Fire();
            }
            // if (IsDown)
            // {
            //     projectile.gameObject.SetActive(true);
            // }
            // else if (IsClick)
            // {
            //     DrawProjectile();
            // }
            // else if (IsUp)
            // {
            //     projectile.gameObject.SetActive(false);
            // }
        }
    }

    private void DrawProjectile()
    {
        projectile.positionCount = projectCount;
        Vector3 velocity = Statics.CalculateVelocity(FirePos, InsPos.position, 1);
        for (int i = 0; i < projectCount; i++)
        {
            float time = ((float)i + 1f) / (float)projectCount;// 1second gej uzew
            Vector3 timedPos = Statics.CalculatePositionWithVelocity(InsPos.position, velocity, time);
            projectile.SetPosition(i, timedPos);
        }
    }

    public void Fire()
    {
        if (sum)
        {
            BackFire.gameObject.SetActive(false);
            sum.gameObject.SetActive(true);
            sum.transform.position = InsPos.position;
            // Vector3 velocity = Statics.CalculateVelocity(FirePos, InsPos.position, 1);
            sum.GetComponent<Rigidbody>().velocity = Vector3.zero;
            Vector3 velocity = InsPos.forward * 25;
            sum.GetComponent<Rigidbody>().AddForce(velocity, ForceMode.VelocityChange);
            sum = null;
            WaitAndCall(() =>
            {
                GetComponent<SphereCollider>().enabled = true;

            });
        }
    }
    void WaitAndCall(Action Action, float Wait = 1)
    {
        StartCoroutine(LocalCor());
        IEnumerator LocalCor()
        {
            yield return new WaitForSeconds(Wait);
            Action();
        }
    }
    bool FireAble = false;
    private void OnTriggerEnter(Collider other)
    {
        DemolishBall ball = other.attachedRigidbody?.GetComponent<DemolishBall>();
        if (ball)
        {
            sum = other.attachedRigidbody;
            sum.velocity = Vector3.zero;
            sum.gameObject.SetActive(false);
            ball.ReleaseBall();
            GetComponent<SphereCollider>().enabled = false;
            WaitAndCall(() =>
            {
                FireAble = true;
                BackFire.gameObject.SetActive(true);
            });
        }
    }

    public void Rotate()
    {
        float currentRotation = Mathf.PingPong(Time.time * 30, 105) - 15;
        Body.localRotation = Quaternion.Euler(0, 0, -currentRotation);
    }
}
