using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

// RASKALOF, v1.0, Main basketball logic script
public class Basketball_game_manager : MonoBehaviour {
    enum GAME_TYPE { INFINITY, TIME_TRIAL }

    [Header("SETTINGS")]
    [SerializeField] GAME_TYPE game_type = GAME_TYPE.INFINITY; // Game type
    [SerializeField] double game_time_seconds = 20; // Awailable game time in seconds in case of TIME_TRIAL
    [SerializeField] Transform spawn_point = null; // Where to spawn new ball
    [SerializeField] GameObject ball_prefab = null; // Ball prefab
    [SerializeField] byte main_munu_scene_index = 0; // Set here main menu id from build settings
    [SerializeField] Transform hoops_GO = null; // Root GO of all hoops


    [Header("UI")]
    [SerializeField] string ui_time_text = "TIME LEFT: ";
    [SerializeField] Text ui_time_counter = null;
    [SerializeField] string ui_score_text = "SCORE: ";
    [SerializeField] Text ui_score_counter = null;
    [SerializeField] Text ui_level_name = null;
    [SerializeField] DialogEndGame dialog = null; // Only used in case of TIME_TRIAL to end game
    [SerializeField] GameObject restart_button = null; // Link o restart button
    [SerializeField] GameObject home_button = null; // Link to home (main menu) button

    int total_score = 0; // Current score counter
    bool game_end = false; // Flag
    public static Basketball_game_manager Instance; // Singleton pattern
    GameObject current_ball; // Current ball prefab in scene (do not link here manually)
    int random_number = -1; // Used for unique target random

    private void Awake() {
        Instance = this; // Singleton pattern
    }

    void Start() {
     
    }
    public void startthegame()
    {
        // Initialization
        restart_button.SetActive(true); // Activate restart button
        home_button.SetActive(true); // Activate home button
        total_score = 0; // Reset score
        game_end = false; // Game not ended
        ui_score_counter.enabled = true; // Enable score counter
        ui_level_name.enabled = true; // Enable level name
        SwitchHoop(); // Generate random hoop
        UpdateUIScore(); // Update UI
        if (game_type == GAME_TYPE.TIME_TRIAL)
        {
            UpdateUITime(); // Only if TIME_TRIAL we need to update time
            StartCoroutine("TimeCalc"); // Only if TIME_TRIAL we need to calculate time
        }
        else
        {
            ui_time_counter.enabled = false; // If not TIME_TRIAL disable time
        }
        SpawnBall(); // After all initialization done - spawn ball
    }
    public void AddScore(int value) {
        total_score += value; // Increase score
        UpdateUIScore(); // Update UI
    }

    public void CheckGameStatus() {
        if (game_end) { // In game ended
            DestroyCurrentBall(); // Destroy current ball
            ui_time_counter.enabled = false; // Disable all UI elements
            ui_score_counter.enabled = false;
            ui_level_name.enabled = false;
            restart_button.SetActive(false);
            home_button.SetActive(false);
            dialog.gameObject.SetActive(true); // Enable end game dialog (in case of !TIME_TRIAL we cant reach here so its doesnt matter)
            dialog.ShowDialog("TIME OVER", "YOUR SCORE: ", total_score); // Show end dialog
        } else {
            DestroyCurrentBall(); // If game not ended we need to clear things
            SpawnBall(); // Spawn new ball
            SwitchHoop(); // Generate new hoop
        }
    }

    IEnumerator TimeCalc() { // Time calculations
        yield return new WaitForSeconds(1f); // Delay to load stuff from level start
        DateTime end_time = DateTime.Now.AddSeconds(game_time_seconds);

        if (!game_end) {
            while (DateTime.Now < end_time) {
                game_time_seconds = (end_time - DateTime.Now).TotalSeconds;
                UpdateUITime();
                yield return null;
            }
        }
        EndGame(); // If time is ended call game end
    }

    void UpdateUITime() {
        ui_time_counter.text = ui_time_text + ((int)(game_time_seconds)).ToString();
    }

    void UpdateUIScore() {
        ui_score_counter.text = ui_score_text + total_score;
    }

    public void SpawnBall() {
        current_ball = Instantiate(ball_prefab, spawn_point);
    }

    public void DestroyCurrentBall() {
        if (current_ball != null) {
            Destroy(current_ball);
            current_ball = null;
        }
    }

    void EndGame() {
        StopAllCoroutines(); // Stop time calcs
        game_time_seconds = 0; // Game time zero
        game_end = true; // Game end flag = true
    }

    public void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoHome() {
        SceneManager.LoadScene(main_munu_scene_index); // Load Main menu
    }

    void SwitchHoop() {
        foreach (Transform t in hoops_GO.transform) {
            t.gameObject.SetActive(false);
        }
        
        int child = GetUniqueRandom(0, hoops_GO.transform.childCount, ref random_number);
        hoops_GO.transform.GetChild(child).gameObject.SetActive(true);
    }

    int GetUniqueRandom(int min, int max, ref int exclude) {
        int tmp = UnityEngine.Random.Range(min, max);
        if (tmp == exclude) {
            return GetUniqueRandom(min, max, ref exclude);
        }
        exclude = tmp;
        return exclude;
    }
}
