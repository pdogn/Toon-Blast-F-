using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UILevel : MonoBehaviour
{
    public void ChooseLevel(int idLevel)
    {
        LevelManager.Instance.ChoosingLevel(idLevel);
        SceneManager.LoadScene("Game");
    }
}
