using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAble : MonoBehaviour
{
    bool isDragged = false;
    Vector2 followObject;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isDragged)
            transform.position = followObject;

        if (rb != null)
            rb.isKinematic = isDragged;

        isDragged = false;
    }

    public void Drag(bool value, Vector2 followObject){
        isDragged = value;
        this.followObject = followObject;
    }


}
