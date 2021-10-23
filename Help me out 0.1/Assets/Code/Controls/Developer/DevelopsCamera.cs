using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevelopsCamera : MonoBehaviour
{
    Transform player;
    SwitchControls switchControls;
    void Awake()
    {
        switchControls = FindObjectOfType<SwitchControls>();
        player = FindObjectOfType<Player_Controller>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(switchControls.currentControlState == ControlState.Player){
            
        }
    }

    private void OnEnable() {
        transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
    }
}
