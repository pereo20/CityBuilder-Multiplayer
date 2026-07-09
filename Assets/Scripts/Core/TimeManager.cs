using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private float gameSpeed = 1f;
    private float elapsedTime = 0f;
    private int gameDay = 0;

    public void Initialize()
    {
        Debug.Log("TimeManager initialized");
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime * gameSpeed;
        
        // 1 game day = 60 real seconds
        if (elapsedTime >= 60f)
        {
            gameDay++;
            elapsedTime = 0f;
            Debug.Log($"Game Day: {gameDay}");
        }
    }

    public float GetGameSpeed() => gameSpeed;
    public void SetGameSpeed(float speed) => gameSpeed = Mathf.Clamp(speed, 0.1f, 2f);
    public int GetGameDay() => gameDay;
    public float GetElapsedTime() => elapsedTime;
}
