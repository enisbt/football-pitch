using TMPro;
using UnityEngine;

public class PlayerSettingsPanel : MonoBehaviour
{
    [SerializeField] TMP_InputField nameInputField;
    [SerializeField] TMP_InputField numberInputField;

    Player player;
    Transform settingsPosition;

    private void Update()
    {
        if (player != null)
        {
            transform.position = settingsPosition.position;
        }
    }

    public void SetPlayer(Player player, Transform settingsPosition)
    {
        transform.position = settingsPosition.position;
        this.player = player;
        this.settingsPosition = settingsPosition;
        nameInputField.text = player.playerName;
        numberInputField.text = player.playerNumber;
    }

    public void ResetPlayer()
    {
        player = null;
        settingsPosition = null;
    }

    public void NameOnValueChange()
    {
        player.UpdatePlayerName(nameInputField.text);
    }

    public void NumberOnValueChange()
    {
        player.UpdatePlayerNumber(numberInputField.text);
    }
}
