using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelText;
    [Space]
    [SerializeField] TextMeshProUGUI TimeCountDownText;
    public float cdTime;

    [Space]
    public GameObject failMenu;
    public GameObject winMenu;

    [Space]
    public GameObject exitBtn;

    public static UIManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        LevelTxt();
        cdTime = 30f;
    }

    void LevelTxt()
    {
        levelText.text = "LV: " + LevelManager.Instance.crrLevelIndex + " " + LevelManager.Instance.crrLevel.width + "x" + LevelManager.Instance.crrLevel.height;
    }

    public void TimeCountDown(float _time)
    {
        TimeCountDownText.text = _time.ToString("0");
    }

    public void Exits()
    {
        SceneManager.LoadScene("menu");
    }

    public void OpenWinMenu()
    {
        winMenu.SetActive(true);
        AnimMenuEnd(winMenu.transform);
    }

    public void CloseWinMenu()
    {
        winMenu.SetActive(false);
    }

    public void OpenFailMenu()
    {
        failMenu.SetActive(true);
        AnimMenuEnd(failMenu.transform);
    }

    public void CloseFailMenu()
    {
        failMenu.SetActive(false);
    }

    void AnimMenuEnd(Transform menuEnd)
    {
        menuEnd.GetChild(0).DOScale(new Vector2(70, 70), .7f);
        menuEnd.GetChild(0).GetComponent<Image>().DOColor(Color.black, 1f);
        menuEnd.GetChild(0).GetComponent<Image>().DOFade(0.75f, 1f).SetEase(Ease.Linear);
        menuEnd.GetChild(1).GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 515), 1f).SetEase(Ease.OutBounce);
        menuEnd.GetChild(2).GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, -440), 1f).SetEase(Ease.OutBounce);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    int clickCount = 0;
    Tween moveExitBtn;
    public void Setting()
    {
        clickCount++;
        if (clickCount == 1)
        {
            exitBtn.SetActive(true);
            moveExitBtn = exitBtn.GetComponent<RectTransform>().DOAnchorPos(new Vector2(80, 50), .4f).SetAutoKill(false);
        }
        else
        {
            clickCount = 0;
            moveExitBtn.PlayBackwards();
            exitBtn.SetActive(false);
        }
    }
}
