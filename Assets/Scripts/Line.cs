using UnityEngine;

public class Line : MonoBehaviour
{
    public Vector3 start = Vector3.zero;
    public Vector3 end = Vector3.zero;
	private LineRenderer lineRenderer;
    private BoxCollider boxCollider;
    public float limitOfDistance = 8;

    void Awake ()
    {
        lineRenderer = GetComponent<LineRenderer>();
        boxCollider = GetComponent<BoxCollider>();
    }
    public void _limitofDistance(ref float distance) {
        if ( distance > limitOfDistance) distance = limitOfDistance;
        Debug.Log(distance);
    }
    public void SetLine()
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPositions(new Vector3[]{start, end});
        Vector3 center = Vector3.Lerp(start, end, 0.5F);
        transform.position = center;
        float distance = Vector3.Distance(start, end);
        Debug.Log(distance);
        boxCollider.size = new Vector3(boxCollider.size.x, boxCollider.size.y, distance);
        //direction start to end
        transform.LookAt(end);
    }

    void OnDrawGizmos()
    {
        if(lineRenderer.positionCount > 0)
        {
            Gizmos.color = Color.green;
        }
    }
}