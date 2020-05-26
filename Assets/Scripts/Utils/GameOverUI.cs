using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    public Canvas _canvas;
    public RectTransform _textMesh;
    float lerp = 0;
    public AudioSource GameOverSound;

    public void ShowGameOver()
    {
        GameOverSound.Play();
        _canvas.enabled = true;

        _textMesh.gameObject.GetComponent<TextMeshProUGUI>().text = GetComponentInParent<PlayerScore>().GetScore.ToString();
    }

    private void FixedUpdate()
    {
        if ( transform.localScale != Vector3.one && _canvas.enabled )
        {
            ScaleUp();
        }
    }

    private void ScaleUp()
    {
        lerp += Time.deltaTime / 4;
        transform.localScale = new Vector3(Mathf.Lerp(0, 1, lerp), Mathf.Lerp(0, 1, lerp), Mathf.Lerp(0, 1, lerp));
    }
}
