using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using System.Linq;

public class SwapWeapons : MonoBehaviour
{
    public SteamVR_Action_Boolean swapAction = null;
    public SteamVR_Action_Boolean swipeLeft = null;
    public SteamVR_Action_Boolean swipeRight = null;

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
        // Open Weapon Menu
        if (swapAction.GetStateDown(_pose.inputSource))
        {
            if (!isDisplayed)
            {
                imageHolder.enabled = true;
                isDisplayed = true;
            }
            else
            {
                imageHolder.enabled = false;
                isDisplayed = false;
                // Disable Previous weapon
                weapons[tempIndex].SetActive(false);
                // Enable current weapon
                weapons[currentImageIndex].SetActive(true);
            }
        }


        // Swipe Left
        if (swipeLeft.GetStateDown(_pose.inputSource) && isDisplayed)
        {
            tempIndex = currentImageIndex;
            currentImageIndex = (currentImageIndex - 1 < 0) ? WeaponImages.Length - 1 : currentImageIndex - 1;
            imageHolder.sprite = WeaponImages[currentImageIndex];           
        }

        // Swipe Right
        if (swipeRight.GetStateDown(_pose.inputSource) && isDisplayed)
        {
            tempIndex = currentImageIndex;
            currentImageIndex = (currentImageIndex + 1 > WeaponImages.Length - 1) ? 0 : currentImageIndex + 1;
            imageHolder.sprite = WeaponImages[currentImageIndex];
        }
    }
}
