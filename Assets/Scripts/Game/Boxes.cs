using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boxes : MonoBehaviour
{
    [SerializeField] Brick[] bricks;
    private void Start()
    {
        bricks = GetComponentsInChildren<Brick>();
    }
    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.CompareTag("Player"))
        {
            if (other.attachedRigidbody.velocity.magnitude > 10)
            {
                foreach (var item in bricks)
                {
                    item.SetFRee();
                }
                Destroy(this);
            }

        }
    }

}
