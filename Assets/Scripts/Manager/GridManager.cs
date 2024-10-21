using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class GameObject2DArray
{
    public GameObject[] rows = new GameObject[1];
}

public class GridManager : MonoBehaviour
{
    //[SerializeField] public LevelDataSO dataLevel;
    [SerializeField] private Transform spawnedBlocksParent;
    [SerializeField] private Transform spawnedPosObjsParent;

    [SerializeField] private GameObject posObjPrefab;
    [SerializeField] private GameObject blockObjPrefab;


    [HideInInspector] public BlockTypes[,] blockTypes = new BlockTypes[1, 1];
    [HideInInspector] public CubeTypes[,] cubeTypes = new CubeTypes[1, 1];

    public GameObject2DArray[] allBlocks;
    [HideInInspector] public GameObject2DArray[] allPosObjs;

    [SerializeField] private Transform Floor;

    private void Start()
    {
        SpawnStartBlocks();
    }

    public void SpawnStartBlocks()
    {
        //Xóa hết phần tử con trong 2 obj
        //int tmpChildCount = spawnedPosObjsParent.childCount;
        //for (int i = 0; i < tmpChildCount; i++)
        //{
        //    DestroyImmediate(spawnedPosObjsParent.GetChild(0).gameObject);
        //}
        //tmpChildCount = spawnedBlocksParent.childCount;
        //for (int i = 0; i < tmpChildCount; i++)
        //{
        //    DestroyImmediate(spawnedBlocksParent.GetChild(0).gameObject);
        //}

        int tmpChildCount = spawnedPosObjsParent.childCount;
        for (int i = 0; i < tmpChildCount; i++)
        {
            DestroyImmediate(spawnedPosObjsParent.GetChild(0).gameObject);
        }
        tmpChildCount = spawnedBlocksParent.childCount;
        for (int i = 0; i < tmpChildCount; i++)
        {
            DestroyImmediate(spawnedBlocksParent.GetChild(0).gameObject);
        }

        //Tính toán vị trí spawn block đầu tiên
        float blockSize = 0.4f;
        float gapSize = 0.1f;
        int gridSizeX = LevelManager.Instance.crrLevel.width;
        int gridSizeY = LevelManager.Instance.crrLevel.height;
        Vector2 startPos = new Vector2(-(((gridSizeX * blockSize) + ((gridSizeX - 1) * gapSize)) / 2f) + blockSize / 2f,
            (((gridSizeY * blockSize) + ((gridSizeY - 1) * gapSize)) / 2f) - blockSize / 2f);

        allBlocks = new GameObject2DArray[gridSizeX];
        allPosObjs = new GameObject2DArray[gridSizeX];
        for (int i = 0; i < gridSizeX; i++)
        {
            allBlocks[i] = new GameObject2DArray();
            allBlocks[i].rows = new GameObject[gridSizeY];

            allPosObjs[i] = new GameObject2DArray();
            allPosObjs[i].rows = new GameObject[gridSizeY];

            for (int j = 0; j < gridSizeY; j++)
            {
                Vector3 spawnPos = transform.position + new Vector3(startPos.x + (i * blockSize) + (i * gapSize),
                    startPos.y - ((j * blockSize) + (j * gapSize)), 0);
                GameObject spawnedPosObj = Instantiate(posObjPrefab, spawnPos, Quaternion.identity,
                    spawnedPosObjsParent);
                GameObject spawnedBlockObj = Instantiate(blockObjPrefab, spawnPos, Quaternion.identity,
                    spawnedBlocksParent);
                //Thêm loại block vào block mới sinh
                //AddBlockTypeToBlockObj(spawnedBlockObj, i, j, spawnedPosObj.transform);
                //GetCubeType(spawnedBlockObj,i, j, spawnedPosObj.transform);
                
                allBlocks[i].rows[j] = spawnedBlockObj;
                allPosObjs[i].rows[j] = spawnedPosObj;
                
                GetCubeType(spawnedBlockObj,i, j, spawnedPosObj.transform);
            }
        }

        Vector3 newPos = new Vector3(this.transform.position.x, (float)(gridSizeY * 0.25f - 4.5f), 0);
        this.transform.position = newPos;

        Floor.position = new Vector3(newPos.x, 0.25f + newPos.y + gridSizeY * 0.25f, 0);
    }

    //public void AddBlockTypeToBlockObj(GameObject blockObj, int xIndex, int yIndex, Transform spawnedPosTransform)
    //{
    //    //BlockTypes curBlockType = blockTypes[xIndex, yIndex];
    //    //CubeTypes curCubeType = cubeTypes[xIndex, yIndex];
    //    CubeTypes curCubeType = (CubeTypes)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(CubeTypes)).Length);
    //    CubeBlock currentBlock = blockObj.AddComponent<CubeBlock>();
    //    blockObj.GetComponent<CubeBlock>().cubeType = curCubeType;

    //    currentBlock.gridIndex = new Vector2(xIndex, yIndex);
    //    currentBlock.target = spawnedPosTransform;
    //    currentBlock.SetupBlock();
    //}

    public void GetCubeType(GameObject blockObj ,int xIndex, int yIndex, Transform spawnedPosTransform)
    {
        CubeBlock currentBlock = blockObj.AddComponent<CubeBlock>();
        int y = LevelManager.Instance.crrLevel.width;//số cột của grid
        CubeTypes crrCubetype = LevelManager.Instance.crrLevel.cubeTypes[yIndex * y + xIndex];
        blockObj.gameObject.GetComponent<CubeBlock>().cubeType = crrCubetype;
        
        currentBlock.gridIndex = new Vector2(xIndex, yIndex);
        currentBlock.target = spawnedPosTransform;
        currentBlock.SetupBlock();

        //if (LevelManager.Instance.crrLevel.patternFlatter[yIndex * y + xIndex] == false)
        //{
        //    currentBlock.gameObject.SetActive(false);
        //    allBlocks[xIndex].rows[yIndex] = null;
        //}

        if (LevelManager.Instance.crrLevel.patternFlatter[yIndex * y + xIndex] == false)
        {
            currentBlock.gameObject.SetActive(false);
            allBlocks[xIndex].rows[yIndex] = null;
        }
    }
}
