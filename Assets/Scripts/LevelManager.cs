using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<LevelDataSO> levelDataSOs;

    [SerializeField] public LevelDataSO crrLevel;

    public int crrLevelIndex;

    public static LevelManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void ChoosingLevel(int indexOfLevel)
    {
        //crrLevelIndex = indexOfLevel;
        //crrLevel = levelDataSOs[indexOfLevel];

        crrLevelIndex = indexOfLevel;
        crrLevel = levelDataSOs[indexOfLevel];
    }
}
