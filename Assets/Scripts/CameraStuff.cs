using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStuff : MonoBehaviour
{
    void Awake()
    {
		Camera.main.aspect = 1.5f;
    }
}
