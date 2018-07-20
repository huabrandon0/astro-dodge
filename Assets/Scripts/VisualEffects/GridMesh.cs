using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class GridMesh : MonoBehaviour
{
    public int _width;
    public int _length;

    void Awake()
    {
        MeshFilter filter = GetComponent<MeshFilter>();
        Material mat = GetComponent<Renderer>().material;
        var mesh = new Mesh();
        var vertices = new List<Vector3>();
        var indices = new List<int>();

        //for (int i = 0; i < _width; i++)
        //{
        //    // Vertical lines (along z)
        //    vertices.Add(new Vector3(i, 0, 0));
        //    vertices.Add(new Vector3(i, 0, _length));

        //    // Horizontal lines (along x)
        //    vertices.Add(new Vector3(0, 0, i));
        //    vertices.Add(new Vector3(_width, 0, i));

        //    indices.Add(4 * i + 0);
        //    indices.Add(4 * i + 1);
        //    indices.Add(4 * i + 2);
        //    indices.Add(4 * i + 3);
        //}

        float widthOffset = _width / -2f;
        float lengthOffset = _length / -2f;

        for (int i = 0; i < _width + 1; i++)
        {
            vertices.Add(new Vector3(widthOffset + i, 0, lengthOffset));
            vertices.Add(new Vector3(widthOffset + i, 0, lengthOffset + _length));
            indices.Add(2 * i + 0);
            indices.Add(2 * i + 1);
        }
        
        for (int i = 0; i < _length + 1; i++)
        {
            vertices.Add(new Vector3(widthOffset, 0, lengthOffset + i));
            vertices.Add(new Vector3(widthOffset + _width, 0, lengthOffset + i));
            indices.Add((_width + 1) * 2 + 2 * i + 0);
            indices.Add((_width + 1) * 2 + 2 * i + 1);
        }

        mesh.vertices = vertices.ToArray(); 
        mesh.SetIndices(indices.ToArray(), MeshTopology.Lines, 0);
        filter.mesh = mesh;

        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
        meshRenderer.material = mat;
    }
}
