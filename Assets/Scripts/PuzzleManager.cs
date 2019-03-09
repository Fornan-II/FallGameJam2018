using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public int PuzzleNumber;
    public GameObject[] PuzzleTrackedObjects;
    public Transform RespawnPoint;

    protected Vector3[] _startingPositions;
    protected Quaternion[] _startingRotations;

    protected virtual void Start()
    {
        _startingPositions = new Vector3[PuzzleTrackedObjects.Length];
        _startingRotations = new Quaternion[PuzzleTrackedObjects.Length];

        for(int i = 0; i < PuzzleTrackedObjects.Length; i++)
        {
            _startingPositions[i] = PuzzleTrackedObjects[i].transform.position;
            _startingRotations[i] = PuzzleTrackedObjects[i].transform.rotation;
        }
    }

    public virtual void ResetPuzzle()
    {
        for(int i = 0; i < PuzzleTrackedObjects.Length; i++)
        {
            PuzzleTrackedObjects[i].transform.position = _startingPositions[i];
            PuzzleTrackedObjects[i].transform.rotation = _startingRotations[i];

            BoxDeployer bd = PuzzleTrackedObjects[i].GetComponent<BoxDeployer>();
            if(bd)
            {
                bd.DestroyBox();
            }
            PoweredExtendedBridge peb = PuzzleTrackedObjects[i].GetComponent<PoweredExtendedBridge>();
            if(peb)
            {
                peb.ResetBridge();
            }
        }
    }

    public virtual void RespawnPlayer(GameObject player)
    {
        if(RespawnPoint)
        {
            player.transform.position = RespawnPoint.position;
        }
        else
        {
            player.transform.position = transform.position;
        }
        

        ResetPuzzle();
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        if(GameManager.Instance.CurrentPuzzle)
        {
            if(GameManager.Instance.CurrentPuzzle.PuzzleNumber < PuzzleNumber)
            {
                GameManager.Instance.CurrentPuzzle = this;
            }
        }
        else
        {
            GameManager.Instance.CurrentPuzzle = this;
        }
        
    }
}
