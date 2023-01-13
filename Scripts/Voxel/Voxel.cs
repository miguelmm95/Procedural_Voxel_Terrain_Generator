using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voxel : MonoBehaviour
{
    public VoxelType type;
    public bool itsAnOre;
    public float noise;
    public int x;
    public float y;
    public int z;

    public Voxel(VoxelType type, bool itsAnOre, float noise, int x, float y, int z)
    {
        this.type = type;
        this.itsAnOre = itsAnOre;
        this.noise = noise;
        this.x = x;
        this.y = y;
        this.z = z;
    }
}

public enum VoxelType
{
    Grass,
    Dirt,
    Stone,
    Snow,
    Water
}
