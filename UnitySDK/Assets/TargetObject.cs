using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetObject : MonoBehaviour
{
    Target parent;
    void Start()
    {
        parent = transform.parent.GetComponent<Target>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        parent.hasHit = true;
        parent.hitForce = collision.relativeVelocity.magnitude;
    }
}
