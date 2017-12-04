using UnityEngine;
using System;
using System.Security.Cryptography;
using UnityEngine.UI;

public class DiceHandler : MonoBehaviour 
{
    public Sprite[] diceSprites;
    public String[] diceFaceNames;
    public Image[] dice;
    public int die1;
    public int die2;
    public int die3;
    public bool RolledTripleDoubleRadiation = false;
    public bool RolledTripleNothing = false;


    //static readonly RNGCryptoServiceProvider _generator = new RNGCryptoServiceProvider();
    //System.Random rand = new System.Random();

    DialogHandler dialogHandler;
    SliderHandler sliderHandler;
    SoundManager sm;

    void Awake() 
    {
        sm = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    void Start()
    {
        dialogHandler = GameObject.Find("DialogBorder").GetComponent<DialogHandler>();
        sliderHandler = GameObject.Find("SliderHandler").GetComponent<SliderHandler>();
    }

    public void SetDiceValues()
    {
        die1 = GetNewRandomInt();
        die2 = GetNewRandomInt();
        die3 = GetNewRandomInt();

        //die1 = GetRandomInt(0,5);
        //die2 = GetRandomInt(0,5);
        //die3 = GetRandomInt(0,5);
    }

    public void DisplayDiceAndUpdate()
    {
        sm.PlaySound(sm.sounds[2]);
        dice[0].sprite = diceSprites[die1];
        dice[1].sprite = diceSprites[die2];
        dice[2].sprite = diceSprites[die3];

        ParseDieRolls();
    }

    public int GetRadiation(int value)
    {
        if(value == 0)
        {
            return 1;
        }

        return value == 5 ? 2 : 0;
    }

    public int GetFuel(int value)
    {
        if(value == 2)
        {
            return 1;
        }

        return value == 3 ? 2 : 0;
    }

//    int GetRandomInt(int minValue, int maxValue)
//    {
//        byte[] randomNumber = new byte[1];
//        _generator.GetBytes(randomNumber);
//        double asciiValueRandomCharacter = Convert.ToDouble(randomNumber[0]);
//        double multiplier = Math.Max(0,(asciiValueRandomCharacter/255d)-0.00000000001d);
//        int range = maxValue - minValue + 1;
//        double randomValueInRange = Math.Floor(multiplier * range);
//
//        //Debug.Log("The random number is "+(int)(minValue + randomValueInRange));
//
//        return (int)(minValue + randomValueInRange);
//    }

    int GetNewRandomInt()
    {
        //This is for the System.Random line above
        //return rand.Next(0,5);

        return UnityEngine.Random.Range(0,6);
    }

    void ParseDieRolls()
    {
        int radiationBonus = 0;
        int fuelBonus = 0;


        //Debug.Log("Die1 = "+die1+" Die2 = "+die2+" Die3 = "+die3);

        if(die1 == 0 & die2 == 0 & die3 == 0)
        {
            sm.PlaySound(sm.sounds[3]);
            radiationBonus = 1;
            dialogHandler.RolledTripleSingleRadiation();
        }
        else if(die1 == 5 & die2 == 5 & die3 == 5)
        {
            sm.PlaySound(sm.sounds[4]);
            RolledTripleDoubleRadiation = true;
            dialogHandler.RolledTripleDoubleRadiation();
        }
        else if(die1 == 2 & die2 == 2 & die3 == 2)
        {
            sm.PlaySound(sm.sounds[8]);
            fuelBonus = 1;
            dialogHandler.RolledTripleSingleFuel();
        }
        else if(die1 == 3 & die2 == 3 & die3 == 3)
        {
            sm.PlaySound(sm.sounds[9]);
            fuelBonus = 2;
            dialogHandler.RolledTripleDoubleFuel();
        }
        else if((die1 == 1 | die1 == 4) & (die2 == 1 | die2 == 4) & (die3 == 1 | die3 == 4))
        {
            sm.PlaySound(sm.sounds[5]);
            RolledTripleNothing = true;
            dialogHandler.RolledTripleNothing();
        }
        else
        {
            dialogHandler.UpdateDialog(diceFaceNames[die1], diceFaceNames[die2], diceFaceNames[die3], false);    
        }

        sliderHandler.UpdateSliders(die1,die2,die3, radiationBonus, fuelBonus);
    }
}
