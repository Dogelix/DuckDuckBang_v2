using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponInventory : MonoBehaviour
{
    public Transform _weaponSpawnLocation;

    public InventorySlots _inventorySlots;

    public Weapon _defaultPistol;

    [System.Serializable]
    public class InventorySlots
    {
        public Weapon Slot1 { get; set; }
        public Weapon Slot2 { get; set; }
    }

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(InputManager._i.GetKey(KeybindingActions.Shoot)){
            Debug.Log("Success");
        }
    }
}
