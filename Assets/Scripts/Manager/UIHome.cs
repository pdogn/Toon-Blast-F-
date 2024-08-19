using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class UIHome : MonoBehaviour
{
    public RectTransform RollLevel;
    public GameObject[] dotBar;
    public Sprite[] dotImg;
    public List<Transform> levelbtn;

    public bool allowPress;
    public int crrPageIndx;
    
    // Start is called before the first frame update
    void Start()
    {
        SetupLevelbtn();
        allowPress = true;
        crrPageIndx = 1;
        dotBar[0].GetComponent<RectTransform>().DOScale(new Vector3(1.5f, 1.5f, 1), .2f);
    }

    public void SetupLevelbtn()
    {
        for (int i = 0; i < levelbtn.Count; i++)
        {
            levelbtn[i].GetChild(0).GetComponent<TMP_Text>().text = (i+1).ToString();
        }
    }
    
    public void NextPageLevel()
    {
        if (RollLevel.anchoredPosition.x > -1990 && allowPress == true)
        {
            allowPress = false;
            crrPageIndx++;
            float targetPosX = RollLevel.anchoredPosition.x - 500;
            Vector2 targetPos = new Vector2((float)targetPosX, 5);
            RollLevel.DOAnchorPos(targetPos, .5f).OnComplete(() =>
            {
                DOTween.Kill(RollLevel);
                allowPress = true;
            });
            ChangeDotBar();
        }
    }

    public void BackPageLevel()
    {
        if (RollLevel.anchoredPosition.x < -10 && allowPress == true)
        {
            allowPress = false;
            crrPageIndx--;
            float targetPosX = RollLevel.anchoredPosition.x + 500;
            Vector2 targetPos = new Vector2((float)targetPosX, 5);
            RollLevel.DOAnchorPos(targetPos, .5f).OnComplete(() =>
            {
                DOTween.Kill(RollLevel);
                allowPress = true;
            });
            ChangeDotBar();
        }
    }

    public void ChangeDotBar()
    {
        for(int i=0; i<dotBar.Length; i++)
        {
            dotBar[i].GetComponent<Image>().sprite = dotImg[0];
            dotBar[i].GetComponent<RectTransform>().DOScale(new Vector3(1f, 1f, 1), .2f);
            if (i + 1 == crrPageIndx)
            {
                dotBar[i].GetComponent<Image>().sprite = dotImg[1];
                dotBar[i].GetComponent<RectTransform>().DOScale(new Vector3(1.5f, 1.5f, 1), .2f);
            }
        }
    }
}
