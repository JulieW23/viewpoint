using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class Level1Theme : MonoBehaviour {
	public Sound sound;
	public static Level1Theme instance;
	public static int buildIndex;
	Scene currentScene;
	static List<GameObject> gameObjs = new List<GameObject>();
	public static void DontDestroyOnLoadCustom(GameObject g) {
		//Debug.Log("func pass");
		gameObjs.Add(g);
    	UnityEngine.Object.DontDestroyOnLoad(g);
    	//Debug.Log("count: " + gameObjs.Count);
		/*if (gameObjs.Count > 1){ //and audio not the current scene
			Debug.Log("if");
			gameObjs.Remove(gameObjs[0]);
			UnityEngine.Object.Destroy(gameObjs[0]);
			
		}*/
		
    }

    public static void DestroyAt(int index) {
		//bool flag = true;
		Debug.Log(gameObjs);
        foreach(var go in gameObjs){
			Debug.Log("audio");
			Debug.Log(buildIndex);
            if(go != null){
                //UnityEngine.Object.Destroy(go);
				//flag = false;
			}
		}
        //gameObjs.Clear();
    }
	void Awake(){
		//if at same scene, don't destroy obj, otherwise start over
		//currentScene = SceneManager.GetActiveScene ();
		//if (currentScene.name == SceneName){
		if (instance==null){
			instance = this;
		}else{
			//Debug.Log("destroy");
			Destroy(gameObject);
			return;
		}
		
		DontDestroyOnLoad(gameObject);
		
		sound.source = gameObject.AddComponent<AudioSource>();
		sound.source.clip = sound.clip;
		sound.source.volume = sound.volume;
		//sound.source.pitch = sound.pitch;
		sound.source.loop = sound.loop;

	}

	void Start () {
		sound.source.Play();
	}
	
	// Update is called once per frame

}
