using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    public float Speed;
	public float WaitBeforeThrow;
	public float RotateValue;
	public static float TreeScore;
	
	public GameObject Tuto, ArmThrow, ArmUp, Spear, MainCamera;
	
	public bool MoveForward, ThrowSpear, SpearRotate, SpaceClicked;
	
    private Vector3 offset;
	
	private Animator mainCamera, hero;
	
	public Text TreeText;
	
	void Awake(){
	
		if (PlayerPrefs.HasKey ("TreeScore")) {
		
		   TreeScore = PlayerPrefs.GetFloat("TreeScore", TreeScore);
		   
		}
	}
	
	void Start()
	{
	
		HeroSpear.GoSpear = false;
		HeroSpear.GameEnd = false;
		
		offset = MainCamera.transform.position - transform.position - new Vector3(0f, 0.36f, 0f);
		mainCamera = MainCamera.GetComponent<Animator>();
		hero = GetComponent<Animator>();
	}

	void LateUpdate()
	{
	    if(MoveForward){
		
           MainCamera.transform.position = transform.position + offset;
	    }
		
		if(HeroSpear.GoSpear || HeroSpear.GameEnd){
		   
		   mainCamera.enabled = true;
		   MainCamera.transform.position = new Vector3( Spear.transform.position.x,
		   MainCamera.transform.position.y, MainCamera.transform.position.z);
		}
	}
	
    void Update()
    {
	    TreeText.text = TreeScore.ToString();
		
	    if(!HeroSpear.GameEnd){
		
		if(!SpaceClicked){
		
	       if(Input.GetKeyDown("space") && !MoveForward){
		
		       MoveForward = true;
		       ArmUp.SetActive(true);
		       Invoke("WaitSeconds", WaitBeforeThrow);
			   SpaceClicked = true;
	       }
		}
	
	    if(MoveForward){
		
		   hero.SetBool("Move", true);
           transform.position += Vector3.right * Speed;
	    }
		
	    if(ThrowSpear){
	   
	        if(Input.GetMouseButtonDown(0)){
			
		       ArmUp.SetActive(false);
		       ArmThrow.SetActive(true);
			   Spear.SetActive(true);
			   SpearRotate = true;
	        }
			
			if(Input.GetMouseButtonUp(0)){
			
			   hero.SetBool("Move", false);
			   hero.SetBool("Throw", true);
			   ArmThrow.SetActive(false);
			   MoveForward = false;
			   ThrowSpear = false;
			   SpearRotate = false;
			   
			   HeroSpear.GoSpear = true;
			   
			   HeroSpear.SpearScore -= 1;
			}
	    }
		
		if(SpearRotate){
		
		   RotateValue += 1f;
		   Vector3 CurrentRotation = Spear.transform.localRotation.eulerAngles;
		   CurrentRotation.z = Mathf.Clamp(RotateValue, 8.95f, 80f);
		   Spear.transform.localRotation = Quaternion.Euler(CurrentRotation);
		}
		
		if(!HeroSpear.GoSpear){
		
		    Spear.transform.position = new Vector3(transform.position.x - 0.2f, Spear.transform.position.y, -3);
		}
		
		}
    }
	
	void WaitSeconds(){
	   ThrowSpear = true;
	}
	
	void OnCollisionEnter2D(Collision2D col){
	
	    if( col.collider.tag == "Limits" )
        {
		   HeroSpear.GameEnd = true;
        }
	}
	
	public void OK(){
	   Tuto.SetActive(false);
	}
}
