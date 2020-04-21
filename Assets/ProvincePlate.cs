using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProvincePlate : MonoBehaviour, IRaycastable
{
    [SerializeField] Canvas ProvinceMenu;

    public void DeSelect()
    {
        ProvinceMenu.GetComponent<Canvas>().enabled = false;
    }

    public IRaycastable Select()
    {
        print("yeet");
        ProvinceMenu.GetComponent<Canvas>().enabled = true;
        return this;
    }
}
