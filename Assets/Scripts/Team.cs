using UnityEngine;

public class Team : MonoBehaviour
{
    [SerializeField] PlayerTeam team;
    [SerializeField] string teamName;
    [SerializeField] Color color;

    Player[] players = new Player[11];

    private void Start()
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

        for (int i = 0; i < players.Length;i++)
        {
            players[i].SetColor(color);
        }
    }
}
