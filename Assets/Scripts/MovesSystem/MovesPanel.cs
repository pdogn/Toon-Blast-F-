using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MovesPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text movesText;
    private int moves;
    public int Moves
    {
        get { return moves; }
        set
        {
            moves = value;
            if (moves <= 0)
            {
                moves = 0;
                //StartCoroutine(MovesFinishedDelay());
            }
            movesText.text = moves.ToString();
        }
    }
    public static MovesPanel Instance { get; private set; }
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

    private void Start()
    {
        GetMovesData();
    }

    public void GetMovesData()
    {
        Moves = LevelManager.Instance.crrLevel.Moves;
    }
}
