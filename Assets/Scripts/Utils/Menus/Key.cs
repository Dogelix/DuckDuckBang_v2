using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MenuParentClass
{
    public char Character;
    public override void Activate()
    {
        if(Character == '0' )
        {
            GetComponentInParent<PlayerNameManager>().RemoveCharacter();
        }
        else if ( Character == '1' )
        {
            GetComponentInParent<PlayerNameManager>().SaveName();
        }
        else
        {
            GetComponentInParent<PlayerNameManager>().AddCharacter(Character);
        }
    }
}
