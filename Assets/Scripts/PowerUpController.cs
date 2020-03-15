using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using System.Linq;

public class PowerUpController : MonoBehaviour
{
    public SteamVR_Action_Boolean useAction = null;
    public List<Sprite> PowerUpImages;

    private PowerUps currentPowerUp = PowerUps.None;
    private SteamVR_Behaviour_Pose _pose;
    private bool isDisplayed;
    private Image imageHolder;


    // Start is called before the first frame update
    void Start()
    {
        imageHolder = GetComponentInChildren<Image>();
        _pose = GetComponentInParent<SteamVR_Behaviour_Pose>();
    }

    // Update is called once per frame
    void Update()
    {       
        DisplayPowerUp();
        UsePowerUp();
    }

    private void DisplayPowerUp()
    {
        if (!isDisplayed && currentPowerUp != PowerUps.None)
        {
            imageHolder.enabled = true;
            imageHolder.sprite = PowerUpImages.FirstOrDefault(x => x.name == currentPowerUp.ToString());           
            isDisplayed = true;
        }
    }

    private void UsePowerUp()
    {
        if (isDisplayed && currentPowerUp != PowerUps.None && useAction.GetStateDown(_pose.inputSource)) // Use
        {

            GetType().GetMethod(currentPowerUp.ToString()).Invoke(this, null); // Calls method by its name (corresponds to Enum names). Avoids use of a switch statement

            isDisplayed = false;
            currentPowerUp = PowerUps.None;
            imageHolder.enabled = false;
        }
    }

    public void EnablePowerUp(PowerUps powerUp)
    {
        currentPowerUp = powerUp;
    }

    #region PowerUps Core Logic
    public void DestroyAll()
    {
        int flyCount = Flock.agents.Count();

        for (int i = flyCount - 1; i >= 0; i--)
        {
            if (Flock.agents[i] != null)
            {
                Destroy(Flock.agents.ElementAt(i).gameObject);
            }
        }

        var walkingAgents = GameObject.FindGameObjectsWithTag("Enemy").Where(x => x.layer == LayerMask.NameToLayer("GroundEnemy")).ToList();
        foreach (var b in walkingAgents) // Destroys walking agents
        {
            Destroy(b); // Points script handles OnDestroy and does rest of the logic.
        }
    }
    #endregion

    public enum PowerUps
    {
        DestroyAll,
        None
    }

}
