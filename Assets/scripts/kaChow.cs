using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kaChow : MonoBehaviour {

	private static kaChow instance;
	public static kaChow Instance{get{ return instance; }}

	public int fbLogin = 0;

	void Start () {
		instance = this;
//		DontDestroyOnLoad (gameObject);

		if (PlayerPrefs.HasKey ("init")) 
		{
			fbLogin = PlayerPrefs.GetInt ("fbLogin");
		} 
		else 
		{
			PlayerPrefs.SetInt ("init", 1);
			Save ();
		}
	}

	public void Save()
	{
		PlayerPrefs.SetInt ("fbLogin", fbLogin);
	}

}
