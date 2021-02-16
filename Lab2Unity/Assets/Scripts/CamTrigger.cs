using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTrigger : MonoBehaviour
{
    public GameObject loc;
    public GameObject loc2;
    public GameObject Camera;
    public float duration = 3;
    private bool isLerp;
    private Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        pos = new Vector3(loc.transform.position.x, loc.transform.position.y, -10);

        isLerp = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        if (isLerp) {

           Camera.transform.position = Vector3.Lerp(Camera.transform.position, pos, 20);
            //Camera.transform.position = pos;
        }
    }
    private void OnTriggerEnter2D()
    {
        //isLerp = true;
        GameManager.Instance.PauseGame();
        StartCoroutine(LerpPosition(pos));

    }
    IEnumerator LerpPosition(Vector3 targetPosition)
    {
        float time = 0;
        Vector3 startPosition = Camera.transform.position;

        while (time < duration)
        {
            Camera.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        Camera.transform.position = targetPosition;
        pos = new Vector3(loc2.transform.position.x,loc2.transform.position.y,-10);
        StartCoroutine(LerpPosition2(pos));
    }
    IEnumerator LerpPosition2(Vector3 targetPosition)
    {
        float time = 0;
        Vector3 startPosition = Camera.transform.position;


        while (time < duration)
        {
            Camera.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        Camera.transform.position = targetPosition;
        GameManager.Instance.PauseGame();
        gameObject.SetActive(false);
    }
}
