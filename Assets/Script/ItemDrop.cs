using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ItemDropper : MonoBehaviour
{
    public GameObject itemToDrop;
    public Transform dropPosition;

    public void DropItem()
    {
        if (itemToDrop != null && dropPosition != null)
        {
            Instantiate(itemToDrop, dropPosition.position, dropPosition.rotation);
        }
    }
}
