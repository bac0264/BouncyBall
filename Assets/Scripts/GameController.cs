using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    public static GameController instance;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    public List<GameObject> lifeImage = new List<GameObject>();
    public int lifeCount = 3;
    // public GameObject UIPrefabs;
    public GameObject gameController;
    //public GameObject preLifeImage;
    public GameObject image;
    public GameObject LifePanel;
    //public Vector3 defaultofpos = new Vector3(140, -83, 0);
    private void Awake()
    {
        _createLifeImage(lifeCount);
        _makeInstance();
    }
    private void _makeInstance()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameController);
        }
        else Destroy(gameController);
    }
    public void _setScoreText(int score)
    {
        scoreText.text = score.ToString();
    }
    public void _destroyLifeImage(bool isholding)
    {
        if (lifeImage.Count > 0 && isholding == false)
        {
            Destroy(lifeImage[lifeImage.Count - 1]);
            lifeImage.RemoveAt(lifeImage.Count - 1);
        }
    }
    public void _createLifeImage(int n)
    {
        for (int i = 0; i < n; i++)
        {
            lifeImage.Add(Instantiate(image, LifePanel.transform));
        }
    }
    public void _clearLifeImage()
    {
        Debug.Log(lifeImage.Count); 
        for (int i = 0; i < lifeImage.Count; i++)
        {
            Destroy(lifeImage[i]);
        }
        lifeImage.Clear();
    }
    public bool _stillAlive()
    {
        if (lifeImage.Count < 1)
            return false;
        return true;
    }
    public void _ReloadLife()
    {
        _clearLifeImage();
        _createLifeImage(lifeCount);
    }
}
