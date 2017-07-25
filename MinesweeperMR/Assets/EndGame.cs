using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("ResetGame", 5);
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void ResetGame()
    {
        SceneManager.LoadScene("MinesweeperMR");
    }

}
