using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool[] isFull;
    public GameObject[] slots;
    public GameObject inventory;
    private bool inventoryIsOn;

    private void Start()
    {
        inventoryIsOn = false;
    }

    public void SwitchInventory()
    {
        if (inventoryIsOn == false)
        {
            inventoryIsOn = true;
            inventory.SetActive(true);
        }
        else if (inventoryIsOn == true)
        {
            inventoryIsOn = false;
            inventory.SetActive(false);
        }
    }
}
