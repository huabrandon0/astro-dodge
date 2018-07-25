using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class GridMesh2 : MonoBehaviour
{
    public int _width;
    public int _length;
    public float _cellWidth;
    public float _cellLength;
    public float _thickness;

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

        float widthOffset = _width * _cellWidth/ -2f;
        float lengthOffset = _length  * _cellLength / -2f;
        float thicknessOffset = _thickness / -2f;

        for (int i = 0; i < _width + 1; i++)
        {
            float offset = widthOffset + thicknessOffset;
            vertices.Add(new Vector3(offset + i * _cellWidth, 0, lengthOffset + thicknessOffset));
            vertices.Add(new Vector3(offset + i * _cellWidth + _thickness, 0, lengthOffset + thicknessOffset));
            vertices.Add(new Vector3(offset + i * _cellWidth, 0, lengthOffset + _length * _cellLength - thicknessOffset));
            vertices.Add(new Vector3(offset + i * _cellWidth + _thickness, 0, lengthOffset + _length * _cellLength - thicknessOffset));

            // Tris
            indices.Add(4 * i + 0);
            indices.Add(4 * i + 2);
            indices.Add(4 * i + 1);

            indices.Add(4 * i + 2);
            indices.Add(4 * i + 3);
            indices.Add(4 * i + 1);
        }

        for (int i = 0; i < _length + 1; i++)
        {
            float offset = lengthOffset + thicknessOffset;
            vertices.Add(new Vector3(widthOffset + thicknessOffset, 0, offset + i * _cellLength));
            vertices.Add(new Vector3(widthOffset + _width * _cellWidth - thicknessOffset, 0, offset + i * _cellLength));
            vertices.Add(new Vector3(widthOffset + thicknessOffset, 0, offset + i * _cellLength + _thickness));
            vertices.Add(new Vector3(widthOffset + _width * _cellWidth - thicknessOffset, 0, offset + i * _cellLength + _thickness));

            // Tris
            indices.Add((_width + 1) * 4 + 4 * i + 0);
            indices.Add((_width + 1) * 4 + 4 * i + 2);
            indices.Add((_width + 1) * 4 + 4 * i + 1);

            indices.Add((_width + 1) * 4 + 4 * i + 2);
            indices.Add((_width + 1) * 4 + 4 * i + 3);
            indices.Add((_width + 1) * 4 + 4 * i + 1);
        }

        mesh.vertices = vertices.ToArray();
        mesh.SetIndices(indices.ToArray(), MeshTopology.Triangles, 0);
        filter.mesh = mesh;

        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
        meshRenderer.material = mat;
    }
}
