using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpriteHPAnimator : HPAnimator {
	
	public List<Sprite> Sprites;

	private SpriteRenderer _renderer;
	private int _index;

	// Use this for initialization
	void Start () {
		_renderer = GetComponent<SpriteRenderer> ();
		_index = 0;
		SetSprite ();
	}
	
	//method invoked by BlockDestroyer to make animation;
	public override void SetHP(int hp){
		//index starts from zero, hp from 4;
		_index = Mathf.Clamp (4 - hp, 0, 3);
		SetSprite ();

	}
	
	//private method that applies the animation state (sprite);
	public void SetSprite(){
		_renderer.sprite = Sprites [_index];
	}

}
