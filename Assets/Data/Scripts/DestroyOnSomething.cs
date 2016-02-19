using UnityEngine;
using System.Collections;

public class DestroyOnSomething : MonoBehaviour {
	
	public float Lifetime;

	public bool DestroyByTime;
	public bool DestroyIfOutsideLevel;
	public bool DestroyIfStandingStill;

	private bool _flag = false;

	// Use this for initialization
	void Start () {
		if (DestroyByTime && !DestroyIfOutsideLevel && !DestroyIfStandingStill) {

		}
	
	}
	
	// Update is called once per frame
	void Update () {
		if (DestroyByTime) {
			_flag = true;
		}
		if (DestroyIfOutsideLevel) {
			//Camera.main.
		}
		if (DestroyIfStandingStill) {
			if (GetComponent<Rigidbody2D>().IsSleeping() && !GetComponent<Rigidbody2D>().isKinematic){
				_flag = true;
			} else {
				_flag = false;
			}
		}



		if (_flag) {
			Destroy(gameObject,Lifetime);
			this.enabled = false;
		}
	}


}
