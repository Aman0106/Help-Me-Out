using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutLevel1 : MonoBehaviour
{
    [SerializeField] GameObject expandButton;
    Animator animator;
    bool shown = false;

    private void Start() {
        expandButton.SetActive(false);
        animator = GetComponent<Animator>();
    }

    private void Update() {
        if(GameManager.Instance.GetCurLives() <= 3 && !shown){
            ActivateButton();

            shown = true;
        }
    }

    public void ActivateButton(){
        expandButton.SetActive(true);
        animator.SetTrigger("Help");
    }
}
