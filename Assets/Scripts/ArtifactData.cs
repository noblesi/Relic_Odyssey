using UnityEngine;

[CreateAssetMenu(fileName = "ArtifactData", menuName = "RelicOdyssey/ArtifactData")]
public class ArtifactData : ScriptableObject
{
    public string artifactName;
    public Sprite icon;
    public string description;
    public ArtifactRarity rarity;
    public int level;
    public int maxLevel;
    public ArtifactType type;
    public ArtifactEffect[] effects;
}

public enum ArtifactRarity
{
    Common,
    Rare,
    Epic,
    Legendary
}

public enum ArtifactType
{
    Offensive,
    Defensive,
    Utility,
    Passive
}

[System.Serializable]
public class ArtifactEffect
{
    public string effectName;
    public string effectDescription;
    public float value;
}
