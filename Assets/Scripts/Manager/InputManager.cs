using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    private bool canClick = false;

    private void Update()
    {
        if (UIManager.instance.cdTime > 0 && MovesPanel.Instance.Moves > 0)
        {
            GetEditorInputs();
        }
    }

    private void GetEditorInputs()
    {
        if (Input.GetMouseButtonUp(0))
        {
            DetectHittedObject(Input.mousePosition);
        }
    }

    void DetectHittedObject(Vector3 touchedPos)
    {
        BoxCollider2D hittedCollider = Physics2D.OverlapPoint(mainCam.ScreenToWorldPoint(touchedPos)) as BoxCollider2D;

        if (hittedCollider)
        {
            GameObject hittedObj = hittedCollider.gameObject;
            //hittedObj.gameObject.GetComponent<Block>().DoTappedActions();
            //Debug.Log(hittedObj.gameObject.GetComponent<Block>().gridIndex);

            Block curBlock = hittedObj.GetComponent<Block>();
            if (curBlock is CubeBlock)
            {
                curBlock.DoTappedActions();
            }
        }
    }
}
