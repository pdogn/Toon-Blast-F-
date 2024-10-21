using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnQuadType : MonoBehaviour
{
    [SerializeField] GameObject[] tQuad;
    public Material material;
    private void Start()
    {
        GetTypeQuad();
    }

    void GetTypeQuad()
    {
        QuadType tquad = LevelManager.Instance.crrLevel.Quad;

        if(tquad == QuadType.water)
        {
            Instantiate(tQuad[0], this.transform);
        }
        //if (tquad == QuadType.poison)
        //{
        //    Instantiate(tQuad[1], this.transform);
        //}

        if (tquad == QuadType.poison)
        {
            Instantiate(tQuad[1], this.transform);
        }
    }
}
