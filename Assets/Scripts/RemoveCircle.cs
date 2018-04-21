using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveCircle : MonoBehaviour
{
	void OnTriggerEnter(Collider c)
	{
		if (c.gameObject.layer == LayerMask.NameToLayer("Line"))
		{
            Drawer.instance.RemoveLine(c.gameObject);
			Destroy(c.gameObject);
		}
	}
}
