using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
	public static bool isPaused;
	public GameObject pauseMenuUI;
	private Scene currentScene;
	public Text firstLevel;
	public Text secondLevel;
	public GameObject firstButton;
	public GameObject secondButton;
	List<int> notCurrent = new List<int> { 1, 2, 3 };

	void Awake () {
		currentScene = SceneManager.GetActiveScene ();
		notCurrent.Remove(currentScene.buildIndex);
		firstLevel.text = "";
		LevelSelection();
		isPaused = false;
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)||Input.GetButtonDown("Pause")){
			Debug.Log("press1");
			if (isPaused){
				Debug.Log("press2");
				Resume();
			} else {
				Debug.Log("press3");
				Pause();
			}
		}
	}
	void Pause(){
		pauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
		isPaused = true;	
	}

	public void Resume(){
		pauseMenuUI.SetActive(false);
		Time.timeScale = 1f;
		isPaused = false;
		firstButton.SetActive(false);
		secondButton.SetActive(false);
	}

	public void BackToMenu(){
		SceneManager.LoadScene("MainMenu");
	}

	public void Restart(){
		SceneManager.LoadScene(currentScene.name);
	}
	public void LevelSelectButton(){
		firstButton.SetActive(true);
		secondButton.SetActive(true);
	}
	public void SetLevel1(){
		SceneManager.LoadScene(notCurrent[0]);
	}
	public void SetLevel2(){
		SceneManager.LoadScene(notCurrent[1]);
	}
	private void LevelSelection(){
		foreach (var i in notCurrent){
			if (i == 1){
				firstLevel.text = "Level 1";
			}else if (i == 2){
				if (firstLevel.text == ""){
					firstLevel.text = "Level 2";
				}else{
					secondLevel.text = "Level 2";
				}
			}else{ //i==3
				secondLevel.text = "Level 3";
			}
		}
	}
	public void QuitGame(){
		Application.Quit();
	}
}
