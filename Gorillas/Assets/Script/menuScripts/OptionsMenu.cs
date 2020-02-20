using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public bool SoundOn;
    public bool MusicOn;
    public Text SoundCaption;
    public Text MusicCaption;
    public GameObject SoundButton;
    public GameObject MusicButton;
    public Color ButtonOn;
    public Color ButtonOff;
    

    void Start()
    {
        SoundCaption.GetComponent<Text>().text = "SOUND : ON";
        MusicCaption.GetComponent<Text>().text = "MUSIC : ON";
        SoundOn = true;
        MusicOn = true;
        SoundButton.GetComponent<Image>().color = ButtonOn;

    }

    public void SoundOnFunc()
    {
        if(SoundOn)
        {
            SoundCaption.GetComponent<Text>().text = "SOUND : OFF";
            SoundOn = false;
            SoundButton.GetComponent<Image>().color = ButtonOff;
            FindObjectOfType<audioManager>().playSFX = false;

        }
        else
        {
            SoundCaption.GetComponent<Text>().text = "SOUND : ON";
            SoundOn = true;
            SoundButton.GetComponent<Image>().color = ButtonOn;
            FindObjectOfType<audioManager>().playSFX = true;
        }
        FindObjectOfType<audioManager>().Play("Select");
    }

    public void MusicOnFunc()
    {
        if (MusicOn)
        {
            MusicCaption.GetComponent<Text>().text = "MUSIC : OFF";
            MusicOn = false;
            MusicButton.GetComponent<Image>().color = ButtonOff;
            FindObjectOfType<audioManager>().StopMusic();
            FindObjectOfType<gameController>().musicOn = false;
        }
        else
        {
            MusicCaption.GetComponent<Text>().text = "MUSIC : ON";
            MusicOn = true;
            MusicButton.GetComponent<Image>().color = ButtonOn;
            FindObjectOfType<audioManager>().Play("Music");
            FindObjectOfType<gameController>().musicOn = true;

        }
        FindObjectOfType<audioManager>().Play("Select");
    }




  
}
