using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ColorHPAnimator : HPAnimator {

	public List<Color> Colors;

	private Renderer myrenderer;
	private int index;

	//class Initialization;
	void Start () {
		myrenderer = GetComponent<Renderer> ();
		index = 0;
		SetColor ();
	}

	//method invoked by BlockDestroyer to make animation;
	public override void SetHP(int hp){
		//index starts from zero, hp from 4;
		index = Mathf.Clamp (4 - hp, 0, 3);
		SetColor ();
	}

	//private method that applies the animation state (color);
	private void SetColor(){
		myrenderer.material.color = Colors [index];
	}


}
