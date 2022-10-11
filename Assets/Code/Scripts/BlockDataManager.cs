using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDataManager : MonoBehaviour
{
    public static BlockDataManager instance;

    [SerializeField] private BlockDataSO blockData;

    private Dictionary<BlockType, BlockData> blockDataDictionary = new Dictionary<BlockType, BlockData>();

    public float TileSizeX { get { return blockData.TextureSizeX; } }
    public float TileSizeY { get { return blockData.TextureSizeY; } }
    public float TextureOffset { get { return blockData.TextureOffset; } }
    public Dictionary<BlockType, BlockData> BlockDataDictionary { get { return blockDataDictionary; } }

    private void Awake()
    {
        instance = this;

        foreach (var item in blockData.BlockData)
        {
            if (blockDataDictionary.ContainsKey(item.blockType) == false)
            {
                blockDataDictionary.Add(item.blockType, item);
            };
        }

    }
}
