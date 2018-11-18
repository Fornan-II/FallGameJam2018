using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

    public GameObject startPanel;
    GameObject player;

    public Camera cam;
    Camera camScript;
    float size;
    float maxDist = 70;
    float minDist = 5;
    Vector3 startPos;
    Vector3 endPos;

	// Use this for initialization
	void Start () {
        startPanel.SetActive(true);

        cam = Camera.main;
        camScript = cam.GetComponent<Camera>();
        size = maxDist;
        camScript.orthographicSize = size;

        startPos = cam.transform.position;

        player = GameObject.Find("Player");
        endPos = new Vector3(0, -15, -36);

        if(player.GetComponent<PlayerController>())
        {
            player.GetComponent<PlayerController>().enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {

        camScript.orthographicSize = size;

        if (Input.anyKey)
        {
            BeginGame();
        }
	}

    void BeginGame()
    {
        startPanel.SetActive(false);
        cam.transform.localPosition = Vector3.Lerp(startPos, endPos, 5000 * Time.deltaTime);

        while(size > minDist)
        {
            size -= 0.1f * Time.deltaTime;
            
            if (size <= minDist)
            {
                player = GameObject.Find("Player");

                if (player.GetComponent<PlayerController>())
                {
                    player.GetComponent<PlayerController>().enabled = true;
                }
                return;
            }
        }
    }
}
