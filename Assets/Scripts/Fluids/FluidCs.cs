using UnityEngine;

public class FluidCs : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -7f)
        {
            this.gameObject.SetActive(false);
        }
    }
}
