using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreboardController : MonoBehaviour, ISaveable
{
    [SerializeField] private Text scoreShower;
    [SerializeField] private Text scoreRecoredShower;
    private PlayerMovement player;
    private int currentScore = 0;
    private int recordScore = 0;
    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
    }
    private void Start()
    {
        AddScore();  
    }
    private void OnEnable()
    {
        player.onMoveForward.AddListener(AddScore);
    }
    private void OnDisable()
    {
        player.onMoveForward.RemoveListener(AddScore);
    }
    private void AddScore()
    {
        currentScore++;
        if(currentScore >= recordScore)
        {
            recordScore = currentScore;
        }
        UpdateUI();
    }

    public object CaptureState()
    {
        return recordScore;
    }

    public void RestoreState(object state)
    {
        currentScore = 0;
        recordScore = (int)state;
        UpdateUI();
    }

    private void UpdateUI()
    {
        scoreRecoredShower.text = string.Format("Рекорд: {0}", recordScore);
        scoreShower.text = currentScore.ToString();
    }
}
