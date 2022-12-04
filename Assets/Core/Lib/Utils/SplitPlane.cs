using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


namespace Lib
{
    public class SplitPlane : MonoBehaviour
    {
        [SerializeField] private MeshFilter _meshFilter;
        [SerializeField] private MeshFilter _prefab;
        [SerializeField] private float _partSize = .5f;

        private void Awake()
        {
            var mesh = _meshFilter.mesh;
            var scale = _meshFilter.transform.localScale;
            var vertices = mesh.vertices.Select(v => v.Multiply(scale)).ToArray();
            var triangles = mesh.triangles;

            float minX = float.MaxValue;
            float maxX = float.MinValue;
            float minY = float.MaxValue;
            float maxY = float.MinValue;
            float minZ = float.MaxValue;
            float maxZ = float.MinValue;

            vertices.ForEach(vert =>
            {
                minX = Mathf.Min(minX, vert.x);
                maxX = Mathf.Max(maxX, vert.x);
                minY = Mathf.Min(minY, vert.y);
                maxY = Mathf.Max(maxY, vert.y);
                minZ = Mathf.Min(minZ, vert.z);
                maxZ = Mathf.Max(maxZ, vert.z);
            });

            // Сделать меши в нахлёст, меши будут иметь общие края
             int left = Mathf.FloorToInt(minX);
             int right = Mathf.CeilToInt(maxX);
             int top = Mathf.FloorToInt(minY);
             int bottom = Mathf.CeilToInt(maxY);
             int up = Mathf.FloorToInt(minZ);
             int down = Mathf.CeilToInt(maxZ);
             
             int totalColumns = Mathf.CeilToInt((right - left) / _partSize) + 1;
             int totalRows = Mathf.CeilToInt((bottom - top) / _partSize) + 1;
             int totalDepth = Mathf.CeilToInt((down - up) / _partSize) + 1;
             
             var pool = new MeshPart[totalColumns, totalRows, totalDepth];
             int vertexStep = 3;
             var localTriangles = new int[vertexStep];
             
             var meshPartSet = new HashSet<MeshPart>();
             
             for (int i = 0; i < triangles.Length; i += vertexStep)
             {
                 meshPartSet.Clear();
             
                 for (int t = 0; t < vertexStep; t++)
                 {
                     int vertIndex = triangles[i + t];
                     var vert = vertices[vertIndex];
             
                     int col = Mathf.FloorToInt((vert.x - left) / _partSize);
                     int row = Mathf.FloorToInt((vert.y - top) / _partSize);
                     int depth = Mathf.FloorToInt((vert.z - up) / _partSize);

                     pool[col, row, depth] ??= new MeshPart();
                     meshPartSet.Add(pool[col, row, depth]);
                     localTriangles[t] = vertIndex;
                 }
             
                 meshPartSet.ForEach(part => part.Triangles.AddRange(localTriangles));
             }
            
            //Вариант без общего края, стык в стык
            // float left = minX;
            // float right = maxX;
            // float top = minY;
            // float bottom = maxY;
            // float up = minZ;
            // float down = maxZ;
            //
            // int totalColumns = Mathf.CeilToInt((right - left) / _partSize) + 1;
            // int totalRows = Mathf.CeilToInt((bottom - top) / _partSize) + 1;
            // int totalDepth = Mathf.CeilToInt((down - up) / _partSize) + 1;
            //
            // var pool = new MeshPart[totalColumns, totalRows, totalDepth];
            // int vertexStep = 3;
            // var localTriangles = new int[vertexStep];
            //
            // for (int i = 0; i < triangles.Length; i += vertexStep)
            // {
            //     int _col = 0;
            //     int _row = 0;
            //     int _depth = 0;
            //     for (int t = 0; t < vertexStep; t++)
            //     {
            //         int vertIndex = triangles[i + t];
            //         var vert = vertices[vertIndex];
            //
            //         int col = Mathf.FloorToInt((vert.x - left) / _partSize);
            //         int row = Mathf.FloorToInt((vert.y - top) / _partSize);
            //         int depth = Mathf.FloorToInt((vert.z - up) / _partSize);
            //
            //         _col += col;
            //         _row += row;
            //         _depth += depth;
            //         localTriangles[t] = vertIndex;
            //     }
            //
            //     _col /= vertexStep;
            //     _row /= vertexStep;
            //     _depth /= vertexStep;
            //
            //     pool[_col, _row, _depth] ??= new MeshPart();
            //     pool[_col, _row, _depth].Triangles.AddRange(localTriangles);
            // }

            foreach (var meshPart in pool)
            {
                if (meshPart == null)
                    continue;

                meshPart.SetupVertices(vertices);

                var meshFilter = Instantiate(_prefab, transform);
                meshFilter.mesh = new Mesh
                {
                    vertices = meshPart.Vertices.ToArray(),
                    triangles = meshPart.Triangles.ToArray()
                };
                meshFilter.GetComponent<MeshCollider>().sharedMesh = meshFilter.mesh;
                meshFilter.transform.position = _meshFilter.transform.position;
                meshFilter.mesh.Optimize();
                meshFilter.mesh.RecalculateBounds();
                meshFilter.mesh.RecalculateNormals();

                //Для шейдера использующего карты нормалей
                // meshFilter.mesh.RecalculateTangents();
            }

            Destroy(_prefab.gameObject);
            Destroy(_meshFilter.gameObject);
            Destroy(this);
            //physics warming up
            Physics.OverlapSphereNonAlloc(Vector3.zero, 0, new Collider[] { });
        }

        private class MeshPart
        {
            public List<int> Triangles = new(1024);
            public List<Vector3> Vertices = new(2048);

            public void SetupVertices(IReadOnlyList<Vector3> vertices)
            {
                var oldIndexesSet = new HashSet<int>();
                oldIndexesSet.AddRange(Triangles);

                var indexesOld = oldIndexesSet.OrderBy(i => i);
                var indexes = new Dictionary<int, int>();

                indexesOld.ForEachIndexed((value, index) =>
                {
                    indexes[value] = index;
                    Vertices.Add(vertices[value]);
                });

                for (int i = 0; i < Triangles.Count; i++)
                    Triangles[i] = indexes[Triangles[i]];
            }
        }
    }
}