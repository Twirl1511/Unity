using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour
{
    public static GameControllerScript singleton;
    [Header("Weat")]
    public int WheatCounter;
    public int WheatToWin;
    [SerializeField] private int WheatPerCircleProduce;
    [SerializeField] private float SecondsToCollectWheat;
    [SerializeField] private ClockFillScript WheatClock;
    [SerializeField] private Text WheatText;
    

    [Header("Meal")]
    [SerializeField] private int MealToFeedVillage;
    [SerializeField] private float SecondsTillFeed;
    [SerializeField] private ClockFillScript MealClock;
    [SerializeField] private Text MealText;

    [Header("Warrior")]
    [SerializeField] private int WarriorCounter;
    [SerializeField] private int WarriorValue;
    [SerializeField] private float SecondsToProduceWarrior;
    [SerializeField] private int HowManyFoodEat;
    [SerializeField] private Button BuyWarriorButton;
    [SerializeField] private ClockFillScript WarriorClock;
    [SerializeField] private Text WarriorsText;

    [Header("Peasant")]
    [SerializeField] private int PeasantCounter;
    [SerializeField] private int PeasantValue;
    [SerializeField] private float SecondsToProducePeasant;
    [SerializeField] private int HowManyFoodProduce;
    [SerializeField] private Button BuyPeasantButton;
    [SerializeField] private ClockFillScript PeasantClock;
    [SerializeField] private Text PeasantsText;

    [Header("Barbarian")]

    [SerializeField] private int BarbarianCounter;
    [SerializeField] private int NewRaid;
    [SerializeField] public float SecondsTillRaid;
    [SerializeField] public float PlusSecondsToNewRaid;
    [SerializeField] public float TimeToFight;
    [SerializeField] private BarbarianRaid BarbarianRaid;
    [SerializeField] private Text BarbariansText;
    
    public enum GameEnds
    {
        Game,
        Victory,
        Loose
    }
    public GameEnds GameEnd;

    private void Awake()
    {
        if(singleton == null)
        {
            singleton = this;
        }
        else
        {
            Destroy(this);
        }
    }
    void Start()
    {
        WheatClock.MaxTime = SecondsToCollectWheat;
        WheatClock.StartLoop();
        BarbarianCounter = NewRaid + BarbarianCounter;
    }

    void Update()
    {
        CheckForWin();

        TextUpdate();

        MeatAndFoodCheck();

        BarbarianCheck();

        HungerCheck();


        if (WarriorCounter >= 1)
        {
            MealToFeedVillage = WarriorCounter * HowManyFoodEat;
            MealClock.StartLoop();
        }
        else
        {
            MealClock.StopLoop();
        }
    }

    public void CheckForWin()
    {
        if (WheatCounter >= WheatToWin)
        {
            GameEnd = GameEnds.Victory;
        }
    }
    



    public void TextUpdate()
    {
        WarriorsText.text = WarriorCounter.ToString();
        PeasantsText.text = PeasantCounter.ToString();
        WheatText.text = WheatCounter.ToString();
        MealText.text = MealToFeedVillage.ToString();
        BarbariansText.text = BarbarianCounter.ToString();
    }

    public void HungerCheck()
    {
        if (WheatCounter <= 0)
        {
            WheatCounter = 0;
            WarriorCounter--;

            if (WarriorCounter <= 0)
            {
                WarriorCounter = 0;
            }
        }
    }
    public void MeatAndFoodCheck()
    {
        // производим зерно
        if (WheatClock.State == ClockFillScript.States.Finished)
        {
            WheatProduse();
        }
        // кормим солдат
        if (MealClock.State == ClockFillScript.States.Finished)
        {
            WheatToFeed();
        }
    }
    public void BarbarianCheck()
    {
        if (BarbarianRaid.State == BarbarianRaid.States.Finished)
        {
            FightPhase();
            BarbarianRaid.State = BarbarianRaid.States.Stand;
        }
    }
    public void FightPhase()
    {

        if(BarbarianCounter > WarriorCounter)
        {
            GameEnd = GameEnds.Loose;
        }
        else
        {
            WarriorCounter -= BarbarianCounter;
            MealToFeedVillage = WarriorCounter * HowManyFoodEat;
            BarbarianCounter += NewRaid;
            SecondsTillRaid += PlusSecondsToNewRaid;
        }
    }

    
    

    public void BuyWarrior()
    {
        if(WheatCounter >= WarriorValue)
        {
            WarriorClock.MaxTime = SecondsToProduceWarrior;
            WarriorClock.FillAmmountToZero();
            WarriorClock.PlayOnce();
            BuyWarriorButton.interactable = false;
            WheatCounter -= WarriorValue;
            StartCoroutine(DelayPurchaseWarrior());
        }
    }
    IEnumerator DelayPurchaseWarrior()
    {
        yield return new WaitForSeconds(SecondsToProduceWarrior);
        WarriorCounter++;
        MealClock.StartLoop();
        MealToFeedVillage += HowManyFoodEat;
        BuyWarriorButton.interactable = true;
    }

    public void BuyPeasant()
    {
        if (WheatCounter >= PeasantValue)
        {
            PeasantClock.MaxTime = SecondsToProducePeasant;
            PeasantClock.FillAmmountToZero();
            PeasantClock.PlayOnce();
            WheatCounter -= PeasantValue;
            BuyPeasantButton.interactable = false;
            StartCoroutine(DelayPurchasePeasant());
        }
    }
    IEnumerator DelayPurchasePeasant()
    {
        yield return new WaitForSeconds(SecondsToProducePeasant);
        PeasantCounter++;
        WheatPerCircleProduce += HowManyFoodProduce;
        BuyPeasantButton.interactable = true;
    }

    


    public void WheatProduse()
    {
        WheatCounter += WheatPerCircleProduce;
    }
    public void WheatToFeed()
    {
        WheatCounter -= MealToFeedVillage;
    }


}
