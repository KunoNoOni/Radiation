using UnityEngine;
using UnityEngine.SceneManagement;


public class StateHandler : MonoBehaviour 
{
    public int currentPlayer;
    public int currentFuelForTurn;

    SliderHandler sliderHandler;
    DialogHandler dialogHandler;
    ComputerAI computerAI;
    DiceHandler diceHandler;
    ButtonHandler buttonHandler;

    float turnCooldown = 2;
    const float cooldownReset = 2f;
    bool IsDoingTurnCoolDown = false;
    bool IsGameOverWait = false;
    bool youLose = false;
    bool muteSounds = false;
    float soundVolume = .5f;
    float maxVolume = 1f;
    float muteVolume = 0;

    SoundManager sm;

    void Awake() 
    {
        sm = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        sm.SetSoundVolume(.5f);
    }

    void Start()
    {
        sliderHandler = GameObject.Find("SliderHandler").GetComponent<SliderHandler>();
        dialogHandler = GameObject.Find("DialogBorder").GetComponent<DialogHandler>();
        computerAI = GameObject.Find("ComputerAI").GetComponent<ComputerAI>();
        diceHandler = GameObject.Find("DicePanel").GetComponent<DiceHandler>();
        buttonHandler = GameObject.Find("ButtonHandler").GetComponent<ButtonHandler>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if(Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            if(soundVolume < maxVolume)
            {
                soundVolume+= .1f;
                sm.SetSoundVolume(soundVolume);
            }
        }

        if(Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            if(soundVolume > muteVolume)
            {
                soundVolume-= .1f;
                sm.SetSoundVolume(soundVolume);
            }
        }

        if(Input.GetKeyDown(KeyCode.M))
        {
            muteSounds = !muteSounds;
            sm.MuteSounds(muteSounds);
        }

        if(IsDoingTurnCoolDown)
        {
            turnCooldown -= Time.deltaTime;
            if(turnCooldown <= 0)
            {
                turnCooldown = cooldownReset;
                IsDoingTurnCoolDown = false;
                NextTurn();
            }
        }

        if(IsGameOverWait)
        {
            turnCooldown -= Time.deltaTime;
            if(turnCooldown <= 0)
            {
                turnCooldown = cooldownReset;
                IsGameOverWait = false;
                if(youLose)
                    SceneManager.LoadScene("_LOSE_");
                else
                    SceneManager.LoadScene("_WIN_");
            }
        }
    }

    public void NextTurn()
    {
        if(!diceHandler.RolledTripleNothing)
        {
            sliderHandler.EndTurnReduceRadiation();
        }
        else
        {
            diceHandler.RolledTripleNothing = false;
        }

        currentFuelForTurn = 0;

        currentPlayer = (currentPlayer + 1 ) % 2;

        if(currentPlayer == 1)
        {
            buttonHandler.rollDiceButton.interactable = false;
            buttonHandler.passbutton.interactable = false;
            computerAI.IsDoingTurnCoolDown = true;
        }
        else
        {
            buttonHandler.rollDiceButton.interactable = true;
            buttonHandler.passbutton.interactable = true;
        }
    }

    public void CheckForGameOver()
    {
        int playerFuel = (int)sliderHandler.playerFuelSlider.value;
        int computerFuel = (int)sliderHandler.computerFuelSlider.value;

        if(computerFuel == 20)
        {
            youLose = true;
            turnCooldown = 1f;
            IsGameOverWait = true;
        }
        else if(playerFuel == 20)
        {
            youLose = false;
            turnCooldown = 1f;
            IsGameOverWait = true;
        }
    }

    public void CheckForMaxRadiation(int radiation)
    {
        if(radiation == 10)
        {
            if(currentPlayer == 0)
            {
                sliderHandler.playerFuelSlider.value -= currentFuelForTurn;
            }
            else
            {
                sliderHandler.computerFuelSlider.value -= currentFuelForTurn;
            }

            dialogHandler.TooMuchRadiationMessage();
            this.IsDoingTurnCoolDown = true;
        }
        else if(diceHandler.RolledTripleDoubleRadiation)
        {
            if(currentPlayer == 0)
            {
                sliderHandler.playerFuelSlider.value -= currentFuelForTurn;
            }
            else
            {
                sliderHandler.computerFuelSlider.value -= currentFuelForTurn;
            }

            diceHandler.RolledTripleDoubleRadiation = false;
            this.IsDoingTurnCoolDown = true;
        }
        else if(diceHandler.RolledTripleNothing)
        {
            this.IsDoingTurnCoolDown = true;
        }
        else
        {
            if(currentPlayer == 1)
                computerAI.IsDoingTurnCoolDown = true;
        }
    }
}