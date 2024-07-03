using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBlock : Block
{
    public override BlockTypes blockType => BlockTypes.Cube;

    public override Vector3 spriteSize => new Vector3(0.5f, 0.5f, 0.5f);

    private SpriteRenderer mySpriteRenderer;
    public CubeTypes cubeType;

    public bool canTapped = true;

    public override void DoTappedActions()
    {
        if (canTapped)
        {
            List<GameObject> mySameNeighbours = NeighbourManager.Instance.FindSameNeighbour(cubeType, gridIndex);
            if(mySameNeighbours.Count > 0)
            {
                canTapped = false;
                //bool isRocketSpawned = false;
                //int sameNeighboursCount = mySameNeighbours.Count;
                MovesPanel.Instance.Moves = MovesPanel.Instance.Moves - 1;
                bool canSpawnRocket = false; /*sameNeighboursCount >= 5;*/
                foreach (GameObject neigh in mySameNeighbours)
                {
                    CubeBlock curBlock = neigh.gameObject.GetComponent<CubeBlock>();
                    curBlock.canTapped = false;
                    curBlock.target = null;
                    DOTween.Kill(neigh);
                    neigh.transform.DOKill();
                    if (canSpawnRocket)
                    {
                        //float arriverTime = 0.4f;
                        //Vector3 targetPos = transform.position;
                        //neigh.transform.DOMove(targetPos, arriverTime).SetEase(Ease.InOutBack).OnComplete(() =>
                        //{
                        //    Destroy(neigh);
                        //    sameNeighboursCount--;
                        //    //Debug.Log("DDDD " + sameNeighboursCount);
                        //    NeighbourManager.Instance.DestroyBlockInAllBlocks(neigh);
                        //    if (!isRocketSpawned)
                        //    {
                        //        isRocketSpawned = true;
                        //        //FallManager.Instance.SpawnRocket(transform, gridIndex, target);
                        //    }
                        //    if (sameNeighboursCount == 0)
                        //    {
                        //        FallManager.Instance.SpawnRocket(transform, gridIndex, target);
                        //        FallManager.Instance.Fall();
                        //    }
                        //}
                        //);
                    }
                    else
                    {
                        Destroy(neigh);
                        NeighbourManager.Instance.DestroyBlockInAllBlocks(neigh);
                    }
                }
                //if (!canSpawnRocket)
                //{
                //    FallManager.Instance.Fall();
                //}
            }
        }
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
        mySpriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        mySpriteRenderer.transform.localScale = spriteSize;
        mySpriteRenderer.sortingOrder = -(int)gridIndex.y + 1;
        mySpriteRenderer.sprite = GetMySprite();
    }

    private Sprite GetMySprite()
    {
        switch (cubeType)
        {
            case CubeTypes.Red:
                {
                    return ImageLibrary.redCubeBlockSprite;
                }
            case CubeTypes.Blue:
                {
                    return ImageLibrary.blueCubeBlockSprite;
                }
            case CubeTypes.Purple:
                {
                    return ImageLibrary.purpleCubeBlockSprite;
                }
            case CubeTypes.Yellow:
                {
                    return ImageLibrary.yellowCubeBlockSprite;
                }
            case CubeTypes.Green:
                {
                    return ImageLibrary.greenCubeBlockSprite;
                }
            default: break;
        }
        return ImageLibrary.blueCubeBlockSprite;
    }
}
