using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class FarmController : MonoBehaviour, IInteractable
{
    [SerializeField] private Crop[] crops;
    public Crop currentCrop;
    private int cropToGrow;
    public FarmManager farmManager;

    public void Start()
    {
        farmManager = GameObject.Find("FarmManager").GetComponent<FarmManager>();
    }
    public void Interact(GameManager gameManager)
    {
        farmManager.farmUI.SetActive(true);
        FarmController farmToGrowOn = this;
        farmManager.OpenFarmUI(farmToGrowOn);
        gameManager.SwitchStatePlayer(GameManager.PlayerState.menu);
    }

    

    public void PlantCrop(int _cropToGrow)
    {
        cropToGrow = _cropToGrow;
        GrowCrop();
    }
    private void GrowCrop()
    {
        currentCrop = crops[cropToGrow];
        StartCoroutine(GrowCrop(crops[cropToGrow]));
    }

    public IEnumerator GrowCrop(Crop _cropToGrow)
    {
        if (_cropToGrow.GrowthStage == _cropToGrow.cropData.GrowthStages.Length)
        {
            StopCoroutine(GrowCrop(null));
        }
        yield return new WaitForSeconds(_cropToGrow.cropData.timeToGrow / 4);

        _cropToGrow.GrowthStage++;
        _cropToGrow.GetComponent<Crop>().Grow();

    }

    public void HarvestCrops()
    {
        //harvest the crop and put it in the inventory
    }

}
