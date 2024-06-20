using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public int whosTurn; //0 for x, 1 for O
    public int turnCount; //count the number of turns

    public GameObject[] turnIcons; //display whos turnit is
    public Sprite[] playerIcons; // 0 = x icon and 1 = y icon
    public Button[] ticTacToeSpaces; //playable cells


    // Start is called before the first frame update
    void Start()
    {
        GameSetUp();
    }

    void GameSetUp()
    {
        whosTurn = 0;
        turnCount = 0;
        turnIcons[0].SetActive(true);
        turnIcons[1].SetActive(false);

        for (int i = 0; i < ticTacToeSpaces.Length; i++)
        {
            ticTacToeSpaces[i].interactable = true;
            ticTacToeSpaces[i].GetComponent<Image>().sprite = null;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
