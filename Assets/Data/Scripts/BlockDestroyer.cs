using UnityEngine;
using System.Collections;

public class BlockDestroyer : MonoBehaviour {

	public float DamageThreshold;

	public int ScoreValue;

	private HPAnimator _spriteanim;
	[SerializeField]
	private int HealthPoints;

	// Use this for initialization
	void Start () {
		HealthPoints = 4;
		_spriteanim = this.GetComponent<HPAnimator> ();
	}

	void OnCollisionEnter2D(Collision2D collision){

		Vector2 velocity = collision.relativeVelocity;
		float speed = velocity.magnitude;
		float coefficient = DamageThreshold;

		//MAKING CALCULATIONS TO REDUCE HP
		while (speed > coefficient) {
			HealthPoints--;

			//animating sprite
			if (_spriteanim != null){
				_spriteanim.SetHP(HealthPoints);
			}

			if (HealthPoints <= 0) {
				DestroyBlock();
			}
			speed -= coefficient;
		}
	}
	
	private void DestroyBlock(){
		GameObject.FindGameObjectWithTag ("GameController").GetComponent<ScoreController> ().AddScore (ScoreValue);
		Destroy (gameObject);
	}

}
