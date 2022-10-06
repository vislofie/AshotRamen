using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTEManager : MonoBehaviour
{
    private Image _qteImage;

    [SerializeField]
    private float _pointerSpeed = 3.0f;

    [SerializeField]
    private RectTransform _targetTransform;
    [SerializeField]
    private RectTransform _pointerTransform;

    private Coroutine _pointerUpdater;

    private void Awake()
    {
        _qteImage = GetComponent<Image>();
    }

    public void ActivateQTE()
    {
        _qteImage.enabled = true;

        _targetTransform.gameObject.SetActive(true);
        _pointerTransform.gameObject.SetActive(true);

        _targetTransform.localPosition = new Vector3(Random.Range(-90, 90), 0, 0);
        _pointerTransform.localPosition = new Vector3(-150, 0, 0);

        _pointerUpdater = StartCoroutine(UpdatePointerPosition());
    }

    public void DeactivateQTE()
    {
        _qteImage.enabled = false;

        _targetTransform.gameObject.SetActive(false);
        _pointerTransform.gameObject.SetActive(false);

        if (_pointerUpdater != null)
            StopCoroutine(_pointerUpdater);
    }

    private IEnumerator UpdatePointerPosition()
    {
        bool flipped = false;

        while (true)
        {
            if (!flipped)
            {
                _pointerTransform.position += Vector3.right * _pointerSpeed * Time.deltaTime / Time.timeScale;
                if (_pointerTransform.localPosition.x > 120)
                {
                    flipped = true;
                }
            }
            else
            {
                _pointerTransform.position += Vector3.left * _pointerSpeed * Time.deltaTime / Time.timeScale;
                if (_pointerTransform.localPosition.x < -120)
                {
                    flipped = false;
                }
            }

            yield return new WaitForEndOfFrame();
        }
    }

    /// <summary>
    /// Returns true if hit the target
    /// </summary>
    /// <returns></returns>
    public bool Click()
    {
        if (Mathf.Abs(_pointerTransform.anchoredPosition.x - _targetTransform.anchoredPosition.x) <= _targetTransform.rect.width / 1.5f)
        {
            return true;
        }
        return false;
    }
}
