using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] string playerName;
    [SerializeField] string playerNumber;

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
}
