using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Unity.VisualScripting;
using System.Linq;

public class SaveSystem : MonoBehaviour
{
    public bool doSave;
    public GameManager gameManager;

    public SavedData savedData;
    public DataSlot curSaveData;
    private string path;
    

    private void Start()
    {
        path = Application.dataPath + "/DataXml.data";
    }

    private void Update()
    {
        if (doSave)
        {
            Save(0);
            doSave = false;
        }
    }
    public void SaveButton(int _saveIndex)
    {
        Save(_saveIndex);
    }

    public void LoadButton(int _loadIndex)
    {
        if (File.Exists(path))
        {
            curSaveData = Load();

            LoadData();
        }
    }
    public void Save(int _saveSlot)
    {
        curSaveData = SetDataToSave(_saveSlot);
        var serializer = new XmlSerializer(typeof(DataSlot));
        var stream = new FileStream(path, FileMode.Create);
        serializer.Serialize(stream, curSaveData);
        stream.Close();
    }
    public DataSlot SetDataToSave(int _saveInSlot)
    {
        var DataSlot = savedData.DataSlots[_saveInSlot];
        savedData.DataSlots[_saveInSlot] = new DataSlot();

        //saving what is in the inventory
        for (int sIndex = 0; sIndex < gameManager.inventoryManager.slots.Count; sIndex++)
        {
            DataSlot.slotItemType.Add((int)gameManager.inventoryManager.itemName);
            DataSlot.slotItemCount.Add(gameManager.inventoryManager.slots[sIndex].GetComponent<Slot>().amount);
        }

        DataSlot.playerPosition = gameManager.playerController.transform.position;
        DataSlot.playerRotation = gameManager.playerController.gameObject.transform.eulerAngles;
        DataSlot.basePostion = gameManager.baseController.gameObject.transform.position;
        DataSlot.baseRotation = gameManager.baseController.transform.rotation.eulerAngles;


        Transform baseParent = gameManager.buildManager.buildingParent;
        for (int bIndex = 0; bIndex < baseParent.childCount; bIndex++)
        {
            DataSlot.buildingIndexes.Add(baseParent.GetChild(bIndex).GetComponent<CheckPlacement>().buildingID);
            DataSlot.buildingPosistions.Add(baseParent.GetChild(bIndex).transform.position);
            DataSlot.buildingRotations.Add(baseParent.GetChild(bIndex).transform.eulerAngles);
        }


        return savedData.DataSlots[_saveInSlot];
    }
    public DataSlot Load()
    {
        var serializer = new XmlSerializer(typeof(DataSlot));
        var stream = new FileStream(path, FileMode.Open);
        var container = serializer.Deserialize(stream) as DataSlot;
        stream.Close();
        return container;
    }

    public void LoadData()
    {
        Debug.Log("Refilling Inventory");
        for (int index = 0; index < gameManager.inventoryManager.slots.Count; index++)
        {
            if ((int)gameManager.inventoryManager.itemHolders[curSaveData.slotItemType[index]].itemName == 0)
            {
                continue;
            }
            else
            {
                gameManager.inventoryManager.AddItem(gameManager.inventoryManager.itemHolders[curSaveData.slotItemType[index]], curSaveData.slotItemCount[index], index);
            }
        }

        Debug.Log("Adjusting Player Transform");
        gameManager.playerController.transform.position = new Vector3(curSaveData.playerPosition.x, curSaveData.playerPosition.y, curSaveData.playerPosition.z);
        gameManager.playerController.gameObject.transform.eulerAngles = new Vector3(curSaveData.playerRotation.x, curSaveData.playerRotation.y, curSaveData.playerRotation.z);

        Debug.Log("Adjusting Base Transform");
        gameManager.baseController.gameObject.transform.position = new Vector3(curSaveData.basePostion.x, curSaveData.basePostion.y, curSaveData.basePostion.z);
        gameManager.baseController.transform.eulerAngles = new Vector3(curSaveData.baseRotation.x, curSaveData.baseRotation.y, curSaveData.baseRotation.z);

        Transform baseParent = gameManager.buildManager.buildingParent;
        for (int bIndex = 0; bIndex < curSaveData.buildingIndexes.Count; bIndex++)
        {
            Debug.Log("Rebuilding Base");
            GameObject newBuilding = Instantiate(baseParent.GetComponent<BuildManager>().objects[curSaveData.buildingIndexes[bIndex]], baseParent);
            newBuilding.transform.position = curSaveData.buildingPosistions[bIndex];
            newBuilding.transform.eulerAngles = new Vector3(curSaveData.buildingRotations[bIndex].x, curSaveData.buildingRotations[bIndex].y, curSaveData.buildingRotations[bIndex].z);
        }

    }
}

