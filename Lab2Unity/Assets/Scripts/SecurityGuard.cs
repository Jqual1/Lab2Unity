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

    public GameObject SecurityGuardChild;
    public GameObject child;
   


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
            SecurityGuardChild.transform.localScale = new Vector3(-1, 1, 1);
            child.transform.rotation = Quaternion.Euler(child.transform.rotation.x,child.transform.rotation.y,90);
        }
        else if (pong.transform.position.x < transform.position.x)
        {
            sr.sprite = left;
            SecurityGuardChild.transform.localScale = new Vector3(1, 1, 1);
            child.transform.rotation = Quaternion.Euler(child.transform.rotation.x, child.transform.rotation.y, 270);


        }
        else if (pong.transform.position.y > transform.position.y)
        {
            sr.sprite = up;
            SecurityGuardChild.transform.localScale = new Vector3(-1, 1, 1);
            child.transform.rotation = Quaternion.Euler(child.transform.rotation.x, child.transform.rotation.y, 180);


        }
        else if (pong.transform.position.y < transform.position.y)
        {
            sr.sprite = down;
            SecurityGuardChild.transform.localScale = new Vector3(1, 1, 1);
            child.transform.rotation = Quaternion.Euler(child.transform.rotation.x, child.transform.rotation.y, 0);


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

    private void OnCollisionEnter2D()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        StopAllCoroutines();
        StartCoroutine(ColorLerp(new Color(1, 1, 1, 0), 2));
    }

    IEnumerator ColorLerp(Color endValue, float duration)
    {
        float time = 0;
        Color startValue = sr.color;

        while (time < duration)
        {
            sr.color = Color.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        sr.color = endValue;
        gameObject.SetActive(false);
    }
}
