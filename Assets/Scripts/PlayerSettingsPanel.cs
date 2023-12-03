using TMPro;
using UnityEngine;

public class PlayerSettingsPanel : MonoBehaviour
{
    [SerializeField] TMP_InputField nameInputField;
    [SerializeField] TMP_InputField numberInputField;

    Player player;

    public void SetPlayer(Player player)
    {
        this.player = player;
        nameInputField.text = player.playerName;
        numberInputField.text = player.playerNumber;
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
