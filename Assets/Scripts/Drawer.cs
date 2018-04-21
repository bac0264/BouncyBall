using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drawer : MonoBehaviour
{
    public static Drawer instance;
    public enum Mode
    {
        Draw, Remove
    };
    public Mode mode = Mode.Draw;
    public List<GameObject> lines = new List<GameObject>();
    private bool isHolding = false;
    private LineRenderer lineReview;
    private Line currentL;
    public Image removeImage;
    public Image addImage;
    public float distance = 0;
    public Transform lineContainer;
    public GameObject removeCirclePrefab;
    public GameObject linePrefab;
    public GameObject ingameDrawerCanvas;

    void Awake()
    {
        if(instance == null)
            instance = this;
    }

    void Start()
    {
        lineReview = GetComponent<LineRenderer>();
        lineReview.enabled = false;
        removeCirclePrefab.gameObject.SetActive(false);
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 positionRay = ray.origin;
        positionRay.z = 0;
        if (mode == Mode.Draw)
            DrawState(positionRay);
        else
            RemoveState(positionRay);
    }

    void DrawState(Vector3 positionRay)
    {
        removeCirclePrefab.gameObject.SetActive(false);
        if (GameController.instance._stillAlive())
        {
            if (Input.GetMouseButton(0))
            {
                if (isHolding == false)
                {
                    var obj = Instantiate(linePrefab);
                    obj.name = "Line " + lines.Count;
                    obj.transform.SetParent(lineContainer);
                    currentL = obj.GetComponent<Line>();
                    currentL.start = positionRay;
                    lineReview.positionCount = 2;
                    lineReview.SetPosition(0, positionRay);
                    isHolding = true;
                    Debug.Log(obj);
                    return;
                }
                else
                {
                    lineReview.SetPosition(1, positionRay);
                }

                lineReview.enabled = true;
            }
            else
            {
                if (isHolding == true)
                {
                    isHolding = false;
                    float dist = Vector3.Distance(currentL.start, positionRay);
                    Vector3 pos = Vector3.Lerp(currentL.start, positionRay, currentL.limitOfDistance / dist);
                    currentL.end = pos;
                    if (currentL.start != currentL.end)
                    {
                        currentL.SetLine();
                        lines.Add(currentL.gameObject);
                        GameController.instance._destroyLifeImage(isHolding);
                    }
                    else
                    {
                        Destroy(currentL.gameObject);
                    }
                    currentL = null;
                  //  isHolding = false;
                    lineReview.enabled = false;
                    lineReview.positionCount = 0;
                }
            }
        }
        else return;
    }

    void RemoveState(Vector3 positionRay)
    {
        removeCirclePrefab.gameObject.SetActive(true);
        removeCirclePrefab.transform.position = positionRay;
        isHolding = false;
        if (currentL != null)
            Destroy(currentL.gameObject);
        lineReview.positionCount = 0;
        lineReview.enabled = false;
    }
    public void RemoveLine(GameObject line)
    {
        lines.Remove(line);
    }

    public void DrawModeEventButton()
    {
        mode = Mode.Draw;
    }

    public void RemoveModeEventButton()
    {
        mode = Mode.Remove;
        Debug.Log(mode.ToString());
    }
    public void _ReloadLine() {
        for (int i = 0; i < lines.Count; i++) {
            Destroy(lines[i]);
        }
        lines.Clear();
    }
    //private void OnDrawGizmos()
    //{
    //    if (isLimited != Vector3.zero)
    //    {
    //        Gizmos.color = Color.cyan;
    //        Gizmos.DrawSphere(isLimited, 0.5f);
    //    }

    //    Gizmos.color = Color.red;

    //    if (currentL && currentL.start != Vector3.zero)
    //        Gizmos.DrawSphere(currentL.start, 0.5F);

    //    Gizmos.color = Color.green;
    //    if (currentL && currentL.end != Vector3.zero)
    //        Gizmos.DrawSphere(currentL.end, 0.5F);
    //}
}

