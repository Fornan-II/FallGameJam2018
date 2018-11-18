using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour {

    public GameObject endPanel;
    GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            player = other.gameObject;

            if (player.GetComponent<PlayerController>())
            {
                player.GetComponent<PlayerController>().enabled = false;
            }

            endPanel.SetActive(true);
        }
    }
}
