using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    private List<Voxel> surfaceVoxels;
    private List<Voxel> groundVoxels;
    private float stoneNoiseScale = 0.9f;

    public GameObject[] blocks;
    public int sizeX;
    public int sizeZ;

    public int worldHeight;
    public int groundHeight;
    public int surfaceDetail;
    public int groundDetail;

    float seed;

    void Start()
    {
        surfaceVoxels = new List<Voxel>();
        groundVoxels = new List<Voxel>();

        seed = Random.Range(100000, 999999);

        GenerateWorld();
    }

    private void GenerateWorld()
    {
        for (int x = 0; x < sizeX; x++)
        {
            for (int z = 0; z < sizeZ; z++)
            {
                int maxHeight = (int)(Mathf.PerlinNoise((x + seed + Random.Range(0.1f, 0.5f)) / surfaceDetail, (z + seed + Random.Range(0.1f, 0.5f)) / surfaceDetail) * worldHeight);
                maxHeight += groundHeight;

                if(maxHeight >= 120)
                {
                    surfaceVoxels.Add(new Voxel(VoxelType.Stone, false, maxHeight, x, maxHeight, z));
                    Instantiate(blocks[0], new Vector3(x, maxHeight, z), Quaternion.identity);
                }
                else
                {
                    surfaceVoxels.Add(new Voxel(VoxelType.Grass, false, maxHeight, x, maxHeight, z));
                    Instantiate(blocks[1], new Vector3(x, maxHeight, z), Quaternion.identity);
                }


                for (int y = 0; y < maxHeight; y++)
                {
                    int dirtLayers = Random.Range(1, 5);
                    int stoneLayers = Random.Range(5, 8);


                    //int oreLayer = Random.Range();
                    
                    if(y >= maxHeight - dirtLayers)
                    {
                        groundVoxels.Add(new Voxel(VoxelType.Dirt, false, y, x, y, z));
                        Instantiate(blocks[2], new Vector3(x, y, z), Quaternion.identity);
                    }
                    else if (y >= maxHeight - stoneLayers)
                    {
                        groundVoxels.Add(new Voxel(VoxelType.Stone, false, y, x, y, z));
                        Instantiate(blocks[3], new Vector3(x, y, z), Quaternion.identity);
                    }
                    else if(y == 0)
                    {
                        Instantiate(blocks[7], new Vector3(x, y, z), Quaternion.identity);
                    }
                    else
                    {
                        float stoneNoise = PerlinNoise3D(x * stoneNoiseScale, y * stoneNoiseScale, z * stoneNoiseScale, groundDetail);

                        if (stoneNoise < 0.5f)
                        {
                            /*float oreProb = Random.Range(0f, 1f);
                            Debug.Log(oreProb);

                            if(.3f < oreProb && oreProb < 0.39f)
                            {
                                Instantiate(blocks[4], new Vector3(x, y, z), Quaternion.identity);

                            }else if(.2f < oreProb && oreProb < 0.29f)
                            {
                                Instantiate(blocks[5], new Vector3(x, y, z), Quaternion.identity);

                            }else if(0f < oreProb && oreProb < 0.05f)
                            {
                                Instantiate(blocks[6], new Vector3(x, y, z), Quaternion.identity);
                            }
                            else*/
                            //{
                                groundVoxels.Add(new Voxel(VoxelType.Stone, false, stoneNoise, x, y, z));
                                Instantiate(blocks[3], new Vector3(x, y, z), Quaternion.identity);
                            //}
                        }
                    }
                }
            }
        }
    }


    private float PerlinNoise3D(float x, float y, float z, float detail)
    {
        float ab = Mathf.PerlinNoise((x + seed) / detail, (y + seed) / detail);
        float bc = Mathf.PerlinNoise((y + seed) / detail, (z + seed) / detail);
        float ac = Mathf.PerlinNoise((x + seed) / detail, (z + seed) / detail);

        float ba = Mathf.PerlinNoise((y + seed) / detail, (x + seed) / detail);
        float cb = Mathf.PerlinNoise((z + seed) / detail, (y + seed) / detail);
        float ca = Mathf.PerlinNoise((z + seed) / detail, (x + seed) / detail);

        float abc = ab + bc + ac + ba + cb + ca;

        return abc / 6f;
    }
}
