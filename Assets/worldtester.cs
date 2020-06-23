using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

[RequireComponent(typeof(MeshFilter))]
public class worldtester : MonoBehaviour
{

    [SerializeField] public int xSize = 300, zSize = 300, breakZ = 300;
    private int prevxSize = 300, prevzSize = 300;
    [SerializeField] public MeshFilter filter = null;
    int[] triangles;
    // Start is called before the first frame update
    void Start()
    {
        filter.sharedMesh = new Mesh();
        filter.sharedMesh.vertices = generateVerticies();
        StartCoroutine(generateTriangles());
    }

    // Update is called once per frame
    void Update()
    {
        filter.sharedMesh.triangles = triangles;

        if (xSize != prevxSize || zSize != prevzSize)
        {
            prevxSize = xSize;
            prevzSize = zSize;
            StopCoroutine(generateTriangles()); Start();
        }
    }


    private Vector3[] generateVerticies()
    {
        Vector3[] verts = new Vector3[(xSize + 1) * (zSize + 1)];       // Create temp array for verticies

        for (int vert = 0, zItt = 0; zItt <= zSize; zItt++)         // Itterate over z,
            for (int xItt = 0; xItt <= xSize; xItt++)
            {              // Itterate over x,
                float y = Mathf.PerlinNoise(xItt * 0.1f, zItt * 0.1f) * 2f;
                verts[vert] = new Vector3(xItt, y, zItt);             // Creating a vert at x, 0, y.
                vert++;                                             // increase itterator.
            }
        return verts;
    }

    IEnumerator generateTriangles()
    {
        triangles = new int[xSize * zSize * 6];
        int vert = 0;
        int tris = 0;
        Profiler.BeginSample("[World Gen Test] Tri generation", this);

        for (int zitt = 0; zitt < zSize; zitt++)
        {
            if (zitt == breakZ){
                Debug.Log("z break");
            }
            for (int xitt = 0; xitt < xSize; xitt++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }
        yield return triangles;
        Profiler.EndSample();
        Debug.Log("Tris: " + tris + " verts: " + vert);
    }
}
