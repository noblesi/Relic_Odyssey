using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotUI : MonoBehaviour
{
    public Image itemImage;
    public Outline outline;
    public TMP_Text levelText;

    private int slotIndex;
    private ArtifactData currentArtifact;

    public void Init(int index)
    {
        slotIndex = index;
        Clear();
    }

    public void SetItem(ArtifactData artifact)
    {
        currentArtifact = artifact;
        if(artifact != null)
        {
            itemImage.sprite = artifact.icon;
            itemImage.enabled = true;
            outline.enabled = false;
            levelText.text = "Lv." + artifact.level.ToString();
        }
    }

    public void Clear()
    {
        currentArtifact = null;
        itemImage.sprite = null;
        itemImage.enabled=false;
        outline.enabled = false;
        levelText.text = "";
    }

    public void OnClick()
    {
        outline.enabled = !outline.enabled;
    }

    public ArtifactData GetArtifact()
    {
        return currentArtifact;
    }

    public int GetIndex()
    {
        return slotIndex;
    }
}
