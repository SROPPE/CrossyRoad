using CrossyRoad.Core;
using CrossyRoad.Saving;
using UnityEngine;
using UnityEngine.UI;

namespace CrossyRoad.UI
{
    public class ScoreboardController : MonoBehaviour, ISaveable
    {
        [SerializeField] private Text scoreShower;
        [SerializeField] private Text scoreRecordShower;
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
            player.onGetNewZPosition.AddListener(AddScore);
        }
        private void OnDisable()
        {
            player.onGetNewZPosition.RemoveListener(AddScore);
        }
        private void AddScore()
        {
            Debug.Log("We");
            currentScore++;
            if (currentScore >= recordScore)
            {
                recordScore = currentScore;
            }
            UpdateUI();
        }
        private void UpdateUI()
        {
            scoreRecordShower.text = string.Format("Рекорд: {0}", recordScore);
            scoreShower.text = currentScore.ToString();
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
    }
}