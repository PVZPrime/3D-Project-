using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDropChance : MonoBehaviour
{
    public List<Loot> lootList = new List<Loot>();
    public bool DropAllInList;


    Loot GetDroppedItem()
    {
        int randomNumber = Random.Range(1, 101);
        List<Loot> possibleItems = new List<Loot>();
        foreach (Loot item in lootList)
        {
            if (randomNumber <= item.DropChance)
            {
                    possibleItems.Add(item);        
            }
        }
        if (possibleItems.Count > 0)
        {
            Loot droppedItem = possibleItems[Random.Range(0, possibleItems.Count)];
            return droppedItem;
        }
        Debug.Log("No Loot Dropped");
        return null;
    }

    List<Loot> GetDroppedItemAll()
    {
        int randomNumber = Random.Range(1, 101);
        List<Loot> possibleItems = new List<Loot>();
        foreach (Loot item in lootList)
        {
            if (randomNumber <= item.DropChance)
            {
                possibleItems.Add(item);
            }
        }
        if (possibleItems.Count > 0)
        {
            return possibleItems;
        }
        Debug.Log("No Loot Dropped");
        return null;
    }

    public void InstantiateLoot(Vector3 spawnPosition)
    {
        if (!DropAllInList)
        {
            Loot droppedItem = GetDroppedItem();
            if (droppedItem != null)
            {
                GameObject lootGameObject = Instantiate(droppedItem.LootObject, spawnPosition, Quaternion.identity);
            }
        }
       
        if (DropAllInList)
        {
            List<Loot> droppedItems = GetDroppedItemAll();
            if (droppedItems != null && droppedItems.Count > 0)
            {
                foreach (Loot droppedItem in droppedItems)
                {
                    GameObject lootGameObject = Instantiate(droppedItem.LootObject, spawnPosition, Quaternion.identity);
                    Debug.Log("loot");
                }
            }
        }
    }
}
