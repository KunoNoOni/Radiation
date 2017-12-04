using UnityEngine;
using UnityEngine.UI;
using System;

public class DialogHandler : MonoBehaviour 
{
    public Text dialog;

    string pronoun;

    StateHandler stateHandler;
    ComputerAI computerAI;

    void Start()
    {
        stateHandler = GameObject.Find("StateHandler").GetComponent<StateHandler>();
        computerAI = GameObject.Find("ComputerAI").GetComponent<ComputerAI>();
    }

    public void UpdateDialog(String die1, String die2, String die3, bool passTurn)
    {
        if(passTurn)
        {
            this.dialog.text = "Passing Turn.";
        }
        else
        {
            if(stateHandler.currentPlayer == 0)
                pronoun = "You";
            else
                pronoun = "The Computer";
            //this.dialog.text += string.Format(" <color=black>Rolled</color> {0} <color=black>&</color> {1} <color=black>&</color> {2}", die1, die2, die3);
            this.dialog.text = string.Format("<color=black>{0} Rolled</color>\n{1}\n{2}\n{3}", pronoun, die1, die2, die3);
        }
    }

    public void RollingDice()
    {
        this.dialog.text = "Rolling...";
    }

    public void TooMuchRadiationMessage()
    {
        if(stateHandler.currentPlayer == 0)
        {
            this.dialog.text = "Maximum radiation exposure! All the fuel found on this turn is lost. " +
                "Your turn is over but your radiation levels will be decreased by 1/2.";
            computerAI.IsLotsOfTextOnScreen = true;
        }
        else
            this.dialog.text = "Maximum radiation exposure! All the fuel found on this turn is lost. " +
                "His turn is over but his radiation levels will be decreased by 1/2.";
    }

    public void RolledTripleSingleRadiation()
    {
        if(stateHandler.currentPlayer == 0)
            this.dialog.text = "You rolled Triple Radiation! You take an additional exposure level of radiation.";
        else
            this.dialog.text = "The Computer rolled Triple Radiation! It takes an additional exposure level of radiation.";
    }

    public void RolledTripleDoubleRadiation()
    {
        if(stateHandler.currentPlayer == 0)
        {
            this.dialog.text = "You rolled Triple Double Radiation! You have lost all the fuel you found this turn and your turn is over!";
            computerAI.IsLotsOfTextOnScreen = true;
        }
        else
            this.dialog.text = "The Computer rolled Triple Double Radiation! All the fuel found on this turn is lost and it's turn is over!";
    }

    public void RolledTripleSingleFuel()
    {
        if(stateHandler.currentPlayer == 0)
            this.dialog.text = "You rolled Triple Fuel! You gain an additional gallon of Fuel!";
        else
            this.dialog.text = "The Computer rolled Triple Fuel! It gains an additional gallon of Fuel!";
    }

    public void RolledTripleDoubleFuel()
    {
        if(stateHandler.currentPlayer == 0)
            this.dialog.text = "You rolled Triple Double Fuel! You gain 2 extra gallons of Fuel!";
        else
            this.dialog.text = "The Computer rolled Triple Double Fuel! It gains 2 extra gallons of Fuel!";
    }

    public void RolledTripleNothing()
    {
        if(stateHandler.currentPlayer == 0)
        {
            this.dialog.text = "You rolled Triple Nothing! Your turn is over and there is no radiation healing!";
            computerAI.IsLotsOfTextOnScreen = true;
        }
        else
            this.dialog.text = "The Computer rolled Triple Nothing! It's turn is over and there is no radiation healing!";
    }
}
