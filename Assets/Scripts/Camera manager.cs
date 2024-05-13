using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Cameramanager : MonoBehaviour
{
    // Start is called before the first frame update
    // Start is called before the first frame update
    public Transform takipEdilenNesne; // Kameran�n takip etti�i nesne
    public Vector2 minSinir; // Kameran�n minimum s�n�r�
    public Vector2 maxSinir; // Kameran�n maksimum s�n�r�

    void LateUpdate()
    {
        if (takipEdilenNesne != null)
        {
            float x = Mathf.Clamp(takipEdilenNesne.position.x, minSinir.x, maxSinir.x);
            float y = Mathf.Clamp(takipEdilenNesne.position.y, minSinir.y, maxSinir.y);

            transform.position = new Vector3(x, y, transform.position.z);
        }
    }
}
