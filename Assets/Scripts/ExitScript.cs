using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitScript : MonoBehaviour
{
    private AudioSource _audio;
    // Start is called before the first frame update
    void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (this.tag == "Exit" && collision.gameObject.tag == "Player")
        {
            _audio.Play();
            while(_audio.isPlaying)
            {

            }
            SceneManager.LoadScene(sceneBuildIndex: 2);
        }
    }
}
