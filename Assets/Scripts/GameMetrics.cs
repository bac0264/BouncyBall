using UnityEngine;

public class GameMetrics : MonoBehaviour
{
    public static GameMetrics get;

    [Range(0, 3)] public float gameTimeScale = 1F;

    private void Awake()
    {
        if (get == null)
            get = this;
    }

    void Update ()
    {
        Time.timeScale = gameTimeScale;
    }
}