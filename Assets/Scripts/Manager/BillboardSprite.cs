using UnityEngine;

public class BillboardSprite : MonoBehaviour
{
    void Update()
    { 
        transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
    }
}
