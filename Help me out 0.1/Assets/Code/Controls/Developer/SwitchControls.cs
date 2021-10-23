using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchControls : MonoBehaviour{
    public ControlState currentControlState;

    Player_Controller playerController;
    DeveloperControls developerControls;
    
    private void Awake() {
        currentControlState = ControlState.Player;
        playerController = FindObjectOfType<Player_Controller>();
        developerControls = FindObjectOfType<DeveloperControls>();
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
    }

    void PlayerState(){
        currentControlState = ControlState.Player;
        playerController.EnableDissable(true);
        developerControls.EnableDissable(false);
    }

}

public enum ControlState{Player, Developer}