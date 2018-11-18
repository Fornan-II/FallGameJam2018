using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    
    public Text[] endTexts;
    GameObject player;

    public Camera cam;
    public float startSize;
    public float endSize;
    public float zoomTime = 7.0f;
    Vector3 startPos;
    public Vector3 endPos = new Vector3(0, -14, -36);

    protected bool _endStarted = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(!_endStarted)
            {
                StartCoroutine(EndTheGame());
                _endStarted = true;
            }

            //endPanel.SetActive(true);
        }
    }

    protected IEnumerator EndTheGame()
    {
        player = GameObject.Find("PlayerController");

        if (player.GetComponent<PlayerController>())
        {
            player.GetComponent<PlayerController>().enabled = false;
        }

        startPos = cam.transform.position;

        for (float timer = 0.0f; timer < zoomTime; timer += Time.deltaTime)
        {
            yield return null;

            float lerpFactor = timer / zoomTime;

            foreach (Text t in endTexts)
            {
                Color c = t.color;
                c.a = Mathf.Lerp(0.0f, 1.0f, lerpFactor * 1.3f);
                t.color = c;
                Debug.Log(c.a);
            }

            cam.orthographicSize = Mathf.Lerp(startSize, endSize, lerpFactor);
            cam.transform.position = Vector3.Lerp(startPos, endPos, lerpFactor);
        }

        cam.transform.position = endPos;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
