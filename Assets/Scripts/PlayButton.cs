using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayButton : MonoBehaviour {

	// Use this for initialization

    public void _playButton()
    {
        SceneManager.LoadScene("lv1");
    }
    public void _removeButton() {
        if (Drawer.instance != null)
        {
            Drawer.instance.RemoveModeEventButton();
            Debug.Log(Drawer.instance.mode.ToString());
        }
    }
    public void _drawButon() {
        if (Drawer.instance != null)
        {
            Drawer.instance.DrawModeEventButton();
            Debug.Log(Drawer.instance.mode.ToString());
        }
    }
}
