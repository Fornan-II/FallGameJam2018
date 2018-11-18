using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

    public Text[] startTexts;
    GameObject player;

    Camera cam;
    Camera camScript;
    public float startSize;
    public float endSize;
    public float zoomTime = 7.0f;
    Vector3 startPos;
    public Vector3 endPos = new Vector3(0, -14, -36);

    protected bool _hasStarted = false;

	// Use this for initialization
	void Start ()
    {
        cam = Camera.main;
        camScript = cam.GetComponent<Camera>();
        //size = maxDist;
        //camScript.orthographicSize = size;

        startPos = cam.transform.localPosition;

        player = GameObject.Find("PlayerController");

        if(player.GetComponent<PlayerController>())
        {
            player.GetComponent<PlayerController>().enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.anyKey && !_hasStarted)
        {
            StartCoroutine(BeginGame());
            _hasStarted = true;
        }
	}

    protected IEnumerator BeginGame()
    {
        cam.transform.localPosition = startPos;
        startPos = cam.transform.localPosition;

        for(float timer = 0.0f; timer < zoomTime; timer += Time.deltaTime)
        {
            yield return null;

            float lerpFactor = timer / zoomTime;

            foreach(Text t in startTexts)
            {
                Color c = t.color;
                c.a *= Mathf.Lerp(1.0f, 0.0f, lerpFactor * 2);
                t.color = c;
            }

            cam.orthographicSize = Mathf.Lerp(startSize, endSize, lerpFactor);
            cam.transform.localPosition = Vector3.Lerp(startPos, endPos, lerpFactor);
        }

        cam.transform.localPosition = endPos;

        player = GameObject.Find("PlayerController");

        if (player.GetComponent<PlayerController>())
        {
            player.GetComponent<PlayerController>().enabled = true;
        }

        /*while(size > minDist)
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
        }*/
    }
}
