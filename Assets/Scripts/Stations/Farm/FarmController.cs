using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.UIElements;

public class FarmController : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject[] crops;
    public Crop currentCrop;
    private int cropToGrow;
    public FarmManager farmManager;

    public void Start()
    {
        farmManager = GameObject.Find("FarmManager").GetComponent<FarmManager>();
    }
    public void Interact(GameManager gameManager)
    {
        FarmController farmToGrowOn = this;
        farmManager.OpenFarmUI(farmToGrowOn);
        gameManager.SwitchStatePlayer(GameManager.PlayerState.ui);
    }

    

    public void PlantCrop(int _cropToGrow)
    {
        cropToGrow = _cropToGrow;
        GrowCrop();
    }
    private void GrowCrop()
    {
        currentCrop = crops[cropToGrow].GetComponent<Crop>();
        GameObject newCrop = Instantiate(currentCrop.cropData.GrowthStages[currentCrop.GrowthStage]);
        newCrop.transform.parent = this.transform;
        StartCoroutine(GrowCrop(crops[cropToGrow].GetComponent<Crop>()));
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
