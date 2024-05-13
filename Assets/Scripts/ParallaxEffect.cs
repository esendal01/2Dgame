using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private float lenghth, startpos;
    public GameObject cam;
    public float parrallaxEfect;
    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x;
        lenghth = GetComponent<SpriteRenderer>().bounds.size.x;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = (cam.transform.position.x) * (1 - parrallaxEfect);
        float dist = (cam.transform.position.x * parrallaxEfect);
        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        if (temp > startpos + lenghth) startpos += lenghth;
        else if (temp < startpos - lenghth) startpos -= lenghth;
    }
}
