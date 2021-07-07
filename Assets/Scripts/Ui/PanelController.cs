using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{

    [System.Serializable]
    public class PlayerCollidedEvent : UnityEvent { }


    public GameObject MoneyPopup;
    public GameObject[] ChestGroup;
    public GameObject MainMenu;
    public GameObject CreditsMenu;
    public GameObject InGameUI;
    public Button BetButton;
    public Button UpDenominatorButton;
    public Button DownDenominatorButton;
    public PlayerStats Player;
    public Text CurrDenomT;
    public Text CurrBalT;
    public Text CurrWinT;
    public Animator MenuAnimator;
    public Animator InGameAnimator;

    float[] denominations = { .25f, .5f, 1f, 5f };
    AudioManager audioManagerMenu;
    int denominationIndex = 0;
    int currChestnum = 0;
    public float winPool = 0;

    // Start is called before the first frame update
    void Start()
    {
        audioManagerMenu = GetComponent<AudioManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        CurrBalT.text = "Pool: " + Player.currBal.ToString("C2");
        CurrWinT.text = "Last Win: " + Player.lastWin.ToString("C2");
    }

    public void ChestClicked(Transform chestClicked)
    {
        audioManagerMenu.PlayOneShot("Button");
        currChestnum++;
        if (winPool <= 0)
        {
            ChestsOff();
            Player.currBal += Player.lastWin;
            StartCoroutine(DisplayChestPrize(chestClicked, 0f, .8f));
            return;
        }

        float winAmount = GamblingLogic.ChestPrizeAmount(currChestnum, winPool);

        winPool = Mathf.Round((winPool - winAmount) * 100f) / 100f;
        Player.lastWin = Mathf.Round((Player.lastWin + winAmount) * 100f) / 100f; 

        StartCoroutine(DisplayChestPrize(chestClicked, winAmount,.8f));
    }

    IEnumerator DisplayChestPrize(Transform chestClicked, float winAmount,float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        var popup = Instantiate(MoneyPopup);
        popup.transform.SetParent(chestClicked.gameObject.transform, false);

        GoldPopUp goldPopUp = popup.GetComponent<GoldPopUp>();
        goldPopUp.Setup(winAmount);
    }

    public void BackOutCreditScreen()
    {
        audioManagerMenu.PlayOneShot("Button");
        CreditsMenu.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 7000, 0);
    }
    public void StartGame()
    {
        audioManagerMenu.PlayOneShot("Button");
        MenuAnimator.SetTrigger("PlayButton");

        InGameAnimator.SetTrigger("StartGame");
    }

    public void ShowCreditsScreen()
    {
        audioManagerMenu.PlayOneShot("Button");
        CreditsMenu.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
    }

    public void StartBet()
    {
        if (Player.currBal < Player.currDenom)
        {
            audioManagerMenu.PlayOneShot("Error");
            return;
        }
        audioManagerMenu.PlayOneShot("Button");
        winPool = GamblingLogic.CalculateWin(Player.currDenom);
        Player.lastWin = 0;
        Player.currBal -= Player.currDenom;
        ChestsOn();
    }
    public void DenominatorUp()
    {
        if (denominationIndex == denominations.Length-1)
        {
            audioManagerMenu.PlayOneShot("Error");
            return;
        }
        audioManagerMenu.PlayOneShot("Button");
        Player.currDenom = denominations[++denominationIndex];
        CurrDenomT.text = Player.currDenom.ToString("C2");
        if (Player.currBal < Player.currDenom)
        {
            BetButton.interactable = false;
        }
    }

    public void DenominatorDown()
    {
        if (denominationIndex == 0)
        {
            audioManagerMenu.PlayOneShot("Error");
            return;
        }
        audioManagerMenu.PlayOneShot("Button");
        Player.currDenom = denominations[--denominationIndex];
        CurrDenomT.text = Player.currDenom.ToString("C2");
        if (Player.currBal > Player.currDenom)
        {
            BetButton.interactable = true;
        }
    }

    public void OpenLink()
    {
        Application.OpenURL("https://www.ronaldvarela.com/");
    }

    private void ChestsOn()
    {
        foreach (GameObject i in ChestGroup)
        {
            i.GetComponent<Button>().interactable = true;
        }
         BetButton.interactable = false;
         UpDenominatorButton.interactable = false;
         DownDenominatorButton.interactable = false;
    }

    private void ChestsOff()
    {
        currChestnum = 0;
        foreach (GameObject i in ChestGroup)
        {
            i.GetComponent<Button>().interactable = false;
        }
        if (Player.currBal >= Player.currDenom)
        {
            BetButton.interactable = true;
        }
        UpDenominatorButton.interactable = true;
        DownDenominatorButton.interactable = true;
    }
}
