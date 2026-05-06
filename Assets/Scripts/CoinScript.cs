using UnityEngine;
using TMPro;
using System;




public class CoinScript : MonoBehaviour
{

    //Audios stuff
    public UnityEngine.UI.Slider volumeSlider;
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioClip clickSounds;
    public AudioClip upgradeSound;



    //UI elements for stats
    public TMP_Text ScoreText;
    public TMP_Text LifetimeEarningsText;
    public TMP_Text TimePlayedText;
    public TMP_Text MultiplierPriceText;
    public TMP_Text MultiplierLevelText;
    public TMP_Text MultiplierValueText;
    public TMP_Text AutoButtonText; //On or OFF 
    public TMP_Text UpgradePriceText;
    public TMP_Text ClickLevelText;
    public TMP_Text ClickValueText;


    //Game Values
    public double totalScore = 0;
    public double lifetimeEarnings = 0;
    private double timePlayed = 0;
    public double moneyFromClicks = 0;



    //Upgrade Settings
    public int clickUpgradeLevel = 0;
    public double clickBase = 0.01;


    //coin multiplier settings
    public int coinUpgradeCount = 0; //level count
    public int MultiplierLevel = 0; //level count
    public int maxCoinLevels = 4; //max lvl
    private int[] coinMultiplierPrice = { 5, 100, 2500, 40000 };
    private int[] multiplierValues = { 1, 2, 5, 10, 20 };


    //AutoClickerSettings
    public bool isAutoClickActive = false;
    public float autoClickSpeed = 0.2222f;
    private float autoClickTimer;

    void Start()
    {
        volumeSlider.value = musicSource.volume; //allows to manually change volume in game, and applied to editor
        volumeSlider.onValueChanged.AddListener(SetVolume);
        musicSource.loop = true;
        musicSource.Play();
        LoadGame();
        UpdateUI();
        UpdateScoreText();
    }
    void Update()
    {
        timePlayed += Time.deltaTime; //tracks time played

        if (isAutoClickActive)
        {
            autoClickTimer += Time.deltaTime;

            if (autoClickTimer >= autoClickSpeed)
            {
                autoClickTimer = 0f; //resets stop watch
                OnPileClicked(); //triggers click, so auto click value is applied.
            }

        }
        UpdateTimePlayedText();
        pressThisForRestart();
    }

    // --- CORE LOGIC!!!!! ---
    public void AddMoney(double amount)
    {
        totalScore += amount; //adds the amount to the total score
        lifetimeEarnings += amount; //adds the amount to the lifetime earnings
        UpdateScoreText();
    }
    public void OnPileClicked()
    {
        double amount = GetCurrentClickValue(); //click
        AddMoney(amount);
        moneyFromClicks += amount; //stores how much money made in clicks
        if (!isAutoClickActive)
            sfxSource.PlayOneShot(clickSounds); //plays click sound only if not auto clicking

        UpdateUI();
    }

    //auto clicker
    public void ToggleAutoClicker()
    {
        isAutoClickActive = !isAutoClickActive;
        sfxSource.PlayOneShot(upgradeSound,2f);
        UpdateUI();
        SaveGame();
    }
    public void BuyClickUpgrade()
    {

        double currentCost = GetClickUpgradeCost();
        if (totalScore >= currentCost - 0.0001) //small tolerance 
        {
            totalScore -= currentCost;
            clickUpgradeLevel += 1;
            sfxSource.PlayOneShot(upgradeSound);
            UpdateUI();
            UpdateScoreText();
            SaveGame();
        }
        else
        {
            Debug.Log("Not enough score to buy click upgrade!");
        }
    }
    public void MultiplierUpgrade()
    {
        if (coinUpgradeCount >= maxCoinLevels)
        {
            Debug.Log("Max click upgrade level reached!");
            return;
        }
        double multiplierCost = coinMultiplierPrice[coinUpgradeCount];
        if (totalScore >= multiplierCost)
        {
            totalScore -= multiplierCost;
            coinUpgradeCount++;
            sfxSource.PlayOneShot(upgradeSound);
            UpdateUI();
            UpdateScoreText();
            SaveGame();
        }

    }


    //---MATHS CALCULATION!!!!!!!! ---
    public double GetCurrentClickValue()
    {
        double baseClick = 0.01 + (clickUpgradeLevel * 0.015);
        if (clickUpgradeLevel % 2 != 0) baseClick += 0.005; //every odd level, add 10% bonus

        int decades = clickUpgradeLevel / 10; //every 10 levels
        double decadeBonus = Math.Pow(1.5, clickUpgradeLevel / 10);

        //double coinMultiplier;
        int currentMult = multiplierValues[coinUpgradeCount]; //gets the current multiplier value based on the coin upgrade count
        return baseClick * decadeBonus * currentMult;

    }
    public double GetClickUpgradeCost()
    {
        double basePrice = 0.1;
        double scalingFactor = 1.15;
        double thresholdY = 1000000;

        double dynamicFactor = scalingFactor + (clickUpgradeLevel / thresholdY);
        return basePrice * Math.Pow(dynamicFactor, clickUpgradeLevel);
    }


