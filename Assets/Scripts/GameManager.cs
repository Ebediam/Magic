using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{

    public GameObject canvas;

    
    public List<Button> buttons;
    public delegate void TurnDelegate();
    public static TurnDelegate enemyTurn;
    public static TurnDelegate playerTurn;

    public bool isEnemyTurn;

    // Start is called before the first frame update
    void Start()
    {
        Player.attackEvent += ChangeTurn;
        EnemyManager.EnemyTurnEndEvent += ChangeTurn;
        foreach(Button button in canvas.GetComponentsInChildren<Button>())
        {
            buttons.Add(button);
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


}
