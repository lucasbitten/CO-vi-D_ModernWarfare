using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject difficultySelectionScreen;
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject characterAvatar;
    [SerializeField] private TMP_Text text;

    private Button button;
    private void Awake()
    {
        mainMenu.SetActive(true);
        settings.SetActive(false);
        difficultySelectionScreen.SetActive(false);
    }

    public void SettingsButton()
    {
        mainMenu.SetActive(false);
        settings.SetActive(true);
    }

    public void StartButton()
    {
        mainMenu.SetActive(false);
        difficultySelectionScreen.SetActive(true);
        characterAvatar.SetActive(false);

    }

    public void DifficultySelected(int difficultyLevel)
    {
        GameManager gameManager = GameManager.Instance;

        gameManager.difficulty = (GameDifficulty) difficultyLevel;
        difficultySelectionScreen.SetActive(false);
        switch (difficultyLevel)
        {
            case 0:
                gameManager.currentDifficultyData = gameManager.easyData;
                break;
            case 1:
                gameManager.currentDifficultyData = gameManager.normalData;
                break;
            case 2:
                gameManager.currentDifficultyData = gameManager.hardData;
                break;
        }

        StartCoroutine(LoadAsynchronously(1));
    }

    public void BackButton()
    {
        mainMenu.SetActive(true);
        difficultySelectionScreen.SetActive(false);
        settings.SetActive(false);
        characterAvatar.SetActive(true);

    }

    public void OnDifficultyHover(int difficultyLevel)
    {
        text.gameObject.SetActive(true);
        switch (difficultyLevel)
        {
            case 0:
                text.text = "<align=center><u><size=200%>Easy</size></u></align>\n\nIn this difficulty indicators will show:\nInfected people without mask - Red\nPeople infected and with mask - White\nPeople without mask - Yellow ";
                break;
            case 1:
                text.text = "<align=center><u><size=200%>Medium</size></u></align>\n\nIn this difficulty indicators will show:\nPeople without mask - Yellow \n";
                break;
            case 2:
                text.text = "<align=center><u><size=200%>Hard</size></u></align>\n\nIn this difficulty there are no indicators ";
                break;
        }
    }


    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    private IEnumerator LoadAsynchronously(int sceneIndex)
    {
        var operation = SceneManager.LoadSceneAsync(sceneIndex);

        mainMenu.SetActive(false);
        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            slider.value = operation.progress / 0.9f;
            if (Math.Abs(slider.value - 0.9f) < 0.01f)
            {
                slider.value = 1f;
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}