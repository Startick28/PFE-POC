using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource playerAudioSource;
    private AudioSource source;
    [SerializeField] private GameObject AudioPrefab;
    [SerializeField] private List<AudioClip> listAudios;

    [SerializeField] private float volumeChangeMultiplier = 0.2f;
    [SerializeField] private float pitchChangeMultiplier = 0.2f;

    [SerializeField] private float randomRangeX = 100f;
    [SerializeField] private float randomRangeY = 3f;
    [SerializeField] private float randomRangeZ = 100f;

    [SerializeField] private float timeToPlay = 200f;
    private float timeRemaining = 0f;
    // Start is called before the first frame update

    [SerializeField] private List<AudioClip> gunSound;
    [SerializeField] private AudioClip alarmSound;

    public static AudioManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining < timeToPlay)
        {
            timeRemaining += Time.deltaTime;
        }
        else
        {
            StartCoroutine(PlayRandomSound());
            timeRemaining = 0f;
        }
    }

    IEnumerator PlayRandomSound()
    {
        Vector3 randomPos = new Vector3(Random.Range(-randomRangeX, randomRangeX), Random.Range(-randomRangeY, randomRangeY), Random.Range(-randomRangeZ, randomRangeZ));
        GameObject tempObject = Instantiate(AudioPrefab, transform.position + randomPos, Quaternion.identity);
        AudioSource source = tempObject.GetComponent<AudioSource>();
        source.clip = listAudios[Random.Range(0, listAudios.Count - 1)];
        source.volume = Random.Range(1 - volumeChangeMultiplier, 1);
        source.pitch = Random.Range(1 - pitchChangeMultiplier, 1 + pitchChangeMultiplier);
        source.spatialBlend = 1;
        source.loop = false;
        source.Play();
        yield return new WaitForSeconds(source.clip.length * 4);
        source.Stop();
        Destroy(tempObject);

    }

    IEnumerator PlaySpecificSound(AudioClip audio)
    {
        Vector3 randomPos = new Vector3(Random.Range(-randomRangeX, randomRangeX), Random.Range(-randomRangeY, randomRangeY), Random.Range(-randomRangeZ, randomRangeZ));
        GameObject tempObject = Instantiate(AudioPrefab, transform.position + randomPos, Quaternion.identity);
        AudioSource source = tempObject.GetComponent<AudioSource>();
        source.clip = audio;
        source.volume = Random.Range(1 - volumeChangeMultiplier, 1);
        source.pitch = Random.Range(1 - pitchChangeMultiplier, 1 + pitchChangeMultiplier);
        source.spatialBlend = 1;
        source.loop = false;
        source.Play();
        yield return new WaitForSeconds(source.clip.length * 4);
        source.Stop();
        Destroy(tempObject);
    }

    IEnumerator PlaySpecificSoundOnPosition(AudioClip audio, Vector3 position)
    {
        GameObject tempObject = Instantiate(AudioPrefab, position, Quaternion.identity);
        AudioSource source = tempObject.GetComponent<AudioSource>();
        source.clip = audio;
        source.volume = 1;
        source.pitch = 1;
        source.spatialBlend = 0;
        source.loop = false;
        source.Play();
        yield return new WaitForSeconds(source.clip.length * 4);
        source.Stop();
        Destroy(tempObject);
    }
    public void playRandomGunSoundRPC()
    {
        GetComponent<PhotonView>().RPC("PlayRandomGunSound", RpcTarget.All, transform.position);
    }

    [PunRPC]
    private void PlayRandomGunSound(Vector3 position)
    {
        StartCoroutine(PlaySpecificSoundOnPosition(gunSound[Random.Range(0,gunSound.Count-1)], position));
    }

    public void PlayAlarmSoundRPC()
    {
        GetComponent<PhotonView>().RPC("PlayAlarmSound", RpcTarget.All, transform.position);
    }

    [PunRPC]
    private void PlayAlarmSound(Vector3 position)
    {
        GameObject tempObject = Instantiate(AudioPrefab, position, Quaternion.identity);
        AudioSource source = tempObject.GetComponent<AudioSource>();
        source.clip = alarmSound;
        source.volume = 1;
        source.pitch = 1;
        source.spatialBlend = 0;
        source.loop = true;
        source.Play();
    }
}
