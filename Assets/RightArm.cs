using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RightArm : MonoBehaviour
{
    public float offset;
    public GameObject projectile;
    public Transform shotPoint;

    void Update ()
    {
        if(Input.GetMouseButtonDown(0)){

         //   ArmUp.SetActive(false);
         //   ArmThrow.SetActive(true);
           // Spear.SetActive(true);
           // SpearRotate = true;
        }

        // Handles the weapon rotation
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        // Calculating the direction betn weapon and mouse cursor..
        //direction = cursor.Pos - weapon.Pos
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        //The amount of degrees the weapon must rotate to face the cursor. So now we know which direction the weapon must be facing.
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);



       
    }
   
private void OnMouseDrag()
    {/*
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(newPosition.x, newPosition.y + 1); */
        
    }

}
