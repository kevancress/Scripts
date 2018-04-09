using UnityEngine;

public class TimeManager : MonoBehaviour {

    public float slowdownFactor = 0.001f;
    public float slowdownLength = 2f;


    #region Singleton
    public static TimeManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory Manager Present!");
            return;
        }
        instance = this;
    }

    #endregion

    public void MenuTime()
    {
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }

    public void NormalTime()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }
}
