using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Stuff : PooledObject {

    public Rigidbody Body { get; private set; }
    public MeshRenderer[] meshRenderers;


    private void Awake()
    {
        Body = GetComponent<Rigidbody>();
        meshRenderers = GetComponentsInChildren<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider enteredCollider)
    {
        if(enteredCollider.CompareTag("Kill Zone"))
        {
            //Destroy(this.gameObject);
            ReturnToPool();
        }
    }

    public void SetMaterial (Material m)
    {
        for(int i=0;i<meshRenderers.Length;i++)
        {
            meshRenderers[i].material = m;
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        ReturnToPool();
    }
}
