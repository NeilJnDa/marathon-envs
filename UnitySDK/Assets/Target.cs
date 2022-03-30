using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Transform obsTarget;
    private Rigidbody obsTargetRigidbody;
    public Vector3 obsTargetPosition;
    public bool reset = false;
    public bool hasHit = true;
    public float hitForce;
    private float maxHitForce = 15f;
    public struct TransformDate{
        public Vector3 postion;
        public Quaternion rotation;
    }
    // Start is called before the first frame update
    List<TransformDate> init = new List<TransformDate>();
    void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            TransformDate tempData = new TransformDate();
            tempData.postion = transform.GetChild(i).transform.position;
            tempData.rotation = transform.GetChild(i).transform.rotation;
            init.Add(tempData);
            if(transform.GetChild(i).name == "Target")
            {
                obsTarget = transform.GetChild(i).transform;
                obsTargetPosition = transform.GetChild(i).position;
                obsTargetRigidbody = transform.GetChild(i).GetComponent<Rigidbody>();
            }
        }
        if(obsTargetPosition == null)
        {
            obsTargetPosition = transform.GetChild(0).position;
            obsTarget = transform.GetChild(0).transform;
            obsTargetRigidbody = transform.GetChild(0).GetComponent<Rigidbody>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ResetTarget()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).transform.position = init[i].postion;
            transform.GetChild(i).transform.rotation = init[i].rotation;
            if (transform.GetChild(i).GetComponent<Rigidbody>())
            {
                transform.GetChild(i).GetComponent<Rigidbody>().velocity = Vector3.zero;
                transform.GetChild(i).GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            }
        }
        obsTargetRigidbody.velocity = Vector3.zero;
        obsTargetRigidbody.angularVelocity = Vector3.zero;
        obsTargetRigidbody.mass = Random.Range(0.5f, 3f);

        hitForce = 0;
        hasHit = false;
    }
    private void OnValidate()
    {
        if(Application.isPlaying && reset == true)
        {
            ResetTarget();
            reset = false;
        }
    }

    public float HitReward(Vector3 weaponPos)
    {
        if (hasHit)
        {
            return Mathf.Clamp(hitForce / maxHitForce, 0, 1f);
        }
        else 
        {
            return Mathf.Exp(-5 * Mathf.Pow(Vector3.Distance(obsTarget.transform.position, weaponPos), 2));
        }
    }
    
}
