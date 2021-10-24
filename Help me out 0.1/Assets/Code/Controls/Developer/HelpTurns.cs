using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpTurns : MonoBehaviour
{
    [SerializeField] Button expandButton;
    [SerializeField] int turns;
    [SerializeField] Text plTurnsLeftText;
    [SerializeField] Text dlTurnsLeftText;

    private static HelpTurns instance;
    public static HelpTurns Instance{
        get{
            if(instance == null)
                instance = FindObjectOfType<HelpTurns>();

            return instance;
        }
    }

    void Start()
    {
        plTurnsLeftText.text = "Turns = " + turns.ToString();
        dlTurnsLeftText.text = "Turns = " + turns.ToString();
    }

    

    public void UpdateTurns(){
        if(--turns <= 0){
            expandButton.interactable = false;
        }

        plTurnsLeftText.text = "Turns = " + turns.ToString();
        dlTurnsLeftText.text = "Turns = " + turns.ToString();

    }
}
