using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class UIHome : MonoBehaviour
{
    [Header("HOME PANEL")]
    public RectTransform btn_Start;
    
    [Space(12)]
    [Header("LEVEL PANEL")]
    public RectTransform RollLevel;
    public GameObject[] dotBar;
    public Sprite[] dotImg;
    public List<Transform> levelbtn;

    bool allowPress;
    int crrPageIndx;

    public RectTransform back_btn, next_btn;
    
    // Start is called before the first frame update
    void Start()
    {
        AnimStartBtn();
        SetupLevelbtn();
        allowPress = true;
        crrPageIndx = 1;
        back_btn.gameObject.SetActive(false);
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
            next_btn.gameObject.SetActive(crrPageIndx == dotBar.Length ? false : true);
            back_btn.gameObject.SetActive(true);
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
            back_btn.gameObject.SetActive(crrPageIndx == 1 ? false : true);
            next_btn.gameObject.SetActive(true);
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

    void AnimStartBtn()
    {
        btn_Start.DOScale(new Vector3(1.5f, 1.5f, 1f), 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }
}
