using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCameraRoration : MonoBehaviour
{
    public GameObject target;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = target.transform.position + new Vector3(0f, 3f, 5.28f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(target.transform.position, Vector3.up, 20 * Time.deltaTime);
    }
}
