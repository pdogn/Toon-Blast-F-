using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NeighbourManager : MonoBehaviour
{
    [SerializeField] GridManager gridManager;
    public static NeighbourManager Instance { get; private set; }
    List<GameObject> neighbours = new List<GameObject>();

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

    public List<GameObject> FindSameNeighbour(CubeTypes cubeType, Vector2 gridIndex)
    {
        neighbours = new List<GameObject>();

        neighbours.Add(gridManager.allBlocks[(int)gridIndex.x].rows[(int)gridIndex.y]);

        RecursiveNeighbourSearch(cubeType, gridIndex);

        return neighbours;
    }

    public void RecursiveNeighbourSearch(CubeTypes cubeType, Vector2 gridIndex)
    {
        int x = (int)gridIndex.x;
        int y = (int)gridIndex.y;

        if (x + 1 < gridManager.allBlocks.Length && gridManager.allBlocks[x + 1].rows[y] != null)
        {
            Block curBlock = gridManager.allBlocks[x + 1].rows[y].GetComponent<Block>();
            if(curBlock is CubeBlock && 
                gridManager.allBlocks[x + 1].rows[y].GetComponent<CubeBlock>().cubeType == cubeType)
            {
                if(!neighbours.Contains(gridManager.allBlocks[x + 1].rows[y]))
                {
                    neighbours.Add(gridManager.allBlocks[x + 1].rows[y]);
                    RecursiveNeighbourSearch(cubeType, new Vector2(x + 1, y));
                }
            }
        }
        if (x - 1 >= 0 && gridManager.allBlocks[x - 1].rows[y] != null)
        {
            Block curBlock = gridManager.allBlocks[x - 1].rows[y].GetComponent<Block>();
            if (curBlock is CubeBlock &&
                gridManager.allBlocks[x - 1].rows[y].GetComponent<CubeBlock>().cubeType == cubeType)
            {
                if (!neighbours.Contains(gridManager.allBlocks[x - 1].rows[y]))
                {
                    neighbours.Add(gridManager.allBlocks[x - 1].rows[y]);
                    RecursiveNeighbourSearch(cubeType, new Vector2(x - 1, y));
                }
            }
        }
        if (y + 1 < gridManager.allBlocks[0].rows.Length && gridManager.allBlocks[x].rows[y+1] != null)
        {
            Block curBlock = gridManager.allBlocks[x].rows[y+1].GetComponent<Block>();
            if (curBlock is CubeBlock &&
                gridManager.allBlocks[x].rows[y + 1].GetComponent<CubeBlock>().cubeType == cubeType)
            {
                if (!neighbours.Contains(gridManager.allBlocks[x].rows[y + 1]))
                {
                    neighbours.Add(gridManager.allBlocks[x].rows[y + 1]);
                    RecursiveNeighbourSearch(cubeType, new Vector2(x, y + 1));
                }
            }
        }
        if (y - 1 >= 0 && gridManager.allBlocks[x].rows[y-1] != null)
        {
            Block curBlock = gridManager.allBlocks[x].rows[y - 1].GetComponent<Block>();
            if (curBlock is CubeBlock &&
                gridManager.allBlocks[x].rows[y - 1].GetComponent<CubeBlock>().cubeType == cubeType)
            {
                if (!neighbours.Contains(gridManager.allBlocks[x].rows[y - 1]))
                {
                    neighbours.Add(gridManager.allBlocks[x].rows[y - 1]);
                    RecursiveNeighbourSearch(cubeType, new Vector2(x, y - 1));
                }
            }
        }
    }

    public void DestroyBlockInAllBlocks(GameObject _neigh)
    {
        //for(int i = 0; i< gridManager.allBlocks.Length; i++)
        //{
        //    for(int j=0; j< gridManager.allBlocks[i].rows.Length; j++)
        //    {
        //        if (gridManager.allBlocks[i].rows[j] == _neigh)
        //        {
        //            gridManager.allBlocks[i].rows[j] = null;
        //        }
        //    }
        //}

        int x = (int)_neigh.GetComponent<Block>().gridIndex.x;
        int y = (int)_neigh.GetComponent<Block>().gridIndex.y;
        gridManager.allBlocks[x].rows[y] = null;
    }
}
