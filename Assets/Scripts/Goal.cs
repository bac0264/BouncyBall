using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {
    public static Goal instance;
    public GameObject goal;
    public Color color = Color.white;
    private void Awake()
    {
        instance = this;
        SpriteRenderer sr = goal.GetComponent<SpriteRenderer>();
        sr.color = new Color(color.r, color.g, color.b, sr.color.a);
    }
    // Update is called once per frame
    void Update () {
		
	}
}
