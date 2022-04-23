using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] GameObject SettingUI;
    // Start is called before the first frame update
    void Start()
    {
        SettingUI.gameObject.SetActive(false);
        SettingUI.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width - 100f, Screen.height - 60);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame(){
        SceneManager.LoadScene(3);
    }

    public void HighScore(){

    }

    public void Setting(){
        SettingUI.gameObject.SetActive(true);
    }

    public void BackToMenu(){
        SettingUI.gameObject.SetActive(false);
    }
}
