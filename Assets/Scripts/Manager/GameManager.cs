using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject person;

    public bool isWin;
    bool ended;
    
    // Start is called before the first frame update
    void Start()
    {
        ended = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (UIManager.instance.cdTime > 0)
        {
            UIManager.instance.cdTime -= 1 * Time.deltaTime;
            UIManager.instance.TimeCountDown(UIManager.instance.cdTime);
        }
        else
        {
            CheckGameState();
        }
    }

    void CheckGameState()
    {
        if (ended == false)
        {
            ended = true;
            Collider2D col = Physics2D.OverlapBox(person.transform.position + new Vector3(0, 0.5f, 0), new Vector2(0.5f, 0.4f), 0);

            if (col.gameObject.tag == "Fluid")
            {
                isWin = false;
                UIManager.instance.OpenFailMenu();
            }
            else
            {
                isWin = true;
                UIManager.instance.OpenWinMenu();
            }
        }
    }

    
}