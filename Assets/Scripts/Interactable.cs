using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    //The returned Vector3 is the position the interacter should snap to
    public abstract Vector3 Interact(GameObject interacter);
}
