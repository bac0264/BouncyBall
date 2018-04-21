using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField] private Vector3 spinStrength = Vector3.zero;
    [SerializeField] private bool active = false;

    void Update()
    {
        if (active)
        {
            transform.Rotate(spinStrength);
        }
    }
}