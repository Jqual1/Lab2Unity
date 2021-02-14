using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityGuard : MonoBehaviour
{

    public List<GameObject> locs;

    private Queue<GameObject> qlocs;

    public float duration = 3;

    SpriteRenderer sr;

    public Sprite right;
    public Sprite left;
    public Sprite up;
    public Sprite down;


    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        qlocs = new Queue<GameObject>();
        foreach (GameObject go in locs) {
            qlocs.Enqueue(go);
        }
        NextUp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void NextUp()
    {
        GameObject pong = qlocs.Dequeue();

        StartCoroutine(LerpPosition(pong.transform.position));

        qlocs.Enqueue(pong);

        if (pong.transform.position.x > transform.position.x)
        {
            sr.sprite = right;
        }
        else if (pong.transform.position.x < transform.position.x)
        {
            sr.sprite = left;
        }
        else if (pong.transform.position.y > transform.position.y)
        {
            sr.sprite = up;
        }
        else if (pong.transform.position.y < transform.position.y)
        {
            sr.sprite = down;
        }

    }

    IEnumerator LerpPosition(Vector3 targetPosition)
    {
        float time = 0;
        Vector3 startPosition = transform.position;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
        NextUp();
    }
}
