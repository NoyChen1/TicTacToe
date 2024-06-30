using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public int whosTurn; //0 for x, 1 for O
    public int turnCount; //count the number of turns
    public int indexWinner; //holda the line/row/diagonal index
    public int xScore;
    public int oScore;

    public GameObject[] turnIcons; //display whos turnit is
    public Sprite[] playerIcons; // 0 = x icon and 1 = y icon
    public Button[] ticTacToeSpaces; //playable cells
    public int[] markedSpaces; //ID's which places are marked by which player
    public GameObject[] winningLines; //holds all the different lines for showing the winning

    public Text winnerText;
    public Text xPlayerScore;
    public Text oPlayerScore;
    public GameObject winnerPanel;
    public GameObject tiePanel;

    public Button xButton;
    public Button oButton;

    public AudioSource buttonClickAudio;
    public AudioSource winAudio;
    public AudioSource tieAudio;


    // Start is called before the first frame update
    void Start()
    {
        // buttonClickAudio = GetComponent<AudioSource>();
        GameSetUp();
        playButtonClick();

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
        for (int i = 0; i < markedSpaces.Length; i++)
        {
            markedSpaces[i] = -100;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TicTacToewButton(int button)
    {
        xButton.interactable = false;
        oButton.interactable = false;
        ticTacToeSpaces[button].image.sprite = playerIcons[whosTurn];
        ticTacToeSpaces[button].interactable = false;
        markedSpaces[button] = whosTurn + 1;
        turnCount++;

        if (turnCount > 4)
        {
            bool isWinner = WinnerCheck();
            if (turnCount == 9 && !isWinner)
            {
                tiePanel.SetActive(true);
                tieAudio.Play();
            }
        }

        
            

        if (whosTurn == 0)
        {
            whosTurn = 1;
            turnIcons[0].SetActive(false);
            turnIcons[1].SetActive(true);
        }
        else
        {
            whosTurn = 0;
            turnIcons[0].SetActive(true);
            turnIcons[1].SetActive(false);
        }

        //    whosTurn = (whosTurn == 0) ? 1 : 0;
    }

    bool WinnerCheck()
    {
        //line win
        //row win
        //primary diagonal win
        //second diagonal win


        if(LineWin() || RowWin() || PrimaryDiagonal() || SecondDiagonal())
        {
            WinnerDisplaye();
            winAudio.Play();
            return true;
        }
            return false;
    }

    bool LineWin()
    {
        if (markedSpaces[0] + markedSpaces[1] + markedSpaces[2] == 3 * (whosTurn + 1))
        {
            indexWinner = 0;
            return true;
        }
        else if (markedSpaces[3] + markedSpaces[4] + markedSpaces[5] == 3 * (whosTurn + 1))
        {
            indexWinner = 1;
            return true;
        }
        else if (markedSpaces[6] + markedSpaces[7] + markedSpaces[8] == 3 * (whosTurn + 1))
        {
            indexWinner = 2;
            return true;
        }
        return false;
    }

    bool RowWin() 
    {
        if (markedSpaces[0] + markedSpaces[3] + markedSpaces[6] == 3 * (whosTurn + 1))
        {
            indexWinner = 3;
            return true;
        }else if (markedSpaces[1] + markedSpaces[4] + markedSpaces[7] == 3 * (whosTurn + 1))
        {
            indexWinner = 4;
            return true;
        }else if (markedSpaces[2] + markedSpaces[5] + markedSpaces[8] == 3 * (whosTurn + 1))
        {
            indexWinner = 5;
            return true;
        }
        return false;
    }

    bool PrimaryDiagonal()
    {
        if(markedSpaces[0] + markedSpaces[4] + markedSpaces[8] == 3 * (whosTurn + 1))
        {
            indexWinner = 6; 
            return true;
        }
        return false;
    }

    bool SecondDiagonal()
    {
        if(markedSpaces[2] + markedSpaces[4] + markedSpaces[6] == 3 * (whosTurn + 1))
        {
            indexWinner = 7;
            return true;
        }
        return false;
    }

    void WinnerDisplaye()
    {
        winnerPanel.gameObject.SetActive(true);
        if (whosTurn == 0)
        {
            xScore++;
            xPlayerScore.text = xScore.ToString();
            winnerText.text = "Player X Won !";
        }
        else
        {
            oScore++;
            oPlayerScore.text = oScore.ToString();
            winnerText.text = "Player O Won !";
        }
        winningLines[indexWinner].gameObject.SetActive(true);

    }

    public void Rematch()
    {
        GameSetUp();
        if (indexWinner != -1)
        {
            winningLines[indexWinner].SetActive(false);
        }
        indexWinner = -1;
        winnerPanel.SetActive(false);
        tiePanel.SetActive(false);
        xButton.interactable = true;
        oButton.interactable = true;
    }

    public void Restart()
    {
        Rematch();
        xScore = 0;
        xPlayerScore.text = xScore.ToString();
        oScore = 0;
        oPlayerScore.text = oScore.ToString();

    }
    public void SwitchPlayer(int player)
    {
        if (player == 0)
        {
            whosTurn = 0;
            turnIcons[player].SetActive(true);
            turnIcons[1].SetActive(false);
        }
        else if (player == 1)
        {
            whosTurn = 1;
            turnIcons[player].SetActive(true);
            turnIcons[0].SetActive(false);
        }
    }

    public void playButtonClick()
    {

        Debug.Log("playButtonClick method called");
        if (buttonClickAudio != null)
        {
            Debug.Log("buttonClickAudio is not null, attempting to play");
            buttonClickAudio.PlayDelayed(0.1f);
        }
        else
        {
            Debug.Log("buttonClickAudio is null");
        }


        //buttonClickAudio.Play();
    }

}
