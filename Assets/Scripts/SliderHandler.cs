using UnityEngine;
using UnityEngine.UI;


public class SliderHandler : MonoBehaviour 
{
    public Slider playerRadiationSlider;
    public Slider playerFuelSlider;
    public Slider computerRadiationSlider;
    public Slider computerFuelSlider;

    StateHandler stateHandler;
    DiceHandler diceHandler;

    void Start()
    {
        stateHandler = GameObject.Find("StateHandler").GetComponent<StateHandler>();
        diceHandler = GameObject.Find("DicePanel").GetComponent<DiceHandler>();
    }


    public void UpdateSliders(int die1, int die2, int die3, int radiationBonus, int fuelBonus)
    {
        int currentRadiation = 0;
        int totalRadiation = 0;
        int fuel = 0;
        int[] dice = {die1,die2,die3};

        for (int i = 0; i < 3; i++)
        {
            currentRadiation += diceHandler.GetRadiation(dice[i]);
            fuel += diceHandler.GetFuel(dice[i]);
        }

        if(currentRadiation > 0)
        {
            if(stateHandler.currentPlayer == 0)
            {
                UpdatePlayerRadiation(currentRadiation+radiationBonus);
                totalRadiation = (int)playerRadiationSlider.value;
            }
            else
            {
                UpdateComputerRadiation(currentRadiation+radiationBonus);
                totalRadiation = (int)computerRadiationSlider.value;
            }
        }

        if(fuel > 0)
        {
            stateHandler.currentFuelForTurn += fuel+fuelBonus;

            if(stateHandler.currentPlayer == 0)
            {
                UpdatePlayerFuel(fuel+fuelBonus);
            }
            else
            {
                UpdateComputerFuel(fuel+fuelBonus);
            }

            stateHandler.CheckForGameOver();
        }

        stateHandler.CheckForMaxRadiation(totalRadiation);
    }

    public void EndTurnReduceRadiation()
    {
        if(stateHandler.currentPlayer == 0)
        {
            if((int)playerRadiationSlider.value > 1)
            {
                playerRadiationSlider.value = Mathf.FloorToInt(playerRadiationSlider.value)/2;
            }
        }
        else
        {
            if((int)computerRadiationSlider.value > 1)
            {
                computerRadiationSlider.value = Mathf.FloorToInt(computerRadiationSlider.value)/2;
            }
        }
    }

    void UpdatePlayerRadiation(int value)
    {
        if(playerRadiationSlider.value + value > 10)
        {
            playerRadiationSlider.value = 10;
        }
        else
        {
            playerRadiationSlider.value += value;
        }
    }
	
    void UpdatePlayerFuel(int value)
    {
        if(playerFuelSlider.value + value > 20)
        {
            playerFuelSlider.value = 20;
        }
        else
        {
            playerFuelSlider.value += value;
        }
    }

    void UpdateComputerRadiation(int value)
    {
        if(computerRadiationSlider.value + value > 10)
        {
            computerRadiationSlider.value = 10;
        }
        else
        {
            computerRadiationSlider.value += value;
        }
    }

    void UpdateComputerFuel(int value)
    {
        if(computerFuelSlider.value + value > 20)
        {
            computerFuelSlider.value = 20;
        }
        else
        {
            computerFuelSlider.value += value;
        }
    }
}
