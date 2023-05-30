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
        for(int index = 0; index < gameManager.inventoryManager.slots.Count; index++)
        {
            savedData.slotItemCount.Add(gameManager.inventoryManager.slots[index].GetComponent<Slot>().amount);
        }
        //for (int index = 0; index < gameManager.inventoryManager.slots.Count; index++)
        //{
        //    savedData.slotItemCount.Add(gameManager.inventoryManager.hotbarSlots[index].GetComponent<Slot>().amount);
        //}

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

