using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{
    public GameObject iconPrefab;
    public RawImage compassImage;
    public Transform player;
    public float maxDistance;

    List<QuestMarker> questMarkers = new List<QuestMarker>();
    
    float compassUnit;

    public QuestMarker one;
    public QuestMarker two;
    public QuestMarker three;

    void Start()
    {
        compassUnit = compassImage.rectTransform.rect.width / 360;

        //AddQuestMarker(one);
        //AddQuestMarker(two);
        //AddQuestMarker(three);
    }

    // Update is called once per frame
    void Update()
    {
        compassImage.uvRect = new Rect(player.localEulerAngles.y / 360, 0, 1, 1);    

        foreach(QuestMarker marker in questMarkers)
        {
            marker.image.rectTransform.anchoredPosition = GetPosOnCompass(marker);

            float dst = Vector2.Distance(new Vector2(player.transform.position.x, player.transform.position.z), marker.Position);
            float scale = 0;

            if(dst < maxDistance)
                scale = 1 - (dst / maxDistance);

            marker.image.rectTransform.localScale = Vector3.one * scale;
        }
    }

    public void AddQuestMarker(QuestMarker marker)
    {
        GameObject newMarker = Instantiate(iconPrefab, compassImage.transform);
        marker.image = newMarker.GetComponent<Image>();
        marker.image.sprite = marker.icon;

        questMarkers.Add(marker);
    }

    Vector2 GetPosOnCompass(QuestMarker marker)
    {
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.z);
        Vector2 playerFwd = new Vector2(player.transform.forward.x, player.transform.forward.z);

        float angle = Vector2.SignedAngle(marker.Position - playerPos, playerFwd);

        return new Vector2(compassUnit * angle, 0);
    }
}
