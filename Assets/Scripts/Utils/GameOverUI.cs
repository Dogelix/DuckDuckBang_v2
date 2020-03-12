using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    public Canvas _canvas;
    public RectTransform _textMesh;

    public void ShowGameOver()
    {
        _canvas.enabled = true;
        _textMesh.gameObject.GetComponent<TextMeshProUGUI>().text = GameObject.FindGameObjectWithTag(StringUtils.SceneManager).GetComponent<PlayerScore>().GetScore.ToString();
    }
}
