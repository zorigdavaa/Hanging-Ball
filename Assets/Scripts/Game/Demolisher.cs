using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demolisher : BaseShooter
{
    [SerializeField] Transform OlsEhlel, Bombog, projectile;
    [SerializeField] LineRenderer Ols;
    [SerializeField] int projectCount = 20;
    Vector3 FirePos => transform.position + transform.forward * 15;
    [SerializeField] ParticleSystem bigger, longer;
    bool playing = false;
    Camera cam;
    Vector3 targetScale = Vector3.one;
    public override void Fire()
    {
        // if (playing && Bombog.transform.position.z < -5)
        // {
        //     // projectile.gameObject.SetActive(false);
        //     // Bombog.GetComponent<Rigidbody>().velocity = projectile.transform.forward * 5;
        //     // Bombog.GetComponent<Rigidbody>().velocity = (new Vector3(-Bombog.position.x * 5, Bombog.position.y, 0) - Bombog.transform.position).normalized * 5;
        // }
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = FindObjectOfType<Camera>();
        // GameController.Instance.OnGamePlay += OnGamePlay;
    }

    private void OnGamePlay(object sender, EventArgs e)
    {
        Bombog.GetComponent<Rigidbody>().isKinematic = false;
        playing = true;
    }

    private void DrawRope()
    {
        Ols.SetPosition(0, OlsEhlel.position);
        Ols.SetPosition(1, Bombog.position);
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
            }
            else
            if (IsClick)
            {
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = 5;
                Vector3 worldPosition = cam.ScreenToWorldPoint(mousePos);
                // Debug.DrawLine(cam.transform.position, worldPosition, Color.red, 1);
                // Vector3 dir = transform.position - Bombog.transform.position;
                Vector3 dirRaw = worldPosition - Bombog.transform.position;
                Bombog.GetComponent<Rigidbody>().velocity = dirRaw;
                // if (Bombog.transform.position.z < -4)
                // {
                //     Bombog.GetComponent<Rigidbody>().velocity = Vector3.Lerp(Bombog.GetComponent<Rigidbody>().velocity, dirRaw, 0.5f);
                // }
                // else
                // {
                //     Bombog.GetComponent<Rigidbody>().velocity += dirRaw.normalized;
                // }
            }
            Bombog.transform.localScale = Vector3.Lerp(Bombog.transform.localScale, targetScale, 0.3f);
        }
    }

    public override void Rotate(Vector3 rotation)
    {
        rotation.x = 0;
        projectile.transform.Rotate(rotation);
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
