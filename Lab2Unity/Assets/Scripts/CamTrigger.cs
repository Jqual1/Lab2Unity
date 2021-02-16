using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTrigger : MonoBehaviour
{
    public GameObject loc;
    public GameObject Camera;
    private bool isLerp;

    // Start is called before the first frame update
    void Start()
    {
        isLerp = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        if (isLerp) {
            Vector3 pos = new Vector3(loc.transform.position.x,loc.transform.position.y,-10);

            Camera.transform.position = Vector3.Lerp(Camera.transform.position, pos, 2);
        }
    }
    private void OnTriggerEnter2D()
    {
        isLerp = true;
    }
}
