using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShot : BaseShooter
{
    [SerializeField] Sum sumPf;
    [SerializeField] Transform InsPos;
    [SerializeField] LineRenderer projectile;
    [SerializeField] int projectCount = 20;
    Vector3 FirePos => transform.position + transform.forward * 15;

    public override void Fire()
    {
        Sum insSum = Instantiate(sumPf, InsPos.position, Quaternion.identity);
        // insSum.GetComponent<Rigidbody>().AddForce(InsPos.forward * Power);
        Vector3 velocity = Statics.CalculateVelocity(FirePos, InsPos.position, 1);
        insSum.GetComponent<Rigidbody>().AddForce(velocity, ForceMode.VelocityChange);
        Destroy(insSum.gameObject, 3);
    }

    public override void Rotate(Vector3 rotation)
    {
        transform.Rotate(rotation);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlaying)
        {
            if (IsDown)
            {
                projectile.gameObject.SetActive(true);
            }
            else if (IsClick)
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
            else if (IsUp)
            {
                projectile.gameObject.SetActive(false);
            }
        }
    }
}
