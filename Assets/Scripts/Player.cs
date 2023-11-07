using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum PlayerTeam
{
    TeamA,
    TeamB,
}

public class Player : MonoBehaviour
{
    [SerializeField] string playerName;
    [SerializeField] string playerNumber;
    [SerializeField] public PlayerTeam team;
    [SerializeField] Color kitColor = Color.blue;
    [SerializeField] Image kit;

    TMP_Text numberText;
    TMP_Text nameText;

    private void Start()
    {
        numberText = transform.Find("Number Text").GetComponent<TMP_Text>();
        nameText = transform.Find("Player Name Text Background").Find("Player Name Text").GetComponent<TMP_Text>();
        kit.color = kitColor;
    }

    private void Update()
    {
        numberText.text = playerNumber;
        nameText.text = playerName;
    }

    public void SetColor(Color color)
    {
        kitColor = color;
        kit.color = kitColor;
    }
}
