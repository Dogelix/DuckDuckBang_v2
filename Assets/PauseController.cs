using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

public class PauseController : MonoBehaviour
{
    public SteamVR_Action_Boolean PauseButton = null;
    public SteamVR_Behaviour_Pose Pose;
    public bool Paused;
    public GameObject Pointer;
    public GameObject Canvas;

    private int coolingMask;
    private GameObject currentWeapon;
    private AudioSource mainMusic;

    private void Start()
    {
        mainMusic = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!Paused)
        {
            if (PauseButton.GetStateDown(Pose.inputSource))
            {
                Canvas.SetActive(true);
                currentWeapon = GameObject.FindGameObjectWithTag("Weapon");
                currentWeapon.SetActive(false);
                Pointer.SetActive(true);
                Time.timeScale = 0;
                Paused = true;
                mainMusic.Pause();
            }
        }
        
    }

    public void Resume()
    {
        Canvas.SetActive(false);
        currentWeapon.SetActive(true);
        Pointer.SetActive(false);
        Time.timeScale = 1;
        Paused = false;
        mainMusic.Play();
    }

    public void Quit()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
