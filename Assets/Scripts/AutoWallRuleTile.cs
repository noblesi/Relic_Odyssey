using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "AutoWallRuleTile", menuName = "Tiles/Auto Wall Rule Tile")]
public class AutoWallRuleTile : RuleTile<AutoWallRuleTile.Neighbor> 
{
    [System.Serializable]
    public class Neighbor : RuleTile.TilingRule.Neighbor {
        public const int wall = 1;
    }

    [Header("Bitmask Mapping (0~15)")]
    public Sprite[] bitmaskSprites = new Sprite[16];

    public override bool RuleMatch(int neighbor, TileBase tile)
    {
        return tile == this;
    }

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        int mask = 0;
        if (HasSameTile(position + Vector3Int.up, tilemap)) mask += 1;
        if (HasSameTile(position + Vector3Int.right, tilemap)) mask += 2;
        if (HasSameTile(position + Vector3Int.down, tilemap)) mask += 4;
        if (HasSameTile(position + Vector3Int.left, tilemap)) mask += 8;

        if(bitmaskSprites != null && mask < bitmaskSprites.Length && bitmaskSprites[mask] != null)
        {
            tileData.sprite = bitmaskSprites[mask];
            tileData.colliderType = Tile.ColliderType.Grid;
        }
        else
        {
            tileData.sprite = null;
        }

        tileData.flags = TileFlags.LockTransform;
        tileData.transform = Matrix4x4.identity;
        tileData.color = Color.white;
    }

    private bool HasSameTile(Vector3Int position, ITilemap tilemap)
    {
        var tile = tilemap.GetTile(position);
        return tile == this;
    }
}