using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public enum PlayerTeam
{
    TeamA,
    TeamB,
}

public class Player : MonoBehaviour, IPointerClickHandler, IDragHandler
{
    [SerializeField] public string playerName;
    [SerializeField] public string playerNumber;
    [SerializeField] public PlayerTeam team;
    [SerializeField] Color primaryKitColor = Color.blue;
    [SerializeField] Color secondaryKitColor = Color.yellow;
    [SerializeField] Image kit;
    [SerializeField] Transform kitParent;
    [SerializeField] RectTransform topBar;
    [SerializeField] Transform leftSettingsPosition;
    [SerializeField] Transform rightSettingsPosition;
    [SerializeField] GameObject playerSettingsPanel;

    TMP_Text numberText;
    TMP_Text nameText;

    bool isSettingsPanelActive = false;
    bool isDragging;

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
        if (isDragging)
        {
            isDragging = false;
            return;
        }

        if (isSettingsPanelActive)
        {
            playerSettingsPanel.SetActive(false);
            playerSettingsPanel.GetComponent<PlayerSettingsPanel>().ResetPlayer();
            isSettingsPanelActive = false;
            return;
        }

        isSettingsPanelActive = true;
        playerSettingsPanel.SetActive(false);
        playerSettingsPanel.GetComponent<PlayerSettingsPanel>().SetPlayer(this, rightSettingsPosition);
        playerSettingsPanel.transform.Find("Number Input Field").GetComponent<TMP_InputField>().text = playerNumber;
        playerSettingsPanel.transform.Find("Name Input Field").GetComponent<TMP_InputField>().text = playerName;
        playerSettingsPanel.SetActive(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        isDragging = true;
        transform.position = Input.mousePosition;
        ClampToPitch();
    }

    private void ClampToPitch()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        float topBound = Screen.height - topBar.sizeDelta.y;
        float y = Mathf.Clamp(rectTransform.position.y, 0, topBound + rectTransform.sizeDelta.y / 2);
        rectTransform.position = new Vector3(rectTransform.position.x, y, rectTransform.position.z);
    }
}
