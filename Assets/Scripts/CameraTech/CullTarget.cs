using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CullTarget : MonoBehaviour
{
    public static Dictionary<CullTarget, bool> sceneTargets;

    public Renderer[] visualComponents;
    protected bool _isActive;

    protected void Awake()
    {
        if(sceneTargets == null)
        {
            sceneTargets = new Dictionary<CullTarget, bool>();
        }

        sceneTargets.Add(this, true);
        _isActive = true;
    }

    protected void OnDestroy()
    {
        sceneTargets.Remove(this);
    }

    public void SetVisibility(bool value)
    {
        sceneTargets[this] = value;
    }

    protected void LateUpdate()
    {
        if (_isActive != sceneTargets[this])
        {
            foreach (Renderer r in visualComponents)
            {
                r.enabled = sceneTargets[this];
            }
            _isActive = sceneTargets[this];
        }

        sceneTargets[this] = true;
    }

#if UNITY_EDITOR
    [ContextMenu("Get Renderers")]
    protected void GetRenderers()
    {
        visualComponents = transform.parent.GetComponentsInChildren<Renderer>(true);
    }
#endif
}