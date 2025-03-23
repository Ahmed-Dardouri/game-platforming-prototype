using UnityEngine;

public class TimeManager : MonoBehaviour {

	public float slowdownFactor = 0.05f;
	public float slowdownLength = 1f;

	void Update ()
	{
		Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
		Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
	}

	public void DoSlowmotion ()
	{
		Time.timeScale = slowdownFactor;
		Time.fixedDeltaTime = Time.timeScale * .02f;
	}
    public void UndoSlowmotion ()
	{
		Time.timeScale = 1f;
	}

    public bool isSlowed ()
	{
		return Time.timeScale != 1f;
	}
}