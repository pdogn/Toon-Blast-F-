using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWater : MonoBehaviour
{
    [SerializeField] private GameObject basefluid;
    private float timeDelay;
    [SerializeField] private int maxFluidCount;
    [SerializeField] int fluidRate;
    [SerializeField] private List<GameObject> waterList;
    public int dd = 0;

    void Update()
    {
        if (transform.childCount < maxFluidCount)
        {
            timeDelay += Time.deltaTime;
            if (timeDelay > 1f / fluidRate)
            {
                GameObject x = Instantiate(basefluid, transform.position, Quaternion.identity, transform);
                waterList.Add(x);
                timeDelay = 0;
                dd++;
            }
        }
        else
        {
            foreach (GameObject child in waterList)
            {
                if (!child.activeInHierarchy)
                {
                    StartCoroutine(RePosWater(child));
                }
            }
        }
    }

    IEnumerator RePosWater(GameObject paticalWater)
    {
        yield return new WaitForSeconds(1.5f / fluidRate);

        paticalWater.transform.position = transform.position;
        paticalWater.SetActive(true);
    }
}
