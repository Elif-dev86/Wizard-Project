using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ConeTest : MonoBehaviour
{

    Mesh mesh;

    public Vector3[] vertices;

    public int[] triangles;

    private void Start() 
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }

    private void Update() 
    {
        mesh.vertices = vertices;
    }

    private void OnEnable() 
    {
        var mesh = new Mesh{ name = "Procedural Mesh"};

        mesh.vertices = new Vector3[] {Vector3.zero, Vector3.right, Vector3.up};
        
        vertices = mesh.vertices;

        mesh.triangles = new int[] {0, 1, 2};

        GetComponent<MeshFilter>().mesh = mesh;
    }
}
