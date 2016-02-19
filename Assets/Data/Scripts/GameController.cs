using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	public GameObject GameOverGUI;
	public GameObject GameWinGUI;

	public Transform Spawner;
	public GameObject Launcher;

	private List<GameObject> _players;
	private Rigidbody2D[] _bodies;
	private List<GameObject> _enemies;

	private bool _youwin;

	// Use this for initialization
	void Start () {
		_youwin = false;
		SetupPlayers ();
		AcquireEnemies ();
	}

	void Update(){
		if (!_youwin)
			CheckIfWon ();
	}

	private void SetupPlayers(){
		_players = new List<GameObject> ();
		int index = 0;
		float pos = 0;//Spawner.position.x;
		foreach (var player in GetComponent<LevelData>().Players) {
			_players.Add(Instantiate(player));
			_players[index].transform.SetParent(Spawner);
			_players[index].GetComponent<Rigidbody2D>().isKinematic = true;
			//set position
			float y = _players[index].GetComponent<Collider2D>().bounds.extents.y;
			float delta = - _players[index].GetComponent<Collider2D>().bounds.extents.x;
			//Debug.Log(delta);
			_players[index].transform.localPosition = new Vector2(pos + delta,y);
			pos += 2*delta;
			index++;
		}
	}

	private void AcquireEnemies()
	{
		_enemies = new List<GameObject> (GameObject.FindGameObjectsWithTag ("Enemy"));
	}

	private void CheckIfWon(){
		if (_enemies.Count == 0) {
			WinGame();
		}
		List<GameObject> copy = new List<GameObject> (_enemies);
		foreach (GameObject enemy in copy) {
			if (enemy == null){
				_enemies.Remove(enemy);
			}
		}
	}

	public GameObject GetCurrentPlayer(){
		if (_players.Count == 0)
			return null;
		return _players [0];
	}

	public int GetPlayersLeft(){
		return _players.Count;
	}

	public void ReadyNextPlayer(){
		if (_players.Count > 0) {
			_players.RemoveAt (0);
		}
	}

	public IEnumerator CheckIfBodiesHaveStopped(){
		bool _allstopped = false;
		_bodies = GameObject.FindObjectsOfType (typeof(Rigidbody2D)) as Rigidbody2D[];
		while (!_allstopped) {
			_allstopped = true;
			foreach (Rigidbody2D body in _bodies) {
				if (body == null){
					continue;
				}
				if (! body.IsSleeping ()) {
					_allstopped = false;
					yield return null;
					break;
				}
			}
			yield return null;
		}
		if(!_youwin)
		GameOver ();
		//Debug.Log ("end of cycle");
	}
	
	private void GameOver(){
		Launcher.GetComponent<LaunchThePlayer> ().enabled = false;
		GameOverGUI.SetActive (true);
		//Time.timeScale = 0;
	}

	private void WinGame(){
		Launcher.GetComponent<LaunchThePlayer> ().enabled = false;
		_youwin = true;
		GetComponent<ScoreController> ().AddScore (10000 * GetPlayersLeft ());
		GameWinGUI.SetActive (true);
		//Time.timeScale = 0;
	}
	
	public void RestartLevel(){
		Time.timeScale = 1;
		Application.LoadLevel (Application.loadedLevel);
	}
}
