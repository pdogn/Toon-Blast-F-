using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class RocketBlock : Block
{
    public override BlockTypes blockType => BlockTypes.Rocket;

    public override Vector3 spriteSize => new Vector3(0.5f, 0.5f, 0.5f);

    private SpriteRenderer rocketLeftSR;
    private SpriteRenderer rocketRightSR;

    public RocketTypes rocketTypes;
    public bool canTapped = true;

    public override void DoTappedActions()
    {
        throw new System.NotImplementedException();
    }

    public override void MoveToTarget(float arriveTime)
    {
        DOTween.Kill(transform);
        transform.DOKill();
        transform.DOMove(target.position, arriveTime).SetEase(Ease.OutBounce).OnComplete(() =>
        {

        });
    }

    public override void SetupBlock()
    {
        rocketLeftSR = transform.GetChild(0).GetComponent<SpriteRenderer>();
        rocketLeftSR.transform.localScale = spriteSize;
        rocketLeftSR.sortingOrder = -(int)gridIndex.y + 1;

        GameObject newSpriteObj = Instantiate(rocketLeftSR.gameObject, rocketLeftSR.transform.position, 
                                                Quaternion.identity, transform);

        rocketRightSR = transform.GetChild(1).GetComponent<SpriteRenderer>();

        rocketTypes = (RocketTypes)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(RocketTypes)).Length);

        rocketLeftSR.sprite = ImageLibrary.rocketLeftSprite;
        rocketRightSR.sprite = ImageLibrary.rocketRightSprite;

        if (rocketTypes == RocketTypes.Vertical)
        {
            rocketRightSR.transform.localEulerAngles = new Vector3(0f, 0f, 90f);
            rocketLeftSR.transform.localEulerAngles = new Vector3(0f, 0, 90f);

            rocketLeftSR.transform.localPosition = new Vector3(0f, -0.12f, 0f);
            rocketRightSR.transform.localPosition = new Vector3(0f, 0.02f, 0f);
        }
        else
        {
            rocketLeftSR.transform.localPosition = new Vector3(-0.07f, 0f, 0f);
            rocketRightSR.transform.localPosition = new Vector3(0.07f, 0f, 0f);
        }
    }
}
