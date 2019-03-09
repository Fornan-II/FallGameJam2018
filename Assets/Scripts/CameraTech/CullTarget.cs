using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CullTarget : MonoBehaviour
{
    public static Dictionary<CullTarget, bool> sceneTargets;

    protected MeshRenderer _mr;

    protected void Awake()
    {
        if(sceneTargets == null)
        {
            sceneTargets = new Dictionary<CullTarget, bool>();
        }

        sceneTargets.Add(this, true);
    }

    protected void OnDestroy()
    {
        sceneTargets.Remove(this);
    }

    protected void Start()
    {
        _mr = gameObject.GetComponent<MeshRenderer>();
    }

    public void SetVisibility(bool value)
    {
        sceneTargets[this] = value;
    }

    private void LateUpdate()
    {
        _mr.enabled = sceneTargets[this];

        sceneTargets[this] = true;
    }
}