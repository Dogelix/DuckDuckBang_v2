//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
////using Valve.VR;
//using System.Linq;

//public class SwapWeapons : MonoBehaviour
//{
//    public Vector2 m_deadzone = new Vector2(0.1f, 0.1f);
//    public Vector2 m_NeutralPosition = new Vector2(0.0f, 0.0f);

//    public SteamVR_Action_Boolean swapAction = null;
//    public SteamVR_Action_Vector2 swipe = null;

//    //public SteamVR_Action_Boolean swipeLeft = null;
//    //public SteamVR_Action_Boolean swipeRight = null;

//    private SteamVR_Behaviour_Pose _pose;
//    private int currentImageIndex = 0;
//    public Sprite[] WeaponImages;

//    public GameObject[] weapons;
//    private bool isDisplayed;
//    private Image imageHolder;
//    private int tempIndex = 0;

//    SteamVR_TrackedObject trackedObj;
//    // Start is called before the first frame update
//    void Start()
//    {
//        imageHolder = GetComponentInChildren<Image>();
//        _pose = GetComponentInParent<SteamVR_Behaviour_Pose>();
//        trackedObj = GetComponent<SteamVR_TrackedObject>();
//    }

   

//    // Update is called once per frame
//    void Update()
//    {
//        // Open Weapon Menu
//        if (swapAction.GetStateDown(_pose.inputSource))
//        {
//            if (!isDisplayed)
//            {
//                imageHolder.enabled = true;
//                isDisplayed = true;
//            }
//            else
//            {
//                imageHolder.enabled = false;
//                isDisplayed = false;
//                // Disable Previous weapon
//                weapons[tempIndex].SetActive(false);
//                // Enable current weapon
//                weapons[currentImageIndex].SetActive(true);
//            }
//        }


//        // Swipe Left
//        Vector2 delta = swipe[SteamVR_Input_Sources.RightHand].delta;
//        if (delta.x >= (m_NeutralPosition.x + m_deadzone.x) && isDisplayed)
//        {
//            tempIndex = currentImageIndex;
//            currentImageIndex = (currentImageIndex - 1 < 0) ? WeaponImages.Length - 1 : currentImageIndex - 1;
//            imageHolder.sprite = WeaponImages[currentImageIndex];
//        }

//    }
//}
