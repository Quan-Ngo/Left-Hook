using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
	public GameObject settingPanel;
	
    public void Quit()
	{
		Application.Quit();
	}
	
	public void Play()
	{
		SceneManager.LoadScene("MainGame");
	}
	
	public void openSettingPanel()
	{
		settingPanel.gameObject.SetActive(true);
	}
	
	public void closePanel()
	{
		transform.parent.gameObject.SetActive(false);
	}
}
