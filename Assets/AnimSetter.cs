using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSetter : MonoBehaviour
{
    public EWalkType _walkType = EWalkType.Walking;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().SetTrigger(_walkType.ToString());
    }

}
