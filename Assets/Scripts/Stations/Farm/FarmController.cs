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
    public GameObject currentCrop;
    private int cropToGrow;
    public FarmManager farmManager;
    //will only update for each growth stage
    public int cropProgress;

    private int GrowthStage;
    public bool fullyGrown;
    private GameObject oldCrop;

    public void Start()
    {
        farmManager = GameObject.Find("FarmManager").GetComponent<FarmManager>();
    }
    //if the player interacts with a farm, tell the farm manager which farm the player wants to interact with
    public void Interact(GameManager gameManager)
    {
        FarmController farmToGrowOn = this;
        farmManager.OpenFarmUI(farmToGrowOn);
        gameManager.SwitchStatePlayer(GameManager.PlayerState.ui);
    }


    //tell the farm wich crop to plant and place it in the world
    public void PlantCrop(int _cropToGrow)
    {
        cropToGrow = _cropToGrow;
        farmManager.cropListUI.SetActive(false);
        farmManager.progressUI.SetActive(true);

        currentCrop = Instantiate(crops[cropToGrow], this.transform);
        currentCrop.transform.position = this.transform.position;
        StartCoroutine(GrowCrop(currentCrop.GetComponent<Crop>()));
        oldCrop = currentCrop;
    }
    //first check if the crop is fully grown now, if so stop growing process, else wait for X seconds(X being the growth time divided by the total ammount of growth stages) 
    public IEnumerator GrowCrop(Crop _cropToGrow)
    {
        bool run = true;
        while(run)
        {
            if (GrowthStage == _cropToGrow.cropData.GrowthStages.Length - 1)
            {
                run = false;
                fullyGrown = true;
                //StopCoroutine(GrowCrop(_cropToGrow));
            }
            else
            {
                yield return new WaitForSeconds(_cropToGrow.cropData.timeToGrow / _cropToGrow.cropData.GrowthStages.Length);
                cropProgress = (GrowthStage / _cropToGrow.cropData.GrowthStages.Length) * 100;

                GrowthStage++;
                if (GrowthStage != _cropToGrow.cropData.GrowthStages.Length)
                {
                    Grow();
                }
            }
        }
    }
    //remove the old growth stage and place the new
    public void Grow()
    {
        if (oldCrop != null)
        {
            Destroy(oldCrop);
        }
        oldCrop = Instantiate(currentCrop.GetComponent<Crop>().cropData.GrowthStages[GrowthStage], this.transform);
        currentCrop = oldCrop;
    }
    //harvest the crop and put it in the inventory
    public void HarvestCrops()
    {
        
    }

}
