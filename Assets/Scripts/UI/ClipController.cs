using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ClipController : MonoBehaviour
{
    [SerializeField] VideoPlayer player;
    [SerializeField] GameObject introClip;

    // Start is called before the first frame update
    void Start()
    {
        player.Play();
    }

    // Update is called once per frame
    void Update()
    {
        ActiveVideo();
    }

    private IEnumerator ActiveVideo()
    {
        while (player.isPlaying)
        {
            yield return null;
        }

        introClip.SetActive(false);
    }
}
