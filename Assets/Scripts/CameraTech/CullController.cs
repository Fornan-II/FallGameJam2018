using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CullController : MonoBehaviour
{
    public bool doCulling = true;
    public float cullThickness = 1.0f;
    public LayerMask cullLayer;

    protected Camera _c;

    private void Start()
    {
        _c = GetComponent<Camera>();
    }

    private void Update()
    {
        if(!doCulling)
        {
            return;
        }

        // https://answers.unity.com/questions/174002/what-is-the-relationship-between-camera-size-units.html
        Vector3 boxDimensions = Vector3.one;
        boxDimensions.z = cullThickness;
        boxDimensions.y = _c.orthographicSize; //would usually mult this by 2, but overlab box takes half dimensions
        boxDimensions.x = boxDimensions.y * _c.aspect;
        Collider[] hitCol = Physics.OverlapBox(transform.position, boxDimensions, transform.rotation, cullLayer);
        foreach(Collider c in hitCol)
        {
            CullTarget ct = c.GetComponent<CullTarget>();
            if(ct)
            {
                ct.SetVisibility(false);
            }
        }
    }
}
