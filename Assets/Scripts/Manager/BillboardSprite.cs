using UnityEngine;

public class BillboardSprite : MonoBehaviour
{
    void Update()
    { 
        transform.rotation = Quaternion.Euler(Camera.main.transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, 0);
    }
}
