using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class YouTubeVideoPlayer : MonoBehaviour
{
    public bool isPlaying, isBuffering, isPausing;
    public double lastTimePlayed;

    [Space(25)]
    public GameObject playButton;
    public GameObject pauseButton;
    public GameObject exitButton;
    public GameObject playbackTimeline;

    [Space(25)]
    public GameObject bufferingImage;
    public GameObject videoCanvas;
    public GameObject materiCanvas;
    public VideoPlayer videoPlayer;

    void Update()
    {
        CheckVideoStatus();

        if (isPausing)
        {
            playButton.SetActive(true);
            pauseButton.SetActive(false);
            exitButton.SetActive(true);
            playbackTimeline.SetActive(true);
        }
        if (isPlaying)
        {
            if (Input.GetMouseButtonDown(0))
                InteractVideo();
            else if (Input.GetMouseButton(0))
                InteractVideo();
            else if (Input.GetMouseButtonUp(0))
                StartCoroutine(ResetUIStatus(3f));
        }
    }

    void EndReached(VideoPlayer vp)
    {
        CloseVideoPanel();
    }

    public void CloseVideoPanel()
    {
        videoPlayer.time = 0;
        playbackTimeline.GetComponent<Image>().fillAmount = 0f;
        Screen.orientation = ScreenOrientation.Portrait;

        playButton.SetActive(false);
        pauseButton.SetActive(false);
        exitButton.SetActive(false);
        playbackTimeline.SetActive(false);

        materiCanvas.SetActive(true);
        videoCanvas.SetActive(false);
        isPausing = isPlaying = false;
    }

    public void InteractVideo()
    {
        playButton.SetActive(false);
        pauseButton.SetActive(true);
        exitButton.SetActive(true);
        playbackTimeline.SetActive(true);
    }

    public void CheckVideoStatus()
    {
        isPlaying = videoPlayer.isPlaying;
        isPausing = videoPlayer.isPaused;

        if (!isPlaying && !isPausing) 
        { 
            bufferingImage.SetActive(true);
            exitButton.SetActive(true);
            StartCoroutine(ResetUIStatus());
        }
        else bufferingImage.SetActive(isBuffering);

        if (videoPlayer.isPlaying && (Time.frameCount % (int)(videoPlayer.frameRate + 1)) == 0)
        {
            if (lastTimePlayed == videoPlayer.time) isBuffering = true;
            else isBuffering = false;
            
            lastTimePlayed = videoPlayer.time;
        }
    }

    public void OpenVideoMateri(string youtubeURL)
    {
        materiCanvas.SetActive(false);
        videoCanvas.SetActive(true);

        Screen.orientation = ScreenOrientation.LandscapeLeft;
        StartCoroutine(SetVideoPlayerUrl(youtubeURL));
    }

    private IEnumerator ResetUIStatus()
    {
        yield return new WaitUntil(() => isPlaying);

        if (!isPausing && Input.touchCount <= 0)
        {
            playButton.SetActive(false);
            pauseButton.SetActive(false);
            exitButton.SetActive(false);
            playbackTimeline.SetActive(false);
        }
    }

    private IEnumerator ResetUIStatus(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (!isPausing && Input.touchCount <= 0)
        {
            playButton.SetActive(false);
            pauseButton.SetActive(false);
            exitButton.SetActive(false);
            playbackTimeline.SetActive(false);
        }
    }

    private IEnumerator SetVideoPlayerUrl(string youtubeURL)
    {
        if (videoPlayer == null)
        {
            Debug.LogError("No video player.");
            yield break;
        }
        
        var request = new YouTubeRequest(youtubeURL);
        yield return request.SendRequest();

        if (request.Result == YouTubeRequestResult.Error)
        {
            Debug.LogError($"Failed to fetch YouTube video details: {request.Error}");
            yield break;
        }
        
        Debug.Log("Fetched YouTube formats.");

        try
        {
            videoPlayer.url = request.BestQualityFormat.url;
            videoPlayer.loopPointReached += EndReached;
            videoPlayer.Play();
        }
        catch (InvalidOperationException)
        {
            Debug.LogError("Failed to find any compatible formats.");
        }
    }
}