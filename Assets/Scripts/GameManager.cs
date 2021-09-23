using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Ghost[] Ghosts;

    public Pacman Pacman;

    public Transform Pellets;

    public int Score { get; private set; }

    public int Lives { get; private set; }

    // Start is called before the first frame update
    private void Start()
    {
        NewGame();
    }

    // Update is called once per frame
    private void Update()
    {
        if(this.Lives <= 0 && Input.anyKeyDown)
        {
            NewGame();
        }
    }

    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound();
    }

    private void NewRound()
    {
        foreach(Transform Pellets in this.Pellets)
        {
            Pellets.gameObject.SetActive(true);
        }

        ResetState();
    }

    private void ResetState()
    {
        for (int i = 0; i < this.Ghosts.Length; i++)
        {
            this.Ghosts[i].gameObject.SetActive(true);
        }

        this.Pacman.gameObject.SetActive(true);
    }

    private void GameOver()
    {
        for (int i = 0; i < this.Ghosts.Length; i++)
        {
            this.Ghosts[i].gameObject.SetActive(false);
        }

        this.Pacman.gameObject.SetActive(false);
    }

    private void SetScore(int Score)
    {
        this.Score = Score;
    }

    private void SetLives(int Lives)
    {
        this.Lives = Lives;
    }

    public void GhostEaten(Ghost Ghost)
    {
        SetScore(this.Score + Ghost.Points);
    }

    public void PacmanEaten()
    {
        this.Pacman.gameObject.SetActive(false);

        SetLives(this.Lives - 1);

        if(this.Lives > 0)
        {
            Invoke(nameof(ResetState), 3.0f); 
        }
        else
        {
            GameOver();
        }
    }

    
}
