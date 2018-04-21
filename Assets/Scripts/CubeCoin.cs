using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCoin : MonoBehaviour 
{
	public float speed = 20;
    void Update()
	{
		float value = speed * Time.deltaTime;
		transform.Rotate(-value, value, -value);
	}

}
