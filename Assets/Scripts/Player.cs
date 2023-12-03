using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public enum PlayerTeam
{
    TeamA,
    TeamB,
}

public class Player : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] public string playerName;
    [SerializeField] public string playerNumber;
    [SerializeField] public PlayerTeam team;
    [SerializeField] Color primaryKitColor = Color.blue;
    [SerializeField] Color secondaryKitColor = Color.yellow;
    [SerializeField] Image kit;
    [SerializeField] Transform kitParent;
    [SerializeField] Transform leftSettingsPosition;
    [SerializeField] Transform rightSettingsPosition;
    [SerializeField] GameObject playerSettingsPanel;

    TMP_Text numberText;
    TMP_Text nameText;

    bool isSettingsPanelActive = false;

    private void Start()
    {
        numberText = transform.Find("Number Text").GetComponent<TMP_Text>();
        nameText = transform.Find("Player Name Text Background").Find("Player Name Text").GetComponent<TMP_Text>();
    }

    private void Update()
    {
        numberText.text = playerNumber;
        nameText.text = playerName;
    }

    public void SetKit(int index, Color primaryColor, Color secondaryColor)
    {
        primaryKitColor = primaryColor;
        secondaryKitColor = secondaryColor;
        kit.color = primaryKitColor;

        for (int i = 0; i < kitParent.childCount; i++)
        {
            if (i == index)
            {
                kitParent.GetChild(i).gameObject.SetActive(true);
                kitParent.GetChild(i).GetComponent<Image>().color = secondaryKitColor;
            }
            else
            {
                kitParent.GetChild(i).gameObject.SetActive(false);
                kitParent.GetChild(i).GetComponent<Image>().color = secondaryKitColor;
            }
        }
    }

    public void UpdatePlayerName(string playerName)
    {
        this.playerName = playerName;
        nameText.text = this.playerName;
    }

    public void UpdatePlayerNumber(string playerNumber)
    {
        this.playerNumber = playerNumber;
        numberText.text = this.playerNumber;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isSettingsPanelActive)
        {
            playerSettingsPanel.SetActive(false);
            isSettingsPanelActive = false;
            return;
        }

        isSettingsPanelActive = true;
        playerSettingsPanel.SetActive(false);
        playerSettingsPanel.GetComponent<PlayerSettingsPanel>().SetPlayer(this);
        playerSettingsPanel.transform.Find("Number Input Field").GetComponent<TMP_InputField>().text = playerNumber;
        playerSettingsPanel.transform.Find("Name Input Field").GetComponent<TMP_InputField>().text = playerName;
        playerSettingsPanel.transform.position = rightSettingsPosition.position;
        playerSettingsPanel.SetActive(true);
    }
}
