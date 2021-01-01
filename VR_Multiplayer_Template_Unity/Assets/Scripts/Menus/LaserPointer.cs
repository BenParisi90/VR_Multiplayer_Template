using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour
{
    
    public Transform shootTransform;
    [SerializeField]
    LineRenderer lineRenderer;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(shootTransform.position, shootTransform.forward, out hit))
        {
            Target target = hit.transform.GetComponent<Target>();
            lineRenderer.gameObject.SetActive(target != null);
            if(target != null)
            {
                Vector3[] positions = new Vector3[]{shootTransform.position, hit.point};
                lineRenderer.SetPositions(positions);
            }
            
        }
        else
        {
            lineRenderer.gameObject.SetActive(false);
        }
    }
}
