using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; protected set; }

    public PuzzleManager CurrentPuzzle;

	// Use this for initialization
	void Start ()
    {
		if(Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
	}
}
