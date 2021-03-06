using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Ghost[] Ghosts;

    public Pacman Pacman;

    public Transform Pellets;

    public Text GameOverText;
    public Text GameWinText;
    public Text Score_GameOverText;
    public Text Lives_GameOverText;
    //public GameObject RestartGame_Button;

    public Text ScoreText;
    public Text LivesText;

    //public GameObject MainMenuButton;

  //  public AudioSource GameSound;

    public int GhostMultiplier { get; private set; } = 1;

    public int Score { get; private set; }

    public int Lives { get; private set; }


    public void Awake()
    {
        Pacman = FindObjectOfType<Pacman>();
        Ghosts = FindObjectsOfType<Ghost>();
        
       // GameSound = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        NewGame();
    }

    // Update is called once per frame
    private void Update()
    {
        if (this.Lives <= 0 )//&& Input.anyKeyDown)
        {
            Time.timeScale = 0;
            this.GameOverText.enabled = true;
            this.Score_GameOverText.enabled = true;
            this.Lives_GameOverText.enabled = true;
            
            //NewGame();
        }
    }

    public void OnPauseButtonPressed()
    {
        Time.timeScale = 0;
    }

    public void OnBackButtonPressed()
    {
        Time.timeScale = 1.0f;
    }

    //public void OnMainMenuButtonPressed()
    //{
    //    SceneManager.LoadScene("Start");
    //}

    public void OnQuitButtonPressed()
    {
        Application.Quit();
    }

    public void NewGame()
    {
        //this.GameOverText.enabled = false;
        SetScore(0);
        SetLives(6);
        NewRound();
    }

    private void NewRound()
    {
        this.GameOverText.enabled = false;
        this.GameWinText.enabled = false;
        this.Score_GameOverText.enabled = false;
        this.Lives_GameOverText.enabled = false;
        //GameSound.Play();
        //RestartGame_Button.SetActive(false);


        foreach (Transform Pellets in this.Pellets)
        {
            Pellets.gameObject.SetActive(true);
        }


        //for( int i = 0; i < this.Ghosts.Length; i++)
        //{
        //    this.Ghosts[i].gameObject.SetActive(true);
        //}

        //this.Pacman.gameObject.SetActive(true);

        ResetState();
    }

    private void ResetState()
    {
        //Debug.Log("Entered into Reset State");

        ResetGhostMultiplier();

        for (int i = 0; i < this.Ghosts.Length; i++)
        {
            this.Ghosts[i].ResetState();
        }

        this.Pacman.ResetState();
    }

    private void GameOver()
    {
        this.GameOverText.enabled = true;
        this.Score_GameOverText.enabled = true;
        this.Lives_GameOverText.enabled = true;
        //AudioSource.Destroy(GameSound);
        //RestartGame_Button.SetActive(true);


        for (int i = 0; i < this.Ghosts.Length; i++)
        {
            this.Ghosts[i].gameObject.SetActive(false);
        }

        this.Pacman.gameObject.SetActive(false);

    }

    private void SetScore(int Score)
    {
        this.Score = Score;

        this.ScoreText.text = Score.ToString();
        this.Score_GameOverText.text = "Score: " + Score.ToString();
    }

    private void SetLives(int Lives)
    {
        this.Lives = Lives;
        this.LivesText.text = "x" + (Lives/2).ToString();
        this.Lives_GameOverText.text = "Lives: " + (Lives/2).ToString();
    }

    public void GhostEaten(Ghost Ghost)
    {
        int Points = Ghost.Points * this.GhostMultiplier;

        SetScore(this.Score + Points);
        this.GhostMultiplier++;
    }

    public void PacmanEaten()
    {

        this.Pacman.FuncDeathSequence();
        //this.Pacman.gameObject.SetActive(false);

        SetLives(this.Lives - 1);
       
        if(this.Lives > 0)
        {
            Invoke(nameof(ResetState), 3.0f); 
        }
        else
        {
            Invoke(nameof(GameOver), 1.0f);           
        }
    }

    public void PelletEaten(Pellet Pellet)
    {
        Pellet.gameObject.SetActive(false);

        SetScore(this.Score + Pellet.Points);

        if (!HasRemainingPellets())
        {
            this.Pacman.gameObject.SetActive(false);
            //Invoke(nameof(NewRound), 5.0f);
            this.GameWinText.enabled = true;
            //MainMenuButton.SetActive(true);
            WaitforSecondsAfterWin();
            SceneManager.LoadScene("Start");
        }
    }

    IEnumerator WaitforSecondsAfterWin()
    {
        yield return new WaitForSeconds(2000.0f);
    }

        public void PowerPelletEaten(PowerPellet PowerPellet)
    {
        for(int i=0; i< this.Ghosts.Length;i++)
        {
            this.Ghosts[i].Frightened.Enable(PowerPellet.Duration);
        }

        PelletEaten(PowerPellet);
        CancelInvoke();
        Invoke(nameof(ResetGhostMultiplier), PowerPellet.Duration);
        
        //CancelInvoke();
       //PelletEaten(PowerPellet);
    }

    private bool HasRemainingPellets()
    {
        foreach (Transform Pellets in this.Pellets)
        {
            if (Pellets.gameObject.activeSelf)
            {
                return true;
            }
        }
        return false;
    }

    private void ResetGhostMultiplier()
    {
        this.GhostMultiplier = 1;
    }
}
