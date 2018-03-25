using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
	public static bool isPaused = false;
	public GameObject pauseMenuUI;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)||Input.GetButtonDown("Pause")){
			if (isPaused){
				Resume();
			} else {
				Pause();
			}
		}
	}
	void Pause(){
		Debug.Log("pause");
		pauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
		isPaused = true;	
	}

	public void Resume(){
		Debug.Log("resume");
		pauseMenuUI.SetActive(false);
		Time.timeScale = 1f;
		isPaused = false;
	}

	public void BackToMenu(){
		SceneManager.LoadScene("MainMenu");
	}

	public void QuitGame(){
		Application.Quit();
	}
}
