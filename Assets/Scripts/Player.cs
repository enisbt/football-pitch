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
    [SerializeField] Color primaryKitColor = Color.blue;
    [SerializeField] Color secondaryKitColor = Color.yellow;
    [SerializeField] Image kit;
    [SerializeField] Transform kitParent;

    TMP_Text numberText;
    TMP_Text nameText;

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
}
