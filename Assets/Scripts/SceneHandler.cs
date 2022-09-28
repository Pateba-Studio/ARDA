using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
  public void HomeScene()
  {
    SceneManager.LoadScene("HomeScene");
  }

  public void ArScene()
  {
    SceneManager.LoadScene("ARScene");
  }

  public void TesScene()
  {
    SceneManager.LoadScene("TesScene");
  }

  public void VideoScene()
  {
    SceneManager.LoadScene("VideoScene");
  }

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.Escape)) { HomeScene(); }
  }
}
