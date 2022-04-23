using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] GameObject SettingUI;
    [SerializeField] private Slider musicSlider;


    // Start is called before the first frame update
    void Start()
    {
        SettingUI.gameObject.SetActive(false);
        SettingUI.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width - 100f, Screen.height - 60);
        musicSlider.value = FindObjectOfType<GameSetting>().GetComponent<AudioSource>().volume;
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
