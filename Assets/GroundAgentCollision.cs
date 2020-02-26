using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundAgentCollision : MonoBehaviour
{
    public void RaycastDestroy()
    {
        Destroy(gameObject);
    }
}
