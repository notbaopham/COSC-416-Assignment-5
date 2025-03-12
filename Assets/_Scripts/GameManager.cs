using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : SingletonMonoBehavior<GameManager>
{
    [SerializeField] private int maxLives = 3;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private Ball ball;
    [SerializeField] private Transform bricksContainer;
    [SerializeField] ParticleSystem ps;

    private ParticleSystem psi;

    private int currentBrickCount;
    private int totalBrickCount;

    private void Start()
    {
        livesText.text = $"Lives: {maxLives}";

        // hide game over UI
        gameOverUI.SetActive(false);
    } 
    private void OnEnable()
    {
        InputHandler.Instance.OnFire.AddListener(FireBall);
        ball.ResetBall();
        totalBrickCount = bricksContainer.childCount;
        currentBrickCount = bricksContainer.childCount;
    }

    private void OnDisable() 
    {
        InputHandler.Instance.OnFire.RemoveListener(FireBall);
    }

    private void FireBall()
    {
        ball.FireBall();
    }

    public void OnBrickDestroyed(Vector3 position)
    {
        // fire audio here
        // implement particle effect here
        psi = Instantiate(ps, position, Quaternion.identity);
        // add camera shake here
        currentBrickCount--;
        Debug.Log($"Destroyed Brick at {position}, {currentBrickCount}/{totalBrickCount} remaining");
        if(currentBrickCount == 0) SceneHandler.Instance.LoadNextScene();
    }

    public void KillBall()
    {
        maxLives--;
        // update lives on HUD here
        // game over UI if maxLives < 0, then exit to main menu after delay
        livesText.text = $"Lives: {maxLives}";
        if(maxLives <= 0)
        {
            gameOverUI.SetActive(true);
            Time.timeScale = 0; // Freeze the game
            StartCoroutine(ExitToMainMenuAfterDelay());
        }
        ball.ResetBall();
    }
    IEnumerator ExitToMainMenuAfterDelay()
    {
        Debug.Log("Game Over! Exiting to Main Menu in 2 seconds");
        yield return new WaitForSecondsRealtime(2f); // Wait for 2 seconds
        Time.timeScale = 1; // Restore time scale before loading the scene
        Debug.Log("Game Over! Exiting to Main Menu");
        SceneHandler.Instance.LoadMenuScene();
    }
}
