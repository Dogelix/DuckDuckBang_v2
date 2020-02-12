using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHealth : MonoBehaviour
{
    private SpriteRenderer[] _hearts = new SpriteRenderer[3];

    private int _health = 3;

    private void Awake()
    {
        var gottenComps = gameObject.GetComponentsInChildren<SpriteRenderer>();

        for (int i = 1; i < 4; i++)
        {
            foreach (var heart in gottenComps)
            {
                if (heart.name.Contains(i.ToString()))
                {
                    _hearts[i - 1] = heart;
                    break;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        //Look at the player camera 
        this.gameObject.transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
    }


    public void TakeDamge()
    {
        if(_health == 1)
        {
            Destroy(this.transform.parent.gameObject);
            GameAssets.i.SceneManager.GetComponent<GameModeController>().GameOver();
            return;
        }

        _hearts[_health - 1].sprite = GameAssets.i.EmptyHeart;

        _health--;
    }

}
