using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Block Data", menuName = "CptLost/Block Data")]
public class BlockDataSO : ScriptableObject
{
    [SerializeField] private float textureSizeX, textureSizeY;
    [SerializeField] private float textureOffset = 0.001f;
    [SerializeField] private List<BlockData> blockData;

    public float TextureSizeX { get { return textureSizeX; } }
    public float TextureSizeY { get { return textureSizeY; } }
    public float TextureOffset { get { return textureOffset; } }
    public List<BlockData> BlockData { get { return blockData; } }
}

[Serializable]
public class BlockData
{
    public BlockType blockType;
    public Vector2Int texture;
    public bool isSolid = true;
    public bool generateColiider = true;
}