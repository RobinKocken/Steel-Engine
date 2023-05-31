using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class SaveSystem : MonoBehaviour
{
    public GameManager gameManager;
    public bool doSave;
    public SavedData savedData;
    private string path;
   
    // Start is called before the first frame update

    public void Start()
    {
        path = Application.dataPath + "/DataXml.data";
        print(path);

        if (File.Exists(path))
        {
            savedData = Load();
        }
        
        for (int index = 0; index < gameManager.inventoryManager.slots.Count; index++)
        {
            if((int)gameManager.inventoryManager.itemHolders[savedData.slotItemType[index]].itemName == 0)
            {
                continue;
            }
            else
            {
                gameManager.inventoryManager.AddItem(gameManager.inventoryManager.itemHolders[savedData.slotItemType[index]], savedData.slotItemCount[index], index);
            }
        }

        gameManager.playerController.transform.position = savedData.playerPosition;
        gameManager.playerController.transform.eulerAngles = new Vector3(savedData.playerRotation.x, savedData.playerRotation.y, savedData.playerRotation.z);
        gameManager.baseController.transform.position = savedData.basePostion;
        gameManager.baseController.transform.eulerAngles = new Vector3(savedData.baseRotation.x, savedData.baseRotation.y, savedData.baseRotation.z);
    }
    private void Update()
    {
        if(doSave)
        {
            Save();
            doSave = false;
        }
    }

    public SavedData SetDataToSave()
    {
        savedData = new SavedData();

        //saving what is in the inventory
        for (int index = 0; index < gameManager.inventoryManager.slots.Count; index++)
        {
            savedData.slotItemType.Add((int)gameManager.inventoryManager.itemName);
            savedData.slotItemCount.Add(gameManager.inventoryManager.slots[index].GetComponent<Slot>().amount);
        }

        savedData.playerPosition = gameManager.playerController.transform.position;
        savedData.playerRotation = gameManager.playerController.transform.rotation.eulerAngles;
        savedData.basePostion = gameManager.baseController.transform.position;
        savedData.baseRotation = gameManager.baseController.transform.rotation.eulerAngles;


        return savedData;
    }

    public void Save()
    {
        savedData = SetDataToSave();
        var serializer = new XmlSerializer(typeof(SavedData));
        var stream = new FileStream(path, FileMode.Create);
        serializer.Serialize(stream, savedData);
        stream.Close();
    }

    public SavedData Load()
    {
        var serializer = new XmlSerializer(typeof(SavedData));
        var stream = new FileStream(path, FileMode.Open);
        var container = serializer.Deserialize(stream) as SavedData;
        stream.Close();
        return container;
    }
}

