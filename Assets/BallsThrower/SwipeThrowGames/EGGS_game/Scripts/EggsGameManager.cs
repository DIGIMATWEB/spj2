using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
// ------------------
// Author:  RASKALOF (https://connect.unity.com/u/dmitry-raskalov)
// Version: 1.0
// Date:    12.09.2019

public class EggsGameManager : MonoBehaviour {
    // Global vars (can be changed from inspector)
	[Header("COMMON")]
    [SerializeField] GameObject ball = null;                // Ball prefab
    [SerializeField] Transform spawn_transform = null;      // Transform where ball will be instantiated
    [SerializeField] GameObject targets_parent = null;      // Empty GO which handles all targets GOs
    [Header("RULES")]
    [SerializeField] byte balls_amount = 10;         // Maximum amount of balls to throw
    [SerializeField] byte percent_to_win = 80;       // This value and more means player won, less - means player lose
    [SerializeField] byte next_level = 1;            // (If needed) next level's id if player won
    [SerializeField] float spawn_delay = 0.1f;       // How long to wait between new ball spawns
    [Header("UI")]
    [SerializeField] string level_text = "LEVEL : ";  // Text to display with level counter
    [SerializeField] string level_numba = "X";       // Level number cant be grabbed from build settings, cos you can have any kind of sorting, better to controll this value for UI
    [SerializeField] Text level_text_component = null;      // Link to UI component
    [SerializeField] string balls_text = "THROWS: "; // Text to display with throws counter
    [SerializeField] Text balls_text_component = null;      // Link to UI component
    [SerializeField] string score_text = "SCORE: ";  // Just text to display with score points
    [SerializeField] Text score_text_component = null;      // Link to UI component
    [Header("WIN_LOSE")]
    [SerializeField] GameObject dialog = null;                   // UI game object to show when player completes level
    [SerializeField] string win_title = "COMPLETED";      // Header to display in win dialog
    [SerializeField] string lose_title = "FAILED";        // Header to display in lose dialog
    [SerializeField] string score_label = "YOUR SCORE: "; // Header to display in win dialog
    [SerializeField] Button restart_button = null;        // Restart button link to disable at gameover
    [Header("MISC")]
    [SerializeField] AudioClip win_sound = null;          // Sound to play when player won 
    [SerializeField] AudioClip lose_sound = null;         // Sound to play when player lose 
    // Private (system purposes)
    byte current_score = 0; // Current player score
    byte balls_left = 0; // How much throws left
    byte score_to_win = 0; // Score to win level
    byte fail_count = 0; // Fail count
    int random_number = -1; // Used for unique target random
    GameObject current_spawned_ball; // Link to current spawned ball to be abble destroy it if needed or manipulate
    public static EggsGameManager Instance; // Singleton pattern
    bool gameover;

    private void Start() { // Used for initialization purposes
        gameover = false;
        if(Instance == null) Instance = this;
        score_to_win  = 0;
        fail_count    = 0;
        balls_left    = balls_amount;
        current_score = 0;
        UpdateLevelTextUI(level_numba);
        UpdateBallsUI((byte)balls_left);
        ShowWinMenu(false);
        ShowLoseMenu(false);
        Throw();
        SpawnTarget();
        UpdateUI(0);
    }

    public void AddScore(byte amount) { // Add score when target hitted
        current_score += amount; // Increase score
        UpdateUI(current_score); // Update UI
    }

    public void AddFail() { // Add fail count to counter
        fail_count++;
        SpawnTarget(); // Spawn new target
    }

    public byte GetBallsLeft() {
        return balls_left;
    }

    public void Throw() { // Calculate all pre throw things
        balls_left--; // Decrease balls count
        UpdateBallsUI(balls_left); // Update balls count
        if(GetBallsLeft() > 0 && (fail_count + 1 < balls_amount / 2)) { // If no balls left and fails count less than half of total balls to throw +1
            StartCoroutine("SpawnAuto"); // Spawn new ball
        } else {
            if(!gameover) StartCoroutine(GameEnd(1f)); // Other case - end game
        }
    }

