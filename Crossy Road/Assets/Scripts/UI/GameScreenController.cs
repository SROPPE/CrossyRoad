﻿using CrossyRoad.Core;
using CrossyRoad.Saving;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
namespace CrossyRoad.UI
{
    [System.Serializable]
    public class ScreenElement
    {
        public GameObject screen;
        public ScreenType screenType;
        public List<GameObject> connectingUI;
    }
    public class GameScreenController : MonoBehaviour
    {
        [SerializeField] private List<ScreenElement> screenElements;

        private ScreenElement currentScreen;
        private PlayerController playerController;
        private SavingWrapper saving;
        private void Awake()
        {
            playerController = FindObjectOfType<PlayerController>();
            currentScreen = screenElements.Find(element => element.screenType == ScreenType.Start);
            TurnOffAllScreens();
        }
        private void OnEnable()
        {
            playerController.onDeath += EndGame;
        }
        private void OnDisable()
        {
            playerController.onDeath -= EndGame;
        }
        private void Start()
        {
            saving = FindObjectOfType<SavingWrapper>();
            ActivateAllConnectedUI(currentScreen);
        }


        public void StartGame()
        {
            TurnOffAllScreens();
            if (currentScreen.screenType == ScreenType.EndGame)
            {
                DOTween.CompleteAll();
            }
            currentScreen = screenElements.Find(element => element.screenType == ScreenType.InGame);
            ActivateAllConnectedUI(currentScreen);
        }
        public void EndGame()
        {
            TurnOffAllScreens();
            currentScreen = screenElements.Find(element => element.screenType == ScreenType.EndGame);

            saving.Save();
            ActivateAllConnectedUI(currentScreen);
        }
        private void TurnOffAllScreens()
        {
            if (currentScreen.connectingUI != null)
            {
                foreach (var UIElement in currentScreen.connectingUI)
                {
                    UIElement.SetActive(false);
                }
            }
            foreach (var element in screenElements)
            {
                element.screen.SetActive(false);
            }
        }
        private void ActivateAllConnectedUI(ScreenElement UIScreen)
        {
            UIScreen.screen.SetActive(true);
            if (UIScreen.connectingUI != null)
            {
                foreach (var UIElement in UIScreen.connectingUI)
                {
                    UIElement.SetActive(true);
                }
            }
        }
    }
}