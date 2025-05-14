using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    private InventorySlotUI selectedSlot;

    private void Awake()
    {
        Instance = this;
    }

    public void SelectSlot(InventorySlotUI slot)
    {
        if(selectedSlot != null)
        {
            selectedSlot.SetSelected(false);
        }

        selectedSlot = slot;
        selectedSlot.SetSelected(true);

        Debug.Log($"선택된 슬롯: {slot.GetIndex()}, 아이템: {slot.GetArtifact()?.artifactName}");
    }

    public ArtifactData GetSelectedArtifact()
    {
        return selectedSlot?.GetArtifact();
    }
}
