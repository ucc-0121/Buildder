using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.ProBuilder.MeshOperations;

public class CreateMesh : MonoBehaviour
{
    public Material quadMaterial;
    // Start is called before the first frame update
    void Start()
    {
        //SingleSideQuad();
        //DoubleSideQuad();
        Ocean();
    }
    public void SingleSideQuad()
    {
        ProBuilderMesh quad = ProBuilderMesh.Create(
            new Vector3[] {
                new Vector3(0f,0f,0f),
                new Vector3(1f,0f,0f),
                new Vector3(0f,1f,0f),
                new Vector3(1f,1f,0f)
            },
            new Face[]
            {
                new Face(new int[]{ 0,2,1}),
                new Face(new int[]{2,3,1 })
            }
            );
        quad.SetMaterial(quad.faces, quadMaterial);
        quad.Refresh();
        quad.ToMesh();
    }
    public void DoubleSideQuad()
    {
        ProBuilderMesh quad = ProBuilderMesh.Create(
            new Vector3[] {
                new Vector3(0f,0f,0f),
                new Vector3(1f,0f,0f),
                new Vector3(0f,1f,0f),
                new Vector3(1f,1f,0f),
                new Vector3(0f,0f,0f),
                new Vector3(1f,0f,0f),
                new Vector3(0f,1f,0f),
                new Vector3(1f,1f,0f)
            },
            new Face[]
            {
                new Face(new int[]{ 0,2,1}),
                new Face(new int[]{ 2,3,1 }),
                new Face(new int[]{ 5,6,4}),
                new Face(new int[]{ 5,7,6 })
            }
            );
        quad.SetMaterial(quad.faces, quadMaterial);
        quad.Refresh();
        quad.ToMesh();
    }
    // Update is called once per frame
    public void Ocean()
    {
        Vector3[] verts = new Vector3[100];
        for (int i = 0; i < 10; ++i)
        {
            for (int j = 0; j < 10; ++j)
            {
                float randomHeight = Random.Range(-0.3f, 0.3f);
                int index = i * 10 + j;
                verts[index].x = i;
                verts[index].y = randomHeight;
                verts[index].z = j;
            }
        }
        List<Face> faces = new List<Face>();
        for (int i = 0; i < 9; ++i)
        {
            for (int j = 0; j < 9; ++j)
            {
                int index1 = i * 10 + j;
                int index2 = (i + 1) * 10 + j;
                int index3 = i * 10 + j + 1;
                Face face1 = new Face(new int[] { index3, index2, index1 });

                index1 = (i + 1) * 10 + j;
                index2 = index1 + 1;
                index3 = i * 10 + j + 1;
                Face face2 = new Face(new int[] { index3, index2, index1 });
                faces.Add(face1);
                faces.Add(face2);
            }
        }
        ProBuilderMesh ocean = ProBuilderMesh.Create(verts, faces);
        ocean.SetMaterial(ocean.faces, quadMaterial);
        ocean.Refresh();
        ocean.ToMesh();
        Debug.Log(ocean.vertexCount);
        VertexEditing.MergeVertices(ocean, new int[] { 0, 1, 2 });
        ocean.ToMesh();
        Debug.Log(ocean.vertexCount);
    }
    void Update()
    {

    }
}