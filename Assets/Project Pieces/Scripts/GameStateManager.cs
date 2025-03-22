using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public Singleton gameManager;    

    // Enum representing different game states
    public enum GameState
    {
        MainMenu_State,   // The game is at the main menu
        Gameplay_State,   // The game is actively being played
        Paused_State,      // The game is paused
        Options_State       //The game is at the options Menu
    }


    // Property to store the current game state, accessible publicly but modifiable only within this class
    public GameState currentState { get; private set; }

    // Added variable to track the last state for "back" functionality
    private GameState lastState;

    // Debugging variables to store the current and last game state as strings for easier debugging in the Inspector
    [SerializeField] private string currentStateDebug;
    [SerializeField] private string lastStateDebug;

    private void Start()
    {
        // Set the initial state of the game to Main Menu when the game starts
        ChangeStateToMainMenu();
    }

    // Method to change the current game state
    public void ChangeState(GameState newState)
    {
        // Store the current state as the last state before changing it
        lastState = currentState;
        lastStateDebug = lastState.ToString();

        // Assign the new state to currentState
        currentState = newState;

        // Call a function to handle any specific actions triggered by the state change
        HandleStateChange(newState);

        // Store the new state in a string variable for debugging purposes
        currentStateDebug = currentState.ToString();
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ChangeState(GameState.MainMenu_State);          
        }

        if (Input.GetKeyDown(KeyCode.Escape) && (currentState == GameState.Gameplay_State || currentState == GameState.Paused_State))
        {
            if(currentState == GameState.Paused_State)
            {
                ChangeState(GameState.Gameplay_State);
            }
            else
            {
                ChangeState(GameState.Paused_State);
            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            ChangeState(GameState.Gameplay_State);
        }        
    }


    // Handles any specific actions that need to occur when switching to a new state
    private void HandleStateChange(GameState state)
    {
        switch (state)
        {
            case GameState.MainMenu_State:                
                gameManager.uIManager.EnableMainMenuUI();
                Time.timeScale = 0;
                break;

            case GameState.Gameplay_State:
                gameManager.uIManager.EnableGameplayUI();               
                Time.timeScale = 1;
                break;

            case GameState.Paused_State:
                gameManager.uIManager.EnablePausedMenuUI();
                Time.timeScale = 0;
                break;

            case GameState.Options_State:
                gameManager.uIManager.EnableOptionsMenuUI();
                Time.timeScale = 0;
                break;
        }

        if (state == GameState.Gameplay_State)
        {
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }
    }

    public void ChangeStateToMainMenu()
    {
        SceneManager.LoadScene(0);
        ChangeState(GameState.MainMenu_State);       
    }

    public void ChangeStateToGamePlay()
    {
        ChangeState(GameState.Gameplay_State);       
    }

    public void ChangeStateToOptions()
    {
        ChangeState(GameState.Options_State);
    }

    public void GoBack()
    {
        ChangeState(lastState);
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }
}
