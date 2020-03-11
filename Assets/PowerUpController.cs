using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using System.Linq;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class PowerUpController : MonoBehaviour
{
    public SteamVR_Action_Boolean useAction = null;
    public List<Sprite> PowerUpImages;
    public float BulletTimeDuration;
    public float bulletTimeScale;

    private PowerUps currentPowerUp = PowerUps.None;
    private SteamVR_Behaviour_Pose _pose;
    private Image imageHolder;

    private FilmGrain filmGrain = null;

    // Start is called before the first frame update
    void Start()
    {
        imageHolder = GetComponentInChildren<Image>();
        _pose = GetComponentInParent<SteamVR_Behaviour_Pose>();
        GameObject.Find("/Post Processing").GetComponent<Volume>().profile.TryGet(out filmGrain); // Get film grain effect
        EnablePowerUp(PowerUps.BulletTime);
    }

    // Update is called once per frame
    void Update()
    {       
        UsePowerUp();
    }

    private void UsePowerUp()
    {
        if (currentPowerUp != PowerUps.None && useAction.GetStateDown(_pose.inputSource)) // Use
        {
            GetType().GetMethod(currentPowerUp.ToString()).Invoke(this, null); // Calls method by its name (corresponds to Enum names). Avoids use of a switch statement           
        }
    }

    public void EnablePowerUp(PowerUps powerUp)
    {
        currentPowerUp = powerUp;

        if ( currentPowerUp != PowerUps.None)
        { // Display image
            imageHolder.enabled = true;
            imageHolder.sprite = PowerUpImages.FirstOrDefault(x => x.name == currentPowerUp.ToString());
        }
    }

    #region PowerUps Core Logic
    public void DestroyAll()
    {
        imageHolder.enabled = false;
        currentPowerUp = PowerUps.None;

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

    public void BulletTime()
    {
        imageHolder.enabled = false;
        currentPowerUp = PowerUps.None;

        // Enable post processing effect
        filmGrain.active = true;

        Time.timeScale = bulletTimeScale;

        StartCoroutine(StopBulletTime());
    }

    private IEnumerator StopBulletTime()
    {
        yield return new WaitForSeconds(BulletTimeDuration * Time.timeScale);
        Time.timeScale = 1.0f;
        filmGrain.active = false;
    }

    #endregion

    public enum PowerUps
    {
        DestroyAll,
        BulletTime,
        None
    }
}
