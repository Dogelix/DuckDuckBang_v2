
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

public class PointingController : MonoBehaviour
{
    public GameObject Barrel;
    public GameObject Parent;
    public AudioSource GunClick;
    public AudioSource GunShot;
    public AudioSource MainSong;

    public Light NotlLight;

    public SteamVR_Action_Boolean _fireAction = null;
    private SteamVR_Behaviour_Pose _pose;
    private bool disableShoot = false;

    // Start is called before the first frame update
    void Start()
    {
        _pose = GetComponentInParent<SteamVR_Behaviour_Pose>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!disableShoot)
        {
            RaycastHit hit = new RaycastHit();
            Ray shot = new Ray(Barrel.transform.position, Parent.transform.forward);
            bool contact = Physics.Raycast(shot, out hit);

            if (_fireAction.GetStateDown(_pose.inputSource) && contact)
            {
                if (hit.collider.tag == "NOTLD")
                {
                    disableShoot = true;
                    GunShot.Play();
                    StartCoroutine(FadeOutMusic(MainSong, 2));
                    SteamVR_Fade.View(Color.black, 2);
                    StartCoroutine(WaitForSceneLoad("NotLD"));
                }
                else if(hit.collider.tag == "MenuItem" )
                {
                    hit.transform.GetComponent<IMenuItem>().Activate();
                }
            }
            else if (_fireAction.GetStateDown(_pose.inputSource))
            {
                GunClick.Play();
            }
            else if (contact)
            {
                if (hit.collider.tag == "NOTLD")
                {
                    NotlLight.intensity = 1000;
                }
            }
            else
            {
                NotlLight.intensity = 250;
            }
        }      
    }

    private IEnumerator WaitForSceneLoad(string scene)
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(scene, LoadSceneMode.Single);

    }

    public static IEnumerator FadeOutMusic(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
            yield return null;
        }
        audioSource.Stop();
    }
}
