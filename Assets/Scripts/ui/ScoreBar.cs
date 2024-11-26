using UnityEngine;
using TMPro;
using BowlingGameEnums;

public class ScoreBar : UIElement {
    [Header("UI Text")]
    [SerializeField] TextMeshProUGUI _currentFrameText;
    [SerializeField] TextMeshProUGUI _currentRollText;
    [SerializeField] TextMeshProUGUI _scoreText;

    [Header("UI Panels")]
    [SerializeField] GameObject _readyPanel;
    [SerializeField] GameObject _scoreTable;

    [Header("References")]
    [SerializeField] InputReader _input;

    void Awake() {
        _input.ActionPerformed += DisablePanels;
    }

    public override void UpdateUI(IBowlingGame bowlingGame) {
        var isLastRoll = bowlingGame.CurrentFrame.IsFinished; //bogus: CurrentFrame is the next one.
        if (isLastRoll) ActivatePanels();

        UpdateText(
            bowlingGame.CurrentFrameIndex,
            bowlingGame.CurrentFrame.CurrentRollNumber,
            bowlingGame.TotalScore
        );
    }

    void UpdateText(int frame, RollNumber roll, int score) {
        _currentRollText.text = "Roll: " + (int)roll;
        _scoreText.text = "Score: " + score;
        _currentFrameText.text = "Frame: " + (int)(frame + 1);
    }

    public void ActivatePanels() {
        _readyPanel.SetActive(true);
        _scoreTable.SetActive(true);
    }

    private void DisablePanels() {
        _readyPanel.SetActive(false);
        _scoreTable.SetActive(false);
    }

    void OnDestroy() {
        _input.ActionPerformed -= DisablePanels;
    }
}