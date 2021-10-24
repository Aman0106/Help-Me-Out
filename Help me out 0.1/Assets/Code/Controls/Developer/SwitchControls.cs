using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchControls : MonoBehaviour{
    public ControlState currentControlState;

    Player_Controller playerController;
    DeveloperControls developerControls;

    [SerializeField] GameObject playerUI, developerUI;
    
    private void Start() {
        currentControlState = ControlState.Player;
        playerController = FindObjectOfType<Player_Controller>();
        developerControls = FindObjectOfType<DeveloperControls>();
        PlayerState();
    }

    public void SwapControl(){
        switch (currentControlState){
            case ControlState.Player:{
                DeveloperState();
                break;
            }
            case ControlState.Developer:{
                PlayerState();
                break;
            }
        }
    }

    void DeveloperState(){
        currentControlState = ControlState.Developer;
        developerControls.EnableDissable(true);
        playerController.EnableDissable(false);

        developerUI.SetActive(true);
        playerUI.SetActive(false);

        GameManager.Instance.SetTranseperency(0.5f);

        if(HelpTurns.Instance != null){
            HelpTurns.Instance.UpdateTurns();
        }
    }

    void PlayerState(){
        currentControlState = ControlState.Player;

        playerController.EnableDissable(true);
        developerControls.EnableDissable(false);

        playerUI.SetActive(true);
        developerUI.SetActive(false);

        GameManager.Instance.SetTranseperency(1f);
    }

}

public enum ControlState{Player, Developer}