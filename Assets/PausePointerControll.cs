using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PausePointerControll : MonoBehaviour
{
    public GameObject Barrel;
    public GameObject Parent;
    public PauseController PauseController;
    public SteamVR_Action_Boolean Shoot = null;
    public SteamVR_Behaviour_Pose Pose;

    private GameObject CurrentHighlight = null;
    // Start is called before the first frame update
    void Start()
    {
        PauseController = FindObjectOfType<PauseController>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit = new RaycastHit();
        Ray shot = new Ray(Barrel.transform.position, Parent.transform.forward);
        bool contact = Physics.Raycast(shot, out hit);

        if (CurrentHighlight != null)
        {
            CurrentHighlight.GetComponent<UnityEngine.UI.Text>().color = Color.black;
            CurrentHighlight = null;
        }

        if (contact && hit.collider.gameObject.layer == LayerMask.NameToLayer("PauseCanvas"))
        {
            CurrentHighlight = hit.collider.gameObject;
            hit.collider.gameObject.GetComponent<UnityEngine.UI.Text>().color = Color.green;

            if (Shoot.GetStateDown(Pose.inputSource))
            {
                if (hit.collider.gameObject.tag == "Resume")
                {
                    PauseController.Resume();
                } 
                else if (hit.collider.gameObject.tag == "Quit")
                {
                    PauseController.Quit();
                }
            }
        }       
        
    }
}
