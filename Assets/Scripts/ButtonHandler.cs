using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHandler : MonoBehaviour 
{
    
    public Button rollDiceButton;
    public Button passbutton;

    bool IsAnimating = false;
    float cooldown = 0;
    const float cooldownReset = .03f;
    int numberOfRolls = 0;
    int dieFaceValue = 0;


    DiceHandler diceHandler;
    DialogHandler dialogHandler;
    StateHandler stateHandler;
    SoundManager sm;

    void Awake() 
    {
        sm = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    void Start()
    {
        diceHandler = GameObject.Find("DicePanel").GetComponent<DiceHandler>();
        dialogHandler = GameObject.Find("DialogBorder").GetComponent<DialogHandler>();
        stateHandler = GameObject.Find("StateHandler").GetComponent<StateHandler>();
    }

    void Update()
    {
        if(IsAnimating)
        {
            cooldown -= Time.deltaTime;
            if(cooldown <= 0)
            {
                cooldown = cooldownReset;
                numberOfRolls++;
                diceHandler.dice[0].sprite = diceHandler.diceSprites[dieFaceValue];
                diceHandler.dice[1].sprite = diceHandler.diceSprites[dieFaceValue];
                diceHandler.dice[2].sprite = diceHandler.diceSprites[dieFaceValue];
                dieFaceValue++;
                if(dieFaceValue == 6)
                    dieFaceValue = 0;
                if(numberOfRolls == 59)
                {
                    numberOfRolls = 0;
                    IsAnimating = false;
                    sm.channels[1].Stop();
                    diceHandler.DisplayDiceAndUpdate();
                }
            }
        }
    }

    public void RollDice()
    {
        if(IsAnimating)
            return;

        if(EventSystem.current.currentSelectedGameObject != null)
            EventSystem.current.SetSelectedGameObject(null);

        sm.PlaySound(sm.sounds[1]);
        dialogHandler.RollingDice();
        diceHandler.SetDiceValues();
        IsAnimating = true;
    }

    public void PassTurn()
    {
        if(IsAnimating)
            return;

        if(EventSystem.current.currentSelectedGameObject != null)
            EventSystem.current.SetSelectedGameObject(null);

        dialogHandler.UpdateDialog("","","", true);
        stateHandler.NextTurn();
    }
}
