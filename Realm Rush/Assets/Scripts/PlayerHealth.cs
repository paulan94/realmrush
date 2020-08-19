using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int baseHealth = 20;
    [SerializeField] Text healthText;

    [SerializeField] AudioClip goalAudioSfx;
    // Start is called before the first frame update

    private void Start()
    {
        healthText.text = baseHealth.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        GetComponent<AudioSource>().PlayOneShot(goalAudioSfx);
        baseHealth--;
        healthText.text = baseHealth.ToString();
        Debug.Log("this working?");

    }
}
