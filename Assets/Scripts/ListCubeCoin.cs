using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListCubeCoin : MonoBehaviour {
    public static ListCubeCoin instance;
    // Use this for initialization
    public List<GameObject> cubeCoins = new List<GameObject>();
    private void Awake()
    {
        if (instance == null)
        instance = this;
    }

    // Update is called once per frame
}
