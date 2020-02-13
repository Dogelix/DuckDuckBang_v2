using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHealth : MonoBehaviour
{
    private SpriteRenderer[] _hearts;

    [SerializeField]
    private int _health = 3;

    [SerializeField]
    private float _heartsHeight = 0.4f;

    private float _spacing = 0.34f;

    private void Awake()
    {
        float currentSpacing = _spacing;

        //Setting up the current 
        for (int i = 0; i < _health; i++)
        {
            currentSpacing += _spacing;
        }

        currentSpacing = -currentSpacing;
        currentSpacing = currentSpacing / 2;

        _hearts = new SpriteRenderer[_health];

        Debug.Log(2 % 2);

        for(int i = 0; i < _health; i++)
        {
            var heart = Instantiate(GameAssets.i.InWorldSprite, transform);
            heart.name = "Heart_" + i.ToString();
            heart.transform.localScale = new Vector3(0.25f, 0.25f, 1);

            heart.transform.localPosition = new Vector3(currentSpacing, _heartsHeight, 0);
            currentSpacing += _spacing;

            heart.GetComponent<SpriteRenderer>().sprite = GameAssets.i.FullHeart;
            _hearts[i] = heart.GetComponent<SpriteRenderer>();
        }
    }


    private void FixedUpdate()
    {
        //Look at the player camera 
        this.gameObject.transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
    }


    public void TakeDamage()
    {
        if(_health == 1)
        {
            Destroy(this.transform.parent.gameObject);
            GameObject.FindGameObjectWithTag(StringUtils.SceneManager).GetComponent<GameModeController>().GameOver();
            return;
        }

        _hearts[_health - 1].sprite = GameAssets.i.EmptyHeart;

        _health--;
    }

}
