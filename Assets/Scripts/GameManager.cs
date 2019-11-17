using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    public GameObject canvas;
    public delegate void  RestartDelegate();
    public static RestartDelegate RestartEvent;
    
    public List<Button> buttons;
    public delegate void TurnDelegate();
    public static TurnDelegate enemyTurn;
    public static TurnDelegate playerTurn;

    public GameObject restartButton;

    public bool isEnemyTurn;

    // Start is called before the first frame update
    void Start()
    {
        Player.attackEvent += ChangeTurn;
        EnemyManager.EnemyTurnEndEvent += ChangeTurn;
        EnemyManager.EnemyDeadEvent += RestartButton;

        foreach(Button button in canvas.GetComponentsInChildren<Button>())
        {
            buttons.Add(button);
        }
    }

    public void RestartButton()
    {
        if (restartButton)
        {
            restartButton.SetActive(true);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisableControls()
    {
        foreach (Button button in buttons)
        {
            button.interactable = false;
        }
    }

    public void EnableControls()
    {
        foreach (Button button in buttons)
        {
            button.interactable = true;
        }
    }

    public void ChangeTurn(float seconds)
    {
        if (isEnemyTurn)
        {
            Invoke("PlayerTurn", seconds);

        }
        else
        {
            Invoke("EnemyTurn", seconds);
            DisableControls();
        }

        isEnemyTurn = !isEnemyTurn;
    }

    public void PlayerTurn()
    {
        playerTurn?.Invoke();

        EnableControls();
    }

    public void EnemyTurn()
    {
        enemyTurn?.Invoke();
    }

    public void Restart()
    {

        Player.attackEvent -= ChangeTurn;
        EnemyManager.EnemyTurnEndEvent -= ChangeTurn;
        EnemyManager.EnemyDeadEvent = RestartButton;
        RestartEvent?.Invoke();
        SceneManager.LoadScene(0);
    }


}
