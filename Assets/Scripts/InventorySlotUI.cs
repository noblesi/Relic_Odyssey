using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventorySlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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
        else
        {
            Clear();
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
        InventoryManager.Instance.SelectSlot(this);
    }

    public void SetSelected(bool selected)
    {
        outline.enabled = selected;
    }

    public ArtifactData GetArtifact()
    {
        return currentArtifact;
    }

    public int GetIndex()
    {
        return slotIndex;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(currentArtifact != null)
        {
            TooltipUI.Instance.ShowTooltip(currentArtifact, transform.position);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipUI.Instance.HideTooltip();
    }
}
