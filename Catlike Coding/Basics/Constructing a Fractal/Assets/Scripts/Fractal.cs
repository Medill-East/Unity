using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fractal : MonoBehaviour
{

    public Mesh mesh;
    public Material material;

    public int maxDepth;
    public float childScale;

    private int depth;
    private static Vector3[] childrenDirections =
    {
        Vector3.up,
        Vector3.right,
        Vector3.left,
        Vector3.forward,
        Vector3.back
    };
    private static Quaternion[] childrenOrientations =
    {
        Quaternion.identity,
        Quaternion.Euler(0f,0f,-90f),
        Quaternion.Euler(0f,0f,90f),
        Quaternion.Euler(90f,0f,0f),
        Quaternion.Euler(-90f,0f,0f),

    };

    // Use this for initialization
    void Start()
    {
        gameObject.AddComponent<MeshFilter>().mesh = mesh;
        gameObject.AddComponent<MeshRenderer>().material = material;
        if (depth < maxDepth)
        {
            StartCoroutine(CreateChildren());
        }
    }

    private IEnumerator CreateChildren()
    {
        for(int i=0;i<childrenDirections.Length;i++)
        {
            yield return new WaitForSeconds(0.5f);
            new GameObject("Fractal Child").AddComponent<Fractal>().Initialize(this,i);
        }
    }

    private void Initialize(Fractal parent,int childIndex)
    {
        mesh = parent.mesh;
        material = parent.material;
        maxDepth = parent.maxDepth;
        childScale = parent.childScale;
        depth = parent.depth + 1;
        transform.parent = parent.transform;
        transform.localScale = Vector3.one * childScale;
        transform.localPosition = childrenDirections[childIndex] * (0.5f + 0.5f * childScale);
        transform.localRotation = childrenOrientations[childIndex];
    }

}