    public IEnumerator GameEnd(float delay = 0.5f) {
        if(!gameover) {
            gameover = true;
            yield return new WaitForSeconds(delay);
            DisableUI();
            if(current_spawned_ball != null) Destroy(current_spawned_ball); // Destroy current ball if exists
            DisableAllTargets();

            if(Mathf.RoundToInt(((float)current_score / (float)score_to_win) * 100f) >= percent_to_win) ShowWinMenu(true); // Calculate win conditions
            else ShowLoseMenu(true);
        }
    }

    public void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel() {
        SceneManager.LoadScene(next_level);
    }

    public void LaunchLevel(int level_number) {
        SceneManager.LoadScene(level_number);
    }

    public void LaunchLevel(string level_name) {
        SceneManager.LoadScene(level_name);
    }

    public IEnumerator SpawnAuto() {
        yield return new WaitForSeconds(spawn_delay);
        SpawnBall(); // Spawn ball after delay
    }

    public void SpawnBall() {
        current_spawned_ball = Instantiate(ball, spawn_transform.position, Quaternion.identity, Camera.main.transform); // Get link to current active ball to manipulate
	}

    public void SpawnTarget() { // Used for enabling one target from array and disable others
        DisableAllTargets();
        if(GetBallsLeft() > 0) { // If we can play
            int child = GetUniqueRandom(0, targets_parent.transform.childCount, ref random_number); // Generate random target id
            targets_parent.transform.GetChild(child).gameObject.SetActive(true); // Enable choosen target
            score_to_win += targets_parent.transform.GetChild(child).gameObject.GetComponent<EggsTarget>().GetTargetScore(); // Increase total awailable score counter
        }
    }

    public void DisableAllTargets() {
        foreach(Transform t in targets_parent.transform) {
            t.gameObject.SetActive(false);
        }
    }

    int GetUniqueRandom(int min, int max, ref int exclude) { // This method should always return unique number
        int tmp = Random.Range(min, max);
        if(tmp == exclude) {
            return GetUniqueRandom(min, max, ref exclude);
        }
        exclude = tmp;
        return exclude;
    }

    public void UpdateUI(byte score) {
        if(score_text_component != null) score_text_component.text = score_text + score;
        else {
            Debug.LogWarning("UpdateUI: score_text_component is not attached! Is it OK?");
        }
    }

    public void UpdateBallsUI(byte score) {
        if(balls_text_component != null) balls_text_component.text = balls_text + score;
        else {
            Debug.LogWarning("UpdateBallsUI: balls_text_component is not attached! Is it OK?");
        }
    }

    public void UpdateLevelTextUI(string value) {
        if (level_text_component != null) level_text_component.text = level_text + value;
        else {
            Debug.LogWarning("UpdateLevelTextUI: level_text_component is not attached! Is it OK?");
        }
    }

    public void DisableUI() {
        level_text_component?.gameObject.SetActive(false);
        score_text_component?.gameObject.SetActive(false);
        balls_text_component?.gameObject.SetActive(false);
        restart_button?.gameObject.SetActive(false);
    }

    public void ShowWinMenu(bool status) {
        dialog?.SetActive(status);
        if(status) {
            GetComponent<AudioSource>()?.PlayOneShot(win_sound);
            dialog.GetComponent<DialogHandler>().ShowDialog(win_title, score_label, true, current_score, score_to_win);
        }
    }

    public void ShowLoseMenu(bool status) {
        dialog?.SetActive(status);
        if(status) {
            GetComponent<AudioSource>()?.PlayOneShot(lose_sound);
            dialog.GetComponent<DialogHandler>().ShowDialog(lose_title, score_label, false, current_score, score_to_win);
        }
    }

}
