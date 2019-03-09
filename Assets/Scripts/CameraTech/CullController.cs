using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CullController : MonoBehaviour
{
    public Transform focusObject;
    public bool doCulling = true;
    [Range(0.0f, 1.0f)]
    public float cullDistance = 0.5f;
    public LayerMask cullLayer;

    protected Camera _c;

    private void Start()
    {
        _c = GetComponent<Camera>();
    }

    private void Update()
    {
        /*Plane[] planes = GeometryUtility.CalculateFrustumPlanes(_c);
        foreach (CullTarget ct in CullTarget.sceneTargets)
        {
            //https://answers.unity.com/questions/8003/how-can-i-know-if-a-gameobject-is-seen-by-a-partic.html
            ct.SetVisibility(GeometryUtility.TestPlanesAABB(planes, ct.collider.bounds));
        }*/

        // https://answers.unity.com/questions/174002/what-is-the-relationship-between-camera-size-units.html
        Vector3 boxDimensions = Vector3.one;
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
