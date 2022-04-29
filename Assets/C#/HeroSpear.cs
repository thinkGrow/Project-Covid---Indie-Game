using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroSpear : MonoBehaviour
{
    public static bool GoSpear, GameEnd;
	
	public List<GameObject> Trees = new List<GameObject>();
	
	public Button Buy_Button;
	
	public GameObject CoinsPS, LeftDetails, DetectorScore, TreeGrowPos, bestScore;
	private GameObject Mytree;

	public Text ScoreText, BestScoreText, CoinsText, SpearText;
	
	public float SpearSpeed;
	private float angle;
	
	public float Pos1, Pos2;
	public static float CoinsScore, BestScore, SpearScore = 3;
	
	private int Rnd_Tree;
	
	private Vector2 v;
	
	private Rigidbody2D rb;
	private BoxCollider2D bc;
	
	void Awake(){
	
		if (PlayerPrefs.HasKey ("BestScore")) {
		
		   BestScore = PlayerPrefs.GetFloat("BestScore", BestScore);
		   BestScoreText.text = BestScore.ToString("F") +"m";
		}
		
		if (PlayerPrefs.HasKey ("CoinsScore")) {
		
		   CoinsScore = PlayerPrefs.GetFloat("CoinsScore", CoinsScore);
		   CoinsText.text = CoinsScore.ToString("F") +"$";
		   
		} else {
		
		   CoinsText.text = CoinsScore.ToString("F") +"$";
		}
		
		if (PlayerPrefs.HasKey ("SpearScore")) {
		
		   SpearScore = PlayerPrefs.GetFloat("SpearScore", SpearScore);
		   SpearText.text = SpearScore.ToString() +"X";
		}
		
	}
	
	void Start(){
	   
	    rb = GetComponent<Rigidbody2D>();
	    bc = GetComponent<BoxCollider2D>();
	}
	
    void Update()
    {
	    if(GoSpear){
			
			rb.AddForce(transform.right * SpearSpeed);
			v = rb.velocity;
	        Invoke("AdjustRotation", 1f);
			
			LeftDetails.SetActive(false);
			
		}
		
		Pos2 = transform.position.x - Pos1;
		ScoreText.text = Pos2.ToString("F") +"m";
		
	    if( CoinsScore >= 1 ){
		
	       Buy_Button.interactable = true;
		   
	    }else {
		   
		   Buy_Button.interactable = false;
		}
		
		SpearText.text = SpearScore.ToString() +"X";
    }
	
	void AdjustRotation(){
	
        angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		Invoke("SetGravity", 0.1f);
	}
	
	void SetGravity(){
	
	   rb.gravityScale = 0.3f;
	   bc.isTrigger = false;
	}
	
	void OnCollisionEnter2D(Collision2D col){
	
	    if( col.collider.tag == "Ground" )
        {
		   
		   DetectorScore.SetActive(false);
		   rb.constraints = RigidbodyConstraints2D.FreezeAll;
		   GameEnd = true;
		   GoSpear = false;
		   
		   LeftDetails.SetActive(true);
		   
		   Rnd_Tree = Random.Range(0, Trees.Count);
		   
		   Mytree = Instantiate(Trees[Rnd_Tree], new Vector3(TreeGrowPos.transform.position.x,
		   TreeGrowPos.transform.position.y, -1), Quaternion.identity);
		   
		   Invoke("EarnCoins", 0.5f);
		   
        }
		
		if( col.collider.tag == "NoGround" )
        {
		   LeftDetails.SetActive(true);
		   
		   rb.constraints = RigidbodyConstraints2D.FreezeAll;
		   GameEnd = true;
		   GoSpear = false;
		   
        }
	}
	
	void OnTriggerExit2D(Collider2D col){
	
	    if( col.gameObject.tag == "ScoreDetector" )
        {
		   Pos1 = transform.position.x;
		   ScoreText.GetComponent<Text>().enabled = true;
        }
	}
	
	void EarnCoins(){
	
	    Hero.TreeScore += 1;
		PlayerPrefs.SetFloat("TreeScore", Hero.TreeScore);
		
	    if(Pos2 >= BestScore){
		   
		   bestScore.SetActive(true);
		   BestScoreText.text = Pos2.ToString("F") +"m";
		   PlayerPrefs.SetFloat("BestScore", Pos2);
		   
		}
		
		if(Rnd_Tree == 0){
				 
		   if(Pos2 >= 30){
	   
	       CoinsScore += Pos2 /30;
		   float coinScore = Pos2 /30;
		   CoinsText.text = CoinsScore.ToString("F") +"$"+" (+"+ coinScore.ToString("F") +")";
		   PlayerPrefs.SetFloat("CoinsScore", CoinsScore);
	       }
		}
		
		if(Rnd_Tree == 1){
		 
		   if(Pos2 >= 20){
	   
	       CoinsScore += Pos2 /20;
		   float coinScore = Pos2 /20;
		   CoinsText.text = CoinsScore.ToString("F") +"$"+" (+"+ coinScore.ToString("F") +")";
		   PlayerPrefs.SetFloat("CoinsScore", CoinsScore);
	       }
		}
		
		if(Rnd_Tree == 2){
				 
		   if(Pos2 >= 10){
	   
	       CoinsScore += Pos2 /10;
		   float coinScore = Pos2 /10;
		   CoinsText.text = CoinsScore.ToString("F") +"$"+" (+"+ coinScore.ToString("F") +")";
		   PlayerPrefs.SetFloat("CoinsScore", CoinsScore);
	       }
		}
		
		GameObject MyCoins = Instantiate(CoinsPS, new Vector2(Mytree.transform.position.x,
		Mytree.transform.position.y), Quaternion.identity);
		MyCoins.transform.Rotate(0,180,0);
	}
	
	public void BuySpear(){
	
	    if( CoinsScore > 0 ){
	   
	       SpearScore += Mathf.RoundToInt(CoinsScore);
		   CoinsScore -=  Mathf.RoundToInt(CoinsScore);
		   CoinsText.text = CoinsScore.ToString("F") +"$";
		   PlayerPrefs.SetFloat("SpearScore", SpearScore);
		   PlayerPrefs.SetFloat("CoinsScore", CoinsScore);
	    }
	}

	public void NewSpear(){
	
	   Application.LoadLevel("GAME");
	}
	
		
	/*public void Del(){
	
	  PlayerPrefs.DeleteAll();
	}*/
	
}
