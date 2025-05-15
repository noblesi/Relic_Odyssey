using TMPro;
using UnityEngine;

public class TooltipUI : MonoBehaviour
{
    public static TooltipUI Instance;

    public GameObject panel;
    public TMP_Text nameText;
    public TMP_Text descriptionText;

    private void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    public void ShowTooltip(ArtifactData data, Vector3 position)
    {
        nameText.text = data.artifactName;
        descriptionText.text = data.description;
        panel.SetActive(true);
        panel.transform.position = position + new Vector3(150f, -30f);
    }

    public void HideTooltip()
    {
        panel.SetActive(false);
    }
}
