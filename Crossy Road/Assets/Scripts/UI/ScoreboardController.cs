using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreboardController : MonoBehaviour, ISaveable
{
    [SerializeField] private Text scoreShower;
    private PlayerMovement player;
    private int score = 0;
    private float playerMaxPosition = 0f;
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
        score++;
        scoreShower.text = score.ToString();
    }

    public object CaptureState()
    {
        return score;
    }

    public void RestoreState(object state)
    {
        score = (int)state;
    }
}
