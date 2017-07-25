using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineManager : MonoBehaviour {

    public GameObject Explosion;
    private Vector2 playerPosition;


    // Use this for initialization
    void Start () {
   //     gameObject.GetComponent<Renderer>().enabled = false;

    }

    // Update is called once per frame
    void Update () {
        //if same x,y position as camera explode
        if ((System.Math.Abs(Camera.main.transform.position.x - gameObject.transform.position.x) <= gameObject.transform.localScale.x / 2) &&
            (System.Math.Abs(Camera.main.transform.position.z - gameObject.transform.position.z) <= gameObject.transform.localScale.z / 2)) {
            GameObject newExplosion;
            newExplosion = Instantiate(Explosion, GameBoardManager.Instance.transform, true);
            newExplosion.transform.position = gameObject.transform.position;
            newExplosion.transform.rotation = gameObject.transform.rotation;
            newExplosion.transform.localScale = gameObject.transform.localScale;
            newExplosion.SetActive(true);
            Destroy(gameObject);
        }
    }
}
