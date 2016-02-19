using UnityEngine;
using System.Collections;

public class LaunchThePlayer : MonoBehaviour {

	public float waitTime;

	private GameController _controller;

	private GameObject _player;
	private GameObject _renderer;

	private bool _isLaunched;

	// Use this for initialization
	void Start () {
		_isLaunched = false;
		_controller = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (_controller.GetPlayersLeft() == 0){
			StartCoroutine(_controller.CheckIfBodiesHaveStopped());
			this.enabled = false;
			return;
		}

		if (_isLaunched) {
			if (_player.transform.position.x > transform.position.x){
				GetComponent<SpringJoint2D>().enabled = false;
				_controller.ReadyNextPlayer();
				_isLaunched = false;
			}
			return;
		}

		if (Input.GetMouseButtonDown (0)) {
			_player = _controller.GetCurrentPlayer();
			//_renderer = _player.GetComponentInChildren<Renderer>().gameObject;
		}

		if (Input.GetMouseButton (0)) {
			//_player.GetComponent<Rigidbody2D>().gravityScale = 0;
			//GetComponent<SpringJoint2D>().enabled = false;
			Vector2 center = GetComponent<Collider2D>().bounds.center;
			Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			float maxradius = GetComponent<CircleCollider2D>().radius;
			Vector2 direction = - (center - mouse).normalized;
			float amplitude = (center - mouse).magnitude;
			float radius = amplitude < maxradius ? amplitude : maxradius;

			_player.transform.position = center + radius * direction;
		}

		if (Input.GetMouseButtonUp(0)){
			//_player.GetComponent<Rigidbody2D>().position = _renderer.transform.position;
			_player.GetComponent<Rigidbody2D>().isKinematic = false;
			_player.GetComponent<TrailRenderer>().enabled = true;
			//_renderer.transform.localPosition = Vector2.zero;
			GetComponent<SpringJoint2D>().connectedBody = _player.GetComponent<Rigidbody2D>();
			GetComponent<SpringJoint2D>().enabled = true;
			//_player.GetComponent<Rigidbody2D>().gravityScale = 1;
			//this.enabled = false;
			_isLaunched = true;
		}
	}
}
