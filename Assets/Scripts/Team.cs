using UnityEngine;
using TMPro;
using UnityEngine.UI;

public enum KitType
{
    Stripes,
    Split,
    VerticalStripes,
    Arms,
    Diagonal,
    Checkers,
    Chest,
    Sash,
    VerticalSash,
    Full,
}

public class Team : MonoBehaviour
{
    [SerializeField] PlayerTeam team;
    [SerializeField] string teamName;
    [SerializeField] Color primaryColor;
    [SerializeField] Color secondaryColor;
    [SerializeField] KitType kitType;
    [SerializeField] TMP_Text teamNameText;
    [SerializeField] Image uiKitBase;
    [SerializeField] Transform uiKitParent;
    [SerializeField] GameObject settingsPanel;

    TMP_InputField teamNameTextInputField;
    Player[] players = new Player[11];

    private void Start()
    {
        teamNameTextInputField = settingsPanel.transform.Find("Team Name Text Input Field").GetComponent<TMP_InputField>();
        teamNameText.text = teamName;
        teamNameTextInputField.text = teamName;

        FindTeamPlayers();
        PaintUIKits();
    }

    private void PaintUIKits()
    {
        uiKitBase.color = primaryColor;
        int kitIndex = (int)kitType;
        for (int i = 0; i < uiKitParent.childCount; i++)
        {
            if (i == kitIndex)
            {
                uiKitParent.GetChild(i).gameObject.SetActive(true);
                uiKitParent.GetChild(i).GetComponent<Image>().color = secondaryColor;
            }
        }
    }

    private void FindTeamPlayers()
    {
        Player[] allPlayers = FindObjectsOfType<Player>();
        int currentPlayerIndex = 0;

        for (int i = 0; i < allPlayers.Length; i++)
        {
            if (allPlayers[i].team == team)
            {
                players[currentPlayerIndex] = allPlayers[i];
                currentPlayerIndex++;
            }
        }

        PaintOnFieldKits();
    }

    private void PaintOnFieldKits()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].SetKit((int)kitType, primaryColor, secondaryColor);
        }
    }

    public void ToggleSettingsPanel()
    {
        if (!settingsPanel.activeSelf)
        {
            PaintSettingsPanelKits();
        }

        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

    private void PaintSettingsPanelKits()
    {
        Transform settingsKitParent = settingsPanel.transform.Find("Kits");

        for (int i = 0; i < settingsKitParent.childCount; i++)
        {
            settingsKitParent.GetChild(i).GetComponent<Image>().color = primaryColor;
            settingsKitParent.GetChild(i).GetChild(0).GetComponent<Image>().color = secondaryColor;
        }

        settingsPanel.transform.Find("Primary Color").GetComponent<Image>().color = primaryColor;
        settingsPanel.transform.Find("Secondary Color").GetComponent<Image>().color = secondaryColor;
    }

    public void SetPrimaryColor(Color color)
    {
        primaryColor = color;
        PaintUIKits();
        PaintSettingsPanelKits();
        PaintOnFieldKits();
    }

    public void SetSecondaryColor(Color color)
    {
        secondaryColor = color;
        PaintUIKits();
        PaintSettingsPanelKits();
        PaintOnFieldKits();
    }

    public void OnTeamNameTextChange()
    {
        teamName = teamNameTextInputField.text;
        teamNameText.text = teamName;
    }
}
