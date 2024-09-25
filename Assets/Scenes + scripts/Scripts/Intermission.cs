using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Intermission : MonoBehaviour
{
    private VideoPlayer video;

    void Awake()
    {
        Cursor.visible = true;
        Camera cam = FindFirstObjectByType<Camera>();
        video = cam.GetComponent<VideoPlayer>();
        video.renderMode = VideoRenderMode.CameraNearPlane;
        video.Prepare();
    }

    public void StartFire()
    {
        GetComponent<Canvas>().enabled= false;
        video.Play();
        video.loopPointReached += OnMovieEnded;
    }

    void OnMovieEnded(VideoPlayer video)
    {
        SceneManager.LoadScene("MAP");
    }

    private void Update()
    {
        Cursor.visible = true;
    }
}
