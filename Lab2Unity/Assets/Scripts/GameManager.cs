using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    public GameObject dialogBox;
    public GameObject dialogText;
    public GameObject startButton;
    public GameObject backgroundImage;

    public GameObject canvas;
    public GameObject events;

    private Coroutine dialogCo;

    private int stampsCollected;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
	{
        if(Instance == null)
		{
            Instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(canvas);
            DontDestroyOnLoad(events);
            
		}
		else
		{
            Destroy(gameObject);
		}
	}

    public void StartDialog(string text)
	{
        dialogBox.SetActive(true);
        dialogCo = StartCoroutine(TypeText(text));
	}

    public void HideDialog()
	{
        dialogBox.SetActive(false);
        if (dialogCo != null)
        {
            StopCoroutine(dialogCo);
        }
	}

    IEnumerator TypeText(string text)
	{
        dialogText.GetComponent<TextMeshProUGUI>().text = "";
        foreach(char c in text.ToCharArray())
		{
            dialogText.GetComponent<TextMeshProUGUI>().text += c;
            yield return new WaitForSeconds(0.01f);
		}
	}

    public void StartButon()
	{
        stampsCollected = 0;
        startButton.SetActive(false);
        StartCoroutine(LoadYourAsyncScene("SpearSnailWorld"));

    }

    public void GameOver()
	{
        startButton.SetActive(true);
        StopAllCoroutines();
        HideDialog();
        StartCoroutine(ColorLerp(new Color(1, 1, 1, 1), 2));
    }

    IEnumerator ColorLerp(Color endValue, float duration)
	{
        float time = 0;
        Image sprite = backgroundImage.GetComponent<Image>();
        Color startValue = sprite.color;

        while(time < duration)
		{
            sprite.color = Color.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
		}
        sprite.color = endValue;
	}
    IEnumerator LoadYourAsyncScene(string scene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        StartCoroutine(ColorLerp(new Color(1, 1, 1, 0), 2));

    }
    public bool IsGameStarted()
    {
        return !startButton.activeSelf;
    }
    public void UpdateStamps()
    {
        stampsCollected++;
        if (stampsCollected == 2)
        {
            GameOver();
        }

    }
}
