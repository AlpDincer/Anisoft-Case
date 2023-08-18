using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowFPS : MonoBehaviour
{
    public GameObject fpsText;
    public float deltaTime;

    void Start()
    {
        fpsText.SetActive(true);
    }
    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.GetComponent<TMP_Text>().text = "FPS: " + Mathf.Ceil(fps).ToString();
    }
}