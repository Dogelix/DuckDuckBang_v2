using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillMe : MonoBehaviour
{
    [SerializeField]
    private float _killMeIn = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, _killMeIn);
    }
}