using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GManager : MonoBehaviour
{
    public static GManager get;
    public UIManager uIManager;
    void Awake()
    {
        get = this;
        DontDestroyOnLoad(this.gameObject);
    }

}
