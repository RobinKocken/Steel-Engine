using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    public Item item;
    public int amount;

    public int slotID;

    public Image iconRenderer;
    public TMP_Text currentAmountText;
}
