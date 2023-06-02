using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Unity.VisualScripting;

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

        LoadData();
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
        for (int sIndex = 0; sIndex < gameManager.inventoryManager.slots.Count; sIndex++)
        {
            savedData.slotItemType.Add((int)gameManager.inventoryManager.itemName);
            savedData.slotItemCount.Add(gameManager.inventoryManager.slots[sIndex].GetComponent<Slot>().amount);
        }

        savedData.playerPosition = gameManager.playerController.transform.position;
        savedData.playerRotation = gameManager.playerController.transform.rotation.eulerAngles;
        savedData.basePostion = gameManager.baseController.transform.position;
        savedData.baseRotation = gameManager.baseController.transform.rotation.eulerAngles;


        Transform baseParent = gameManager.buildManager.buildingParent;
        for (int bIndex = 0; bIndex < baseParent.childCount; bIndex++)
        {
            savedData.buildingIndexes.Add(bIndex);
            savedData.buildingPosistions.Add(baseParent.GetChild(bIndex).transform.position);
            savedData.buildingRotations.Add(baseParent.GetChild(bIndex).transform.eulerAngles);
        }


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

    public void LoadData()
    {
        for (int index = 0; index < gameManager.inventoryManager.slots.Count; index++)
        {
            if ((int)gameManager.inventoryManager.itemHolders[savedData.slotItemType[index]].itemName == 0)
            {
                continue;
            }
            else
            {
                gameManager.inventoryManager.AddItem(gameManager.inventoryManager.itemHolders[savedData.slotItemType[index]], savedData.slotItemCount[index], index);
            }
        }

        gameManager.playerController.transform.position = new Vector3(savedData.playerPosition.x, savedData.playerPosition.y, savedData.playerPosition.z);
        gameManager.playerController.transform.eulerAngles = new Vector3(savedData.playerRotation.x, savedData.playerRotation.y, savedData.playerRotation.z);
        gameManager.baseController.transform.position = new Vector3(savedData.basePostion.x, savedData.basePostion.y, savedData.basePostion.z);
        gameManager.baseController.transform.eulerAngles = new Vector3(savedData.baseRotation.x, savedData.baseRotation.y, savedData.baseRotation.z);
    }
}

