using System.Collections;
using UnityEngine;

public class BackGroundMusic : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSourceMain;

    [SerializeField]
    private AudioSource _audioSourceFight;

    [SerializeField]
    private bool _isFight;

    [SerializeField]
    private float _volumeModifier = 0.5f;

    private Coroutine _currentCoroutine = null;

    private void Start()
    {
        _audioSourceFight.volume = 0f;
        _audioSourceMain.volume = _volumeModifier;
        _audioSourceFight.Play();
        _audioSourceMain.Play();
    }

    private IEnumerator Transition(AudioSource audioSource1, AudioSource audioSource2)
    {
        Debug.Log("coroutine");
        audioSource2.Play();
        audioSource2.volume = 0f;
        while (audioSource1.volume > 0.1f)
        {
            audioSource1.volume = Mathf.Lerp(audioSource1.volume, 0, Time.deltaTime);
            yield return new WaitForEndOfFrame();
            audioSource2.volume = Mathf.Lerp(audioSource2.volume, _volumeModifier, Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        audioSource1.Stop();
        audioSource1.volume = 0f;
        _currentCoroutine = null;
    }

    /// <summary>
    /// Invoke in Update
    /// </summary>
    public void StartFight()
    {
        if (_currentCoroutine == null)
        {
            if (!_isFight)
            {
                _audioSourceMain.volume = _volumeModifier;
                _isFight = true;
                _currentCoroutine = StartCoroutine(Transition(_audioSourceMain, _audioSourceFight));
            }
        }
    }

    /// <summary>
    /// Invoke in Update
    /// </summary>
    public void GoMainTheme()
    {
        if (_currentCoroutine == null)
        {
            if (_isFight)
            {
                _audioSourceFight.volume = _volumeModifier;
                _isFight = false;
                _currentCoroutine = StartCoroutine(Transition(_audioSourceFight, _audioSourceMain));
            }
        }
    }
}