    //--- UI AND DATA MANAGEMENT!!!! ---
    void UpdateScoreText()
    {
        ScoreText.text = "BANK:$ " + totalScore.ToString("N2"); //allows to show 0.01, etc
    }
    void UpdateUI()
    {

        //Text for STATS!!!
        LifetimeEarningsText.text = "Lifetime Earnings: $" + lifetimeEarnings.ToString("N2"); //Lifetime money 

        // Text for upgrades, Click upgrades only:
        UpgradePriceText.text = "$" + GetClickUpgradeCost().ToString("N2");
        ClickLevelText.text = "(LVL: " + clickUpgradeLevel + ")";
        ClickValueText.text = "Click Value: " + GetCurrentClickValue().ToString("N2");


        //Text for upgrades, multiplier only:
        if (coinUpgradeCount < maxCoinLevels) //if coinUpgrade count is less than 4, shows prices and stuff
        {
            MultiplierPriceText.text = "$" + coinMultiplierPrice[coinUpgradeCount].ToString();
            MultiplierLevelText.text = "(LVL: " + coinUpgradeCount + ")";

            int nextValue = multiplierValues[coinUpgradeCount];
            MultiplierValueText.text = "Next: x" + nextValue;
        }
        else //shows when Mult lvl = 4
        {
            MultiplierPriceText.text = "MAXED OUT";
            MultiplierLevelText.text = "(Lvl: 4";
            MultiplierValueText.text = "CURRENT: x20";
        }

        if (AutoButtonText != null)
        {
            AutoButtonText.text = isAutoClickActive ? "ON" : "OFF";
        }

    }
    void UpdateTimePlayedText()
    {
        int hours = Mathf.FloorToInt((float)timePlayed / 3600);
        int minutes = Mathf.FloorToInt((float)timePlayed % 3600 / 60);
        int seconds = Mathf.FloorToInt((float)timePlayed % 60);

        //display
        TimePlayedText.text = string.Format("Time Played: {0:D2}:{1:D2}:{2:D2}", hours, minutes, seconds);
    }
    void SetVolume(float volume)
    {
        musicSource.volume = volume;
        sfxSource.volume = volume;
    }



    // SAVE LOAD QUIT
    void SaveGame() //saves game
    {
        PlayerPrefs.SetString("MoneyFromClicks", moneyFromClicks.ToString());
        PlayerPrefs.SetString("TotalScore", totalScore.ToString());
        PlayerPrefs.SetString("TimePlayed", timePlayed.ToString());
        PlayerPrefs.SetString("LifetimeEarnings", lifetimeEarnings.ToString());
        PlayerPrefs.SetInt("ClickUpgradeLevel", clickUpgradeLevel);
        PlayerPrefs.SetInt("CoinUpgradeCount", coinUpgradeCount);
        PlayerPrefs.SetInt("IsAutoClickActive", isAutoClickActive ? 1 : 0);
        PlayerPrefs.Save();
    }
    void LoadGame()
    {
        moneyFromClicks = double.Parse(PlayerPrefs.GetString("MoneyFromClicks", "0"));
        timePlayed = PlayerPrefs.GetFloat("TimePlayed", 0);
        totalScore = double.Parse(PlayerPrefs.GetString("TotalScore", "0")); //
        lifetimeEarnings = double.Parse(PlayerPrefs.GetString("LifetimeEarnings", "0"));
        clickUpgradeLevel = PlayerPrefs.GetInt("ClickUpgradeLevel", 0);
        coinUpgradeCount = PlayerPrefs.GetInt("CoinUpgradeCount", 0);
        isAutoClickActive = PlayerPrefs.GetInt("IsAutoClickActive", 0) == 1;

        UpdateUI(); //loads when refreshings
    }
    void OnApplicationQuit() //QUit lol
    {
        SaveGame();
    }


    //developer testing tool
    void pressThisForRestart()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ClearSave();
        }
    }
    public void ClearSave()
    {
        PlayerPrefs.DeleteAll();
        totalScore = 0; //reset
        clickUpgradeLevel = 0; //reset
        isAutoClickActive = false; //reset
        coinUpgradeCount = 0; //reset
        lifetimeEarnings = 0; //reset
        moneyFromClicks = 0; //reset
        timePlayed = 0; //reset
        autoClickTimer = 0; //reset

        UpdateUI(); //reset
        UpdateScoreText();


        Debug.Log("Save Data Wiped!"); //message
    }

} 