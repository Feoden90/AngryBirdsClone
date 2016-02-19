using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

	public Text ScoreText;

	private int _score;

	void Start(){
		_score = 0;
		ChangeScoreText ();
	}

	public void AddScore(int value){
		_score += value;
		ChangeScoreText ();
	}

	public int GetScore(){
		return _score;
	}

	private void ChangeScoreText(){
		ScoreText.text = "Score = " + _score;
		//Debug.Log (_score);
	}

}
