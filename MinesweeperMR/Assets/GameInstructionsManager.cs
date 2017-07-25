using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstructionsManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var soundManager = GameObject.Find("Audio Manager");
        TextToSpeechManager textToSpeech = soundManager.GetComponent<TextToSpeechManager>();
        textToSpeech.Voice = TextToSpeechVoice.Mark;
        textToSpeech.SpeakText("Please walk around and map your room to start the game!");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
