using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using System.Linq;

public class SwapWeapons : MonoBehaviour
{
    public Vector2 m_deadzone = new Vector2(0.1f, 0.1f);
    public Vector2 m_NeutralPosition = new Vector2(0.0f, 0.0f);

    public SteamVR_Action_Boolean swapAction = null;
    public SteamVR_Action_Vector2 touch = null;
    public PauseController pauseController;

    private SteamVR_Behaviour_Pose _pose;
    private int currentImageIndex = 0;
    public Sprite[] WeaponImages;

    public GameObject[] weapons;
    private bool isDisplayed;
    private Image imageHolder;
    private int tempIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        imageHolder = GetComponentInChildren<Image>();
        _pose = GetComponentInParent<SteamVR_Behaviour_Pose>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!pauseController.Paused)
        {
            Vector2 axis = touch.GetAxis(_pose.inputSource);

            // Open Weapon Menu
            if (swapAction.GetStateDown(_pose.inputSource))
            {
                if (!isDisplayed && axis.x > -0.7f && axis.x < 0.7f)
                {
                    imageHolder.enabled = true;
                    isDisplayed = true;
                    tempIndex = currentImageIndex;
                }
                else if (isDisplayed)
                {
                    if (axis.x <= -0.7f) // Swipe Left
                    {
                        currentImageIndex = (currentImageIndex - 1 < 0) ? WeaponImages.Length - 1 : currentImageIndex - 1;
                        imageHolder.sprite = WeaponImages[currentImageIndex];
                    }
                    else if (axis.x >= 0.7f) // Swipe Right
                    {
                        currentImageIndex = (currentImageIndex + 1 > WeaponImages.Length - 1) ? 0 : currentImageIndex + 1;
                        imageHolder.sprite = WeaponImages[currentImageIndex];
                    }
                    else if (axis.x > -0.7f && axis.x < 0.7f)
                    {
                        imageHolder.enabled = false;
                        isDisplayed = false;
                        // Disable Previous weapon
                        weapons[tempIndex].SetActive(false);
                        // Enable current weapon
                        weapons[currentImageIndex].SetActive(true);
                    }
                }
            }
        }       
    }

}
