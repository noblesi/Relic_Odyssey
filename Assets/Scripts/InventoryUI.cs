
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject slotPrefab;
    public Transform slotParent;
    public int slotCount = 64;

    private List<InventorySlotUI> slotUIs = new();

    private void Start()
    {
        InitSlots();
    }

    private void InitSlots()
    {
        for(int i = 0; i < slotCount; i++)
        {
            GameObject obj = Instantiate(slotPrefab, slotParent);
            InventorySlotUI slotUI = obj.GetComponent<InventorySlotUI>();
            slotUI.Init(i);
            slotUIs.Add(slotUI);
        }
    }

    public void SetItemToSlot(int index, ArtifactData data)
    {
        if(index >= 0 && index < slotUIs.Count)
        {
            slotUIs[index].SetItem(data);
        }
    }

    public void ClearSlot(int index)
    {
        if (index >= 0 && index < slotUIs.Count)
        {
            slotUIs[index].Clear();
        }
    }
}
