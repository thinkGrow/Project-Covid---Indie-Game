using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
  

    private void Update()
    {
            void OnMouseDown()
            {
                GetComponent<SpriteRenderer>().material.color = Color.red;
                //GetComponent<LineRenderer>().enabled = true;
            }
       

    }

    void OnMouseDrag()
    {
        Transform animBone = transform.Find("Core/Right Arm");

        /*Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        animBone.position = new Vector3(newPosition.x, newPosition.y + 1);*/
    }








}
