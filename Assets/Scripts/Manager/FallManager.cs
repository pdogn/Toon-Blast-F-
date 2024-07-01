using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FallManager : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;
    [SerializeField] private Transform spawnedBlocksParent;
    [SerializeField] private GameObject blockPrefab;

    private Vector3 startSpawnPos;
    public static FallManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void Fall()
    {
        for(int i = 0; i < gridManager.allBlocks.Length; i++)
        {
            startSpawnPos = gridManager.allPosObjs[i].rows[0].transform.position + new Vector3(0, 1, 0);
            ChangePos(i);
        }
    }

    public void ChangePos(int x)
    {
        GameObject[] newColumObjs = new GameObject[gridManager.allBlocks[0].rows.Length];

        int noneNullIndex = gridManager.allBlocks[0].rows.Length - 1;

        // Duyệt qua mảng Box từ cuối về đầu để sắp xếp
        for(int y = gridManager.allBlocks[x].rows.Length - 1; y >= 0; y--)
        {
            if (gridManager.allBlocks[x].rows[y] != null)
            {
                newColumObjs[noneNullIndex] = gridManager.allBlocks[x].rows[y];
                noneNullIndex--;
            }
        }
        gridManager.allBlocks[x].rows = newColumObjs;

        //cập nhật lại target của block
        int nullCount = 0;
        for(int i = gridManager.allBlocks[x].rows.Length -1; i >=0 ; i--)
        {
            if (gridManager.allBlocks[x].rows[i] != null)
            {
                gridManager.allBlocks[x].rows[i].GetComponent<Block>().target = gridManager.allPosObjs[x].rows[i].transform;

                gridManager.allBlocks[x].rows[i].GetComponent<Block>().gridIndex = new Vector2(x, i);
                //gridManager.allBlocks[x].rows[i].GetComponent<Block>().MoveToTarget(1f);//
            }
            else
            {
                nullCount++;
                Vector3 spawnPos = startSpawnPos + new Vector3(0, nullCount * 1f, 0);
                GameObject spawnedBlockObj = Instantiate(blockPrefab, spawnPos, Quaternion.identity, spawnedBlocksParent);

                CubeTypes curCubeType = (CubeTypes)UnityEngine.Random.Range(2, System.Enum.GetValues(typeof(CubeTypes)).Length);
                CubeBlock crrBlock = spawnedBlockObj.AddComponent<CubeBlock>();
                spawnedBlockObj.GetComponent<CubeBlock>().cubeType = curCubeType;

                crrBlock.gridIndex = new Vector2(x, i);
                crrBlock.target = gridManager.allPosObjs[x].rows[i].transform;
                //crrBlock.SetupBlock();
                gridManager.allBlocks[x].rows[i] = spawnedBlockObj;
            }

            gridManager.allBlocks[x].rows[i].GetComponent<Block>().MoveToTarget(.8f);
            //gridManager.allBlocks[x].rows[i].GetComponent<Block>().SetupBlock();
            Block blockItem = gridManager.allBlocks[x].rows[i].GetComponent<Block>();
            if (blockItem is CubeBlock)
            {
                blockItem.SetupBlock();
            }
        }
    }

    public void SpawnRocket(Transform spawnPos, Vector2 _gridIndex, Transform _target)
    {
        GameObject spawnedRocketObj = Instantiate(blockPrefab, spawnPos.position, Quaternion.identity, spawnedBlocksParent);
        RocketBlock rkBlock = spawnedRocketObj.AddComponent<RocketBlock>();
        rkBlock.gridIndex = new Vector2((int)_gridIndex.x, (int)_gridIndex.y);
        rkBlock.target = _target;

        rkBlock.SetupBlock();

        gridManager.allBlocks[(int)_gridIndex.x].rows[(int)_gridIndex.y] = spawnedRocketObj;
    }
    
}
