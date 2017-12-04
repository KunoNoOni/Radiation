using UnityEngine;

public class ComputerAI : MonoBehaviour
{
    SliderHandler sliderHandler;
    ButtonHandler buttonHandler;

    public bool IsDoingTurnCoolDown = false;
    public bool IsLotsOfTextOnScreen = false;
    float turnCooldown = 3f;
    const float cooldownReset = 3f;


    void Start()
    {
        sliderHandler = GameObject.Find("SliderHandler").GetComponent<SliderHandler>();
        buttonHandler = GameObject.Find("ButtonHandler").GetComponent<ButtonHandler>();
    }

    void Update()
    {
        if(IsLotsOfTextOnScreen)
        {
            buttonHandler.rollDiceButton.interactable = false;
            buttonHandler.passbutton.interactable = false;
            turnCooldown = 5f;
            IsLotsOfTextOnScreen = false;
        }

        if(IsDoingTurnCoolDown)
        {
            turnCooldown -= Time.deltaTime;
            if(turnCooldown <= 0)
            {
                turnCooldown = cooldownReset;
                IsDoingTurnCoolDown = false;
                TakeTurn();
            }
        }
    }

    public void TakeTurn()
    {
        if((int)sliderHandler.computerRadiationSlider.value < 7)
        {
            buttonHandler.RollDice();
        }
        else
        {
            buttonHandler.PassTurn();    
        }
    }
}