using System;
using System.Collections.Generic;
using UnityEngine;

public class GamePieceManager : MonoBehaviour {

    public GameObject Number;


    // Use this for initialization
    void Start () {
        gameObject.GetComponent<Renderer>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        //if same x,y position as player
        if ((Math.Abs(Camera.main.transform.position.x - gameObject.transform.position.x) <= gameObject.transform.localScale.x / 2) &&
           (Math.Abs(Camera.main.transform.position.z - gameObject.transform.position.z) <= gameObject.transform.localScale.z / 2))
        {
            // generate newNumber
            GameObject newNumber;
            newNumber = Instantiate(Number, GameBoardManager.Instance.transform, true);
            newNumber.transform.position = gameObject.transform.position;
            newNumber.transform.rotation = gameObject.transform.rotation;
            newNumber.transform.localScale = gameObject.transform.localScale;
            newNumber.GetComponent<TextMesh>().text = countAdjacentMines().ToString();
            newNumber.SetActive(true);
            // destroy
            Destroy(gameObject);
        }


    }

    private int countAdjacentMines() {
        int mineCount = 0;
        // these are the distances betweeen cells 
        float cellXOffset = 1;
        float cellYOffset = 1;
        //pseudo code for finding grid neighbors from https://stackoverflow.com/questions/652106/finding-neighbours-in-a-two-dimensional-array
        List<Vector2> deltas = new List<Vector2> { new Vector2(-cellXOffset, -cellYOffset),
                                                   new Vector2(0, -cellYOffset),
                                                   new Vector2(cellXOffset, -cellYOffset),                       
                                                   new Vector2(-cellXOffset, 0),
                                                   new Vector2(cellXOffset, 0),
                                                   new Vector2(-cellXOffset, cellYOffset),
                                                   new Vector2(0, -cellYOffset),
                                                   new Vector2(cellXOffset, cellYOffset)
                                                  };
        
        // Remove first 5 characters (i.e. the "cell:")
        string[] currentCellArray = gameObject.transform.name.Substring(5).Split(',');
        Vector2 currentCell = new Vector2(Convert.ToInt32(currentCellArray[0]), Convert.ToInt32(currentCellArray[1]));
        foreach (Vector2 delta in deltas) {
            Transform cell = GameBoardManager.Instance.transform.Find("cell:" + (currentCell.x + delta.x).ToString() + "," + (currentCell.y + delta.y).ToString());
            //can potentially optomize by checking grid bounds to prevent null case
            if (cell != null && cell.tag == "Mine") {
                mineCount++;
            }
        }
        return mineCount;
    }
}

