using MelonLoader;
using MelonLoader.Utils;
using UnityEngine;
using System.IO;

namespace mszbhop
{
    [RegisterTypeInIl2Cpp]
    public class TerrainGenerator : MonoBehaviour
    {
        private void Start()
        {
            int resolution = 256;
            float size = 500f;
            float height = 60f;

            float offsetX = Random.Range(0f, 9999f);
            float offsetZ = Random.Range(0f, 9999f);

            Vector3[] vertices = new Vector3[(resolution + 1) * (resolution + 1)];
            Vector2[] uvs = new Vector2[vertices.Length];

            for (int z = 0; z <= resolution; z++)
            {
                for (int x = 0; x <= resolution; x++)
                {
                    int i = z * (resolution + 1) + x;
                    float fx = (float)x / resolution;
                    float fz = (float)z / resolution;

                    float nx = fx * size + offsetX;
                    float nz = fz * size + offsetZ;

                    float h = 0f;
                    h += Mathf.PerlinNoise(nx * 0.003f, nz * 0.003f) * 0.6f;
                    h += Mathf.PerlinNoise(nx * 0.01f, nz * 0.01f) * 0.3f;
                    h += Mathf.PerlinNoise(nx * 0.04f, nz * 0.04f) * 0.1f;

                    vertices[i] = new Vector3(fx * size - size * 0.5f, h * height, fz * size - size * 0.5f);
                    uvs[i] = new Vector2(fx, fz);
                }
            }

            int[] triangles = new int[resolution * resolution * 6];
            int t = 0;
            for (int z = 0; z < resolution; z++)
            {
                for (int x = 0; x < resolution; x++)
                {
                    int i = z * (resolution + 1) + x;
                    triangles[t++] = i;
                    triangles[t++] = i + resolution + 1;
                    triangles[t++] = i + 1;
                    triangles[t++] = i + 1;
                    triangles[t++] = i + resolution + 1;
                    triangles[t++] = i + resolution + 2;
                }
            }

            Mesh mesh = new Mesh();
            mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.uv = uvs;
            mesh.RecalculateNormals();

            string texturePath = Path.Combine(MelonEnvironment.ModsDirectory, "mszbhop", "grass.png");
            byte[] bytes = File.ReadAllBytes(texturePath);
            Texture2D tex = new Texture2D(2, 2, TextureFormat.RGBA32, false);

            ImageConversion.LoadImage(tex, bytes);
            GameObject terrainObj = new GameObject("ProceduralTerrain");
            terrainObj.transform.position = new Vector3(0, -36f, 0);
            terrainObj.AddComponent<MeshFilter>().mesh = mesh;
            MeshRenderer renderer = terrainObj.AddComponent<MeshRenderer>();
            renderer.material = new Material(Shader.Find("Standard"));
            renderer.material.mainTexture = tex;
            renderer.material.mainTextureScale = new Vector2(50f, 50f);
            terrainObj.AddComponent<MeshCollider>().sharedMesh = mesh;
        }
    }
}
