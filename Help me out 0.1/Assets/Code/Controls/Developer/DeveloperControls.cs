using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DeveloperControls : MonoBehaviour{
    Rigidbody2D rb;
    public Camera cm;

    PlayerInputActions dragControls;
  
    bool isDragging;
    DragAble currentlyHolding;

    private void Awake() {
        dragControls = new PlayerInputActions();
        EnableDissable(false);

        dragControls.DeveloperControls.MouseDown.started += MouseDown;
        dragControls.DeveloperControls.MouseDown.canceled += MouseUp;
        cm.gameObject.SetActive(false);
    }

    private void OnMouseDrag() {

        Vector3 objPos = cm.ScreenToWorldPoint(Input.mousePosition);
        
        objPos.z = 0;

        transform.position = objPos;

        print("Dragging");

        // rb.isKinematic = true;
    }

    private void Update() {
        if(isDragging)
            DetectObject();
    }

    void DetectObject(){
        Collider2D col = Physics2D.OverlapCircle(GetMousePos(), 0.1f);
        if(col == null)
            return;
        currentlyHolding = col.GetComponent<DragAble>();

        if(currentlyHolding != null)
            currentlyHolding.Drag(true, GetMousePos());
    }

    Vector2 GetMousePos(){
        Vector2 pos = cm.ScreenToWorldPoint(dragControls.DeveloperControls.MousePosition.ReadValue<Vector2>());
        return pos;
    }
    

    private void MouseDown(InputAction.CallbackContext ctx) {
        isDragging = true;
    }

    void MouseUp(InputAction.CallbackContext ctx){
        isDragging = false;
        if(currentlyHolding != null){
            currentlyHolding.Drag(false, Vector2.one);
            currentlyHolding = null;
        }
    }

    public void EnableDissable(bool val){
        if(val){
            dragControls.DeveloperControls.Enable();
            cm.gameObject.SetActive(true);
        }
        else{
            dragControls.DeveloperControls.Disable();
            cm.gameObject.SetActive(false);
        }
    }

}
