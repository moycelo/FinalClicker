using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinScript : MonoBehaviour
{
    public TMP_Text ScoreText;
    public double totalScore = 0;
    public double autoCoinsPerSecond = 1;
    public double clickUpgradeCost = 10;
    public double autoClickUpgradeCost = 100;
    public double clickValue = 1;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //totalScore += autoCoinsPerSecond * Time.deltaTime;
        UpdateScoreText();
    }
    public void OnPileClicked()
    {
        totalScore += clickValue;

    }
    void UpdateScoreText()
    {
        ScoreText.text = "Score: " + totalScore.ToString("N0");
    }
    public void BuyClickUpgrade()
    {
        totalScore -= clickUpgradeCost;
        clickValue +=1;
        clickUpgradeCost *=2;

    }
    public void BuyAutoClickUpgrade()
    {
        totalScore -=autoClickUpgradeCost;
        autoCoinsPerSecond += 1;
        autoClickUpgradeCost *=2;
    }
}
