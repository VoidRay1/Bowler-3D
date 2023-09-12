using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private TMP_Text _loadOperationDescriptionText;
    [SerializeField] private Slider _progressBar;
    [SerializeField] private float _progressBarSpeed;

    private float _targetProgress;

    public async Task Load(Queue<ILoadingOperation> loadingOperations)
    {
        _canvas.enabled = true;
        StartCoroutine(UpdateProgressBar());
        foreach(ILoadingOperation operation in loadingOperations)
        {
            ResetProgressBar();
            _loadOperationDescriptionText.text = operation.Description;
            await operation.Load(OnProgress);
            await WaitForBarFill();
        }
        _canvas.enabled = false;
    }

    private void ResetProgressBar()
    {
        _progressBar.value = 0;
        _targetProgress = 0;
    }

    private async Task WaitForBarFill()
    {
        while (_progressBar.value < _targetProgress)
        {
            await Task.Delay(1);
        }
        await Task.Delay(TimeSpan.FromSeconds(0.15f));
    }

    private void OnProgress(float progress)
    {
        _targetProgress = progress;
    }

    private IEnumerator UpdateProgressBar()
    {
        while (_canvas.enabled)
        {
            if (_progressBar.value < _targetProgress)
            {
                _progressBar.value += Time.deltaTime * _progressBarSpeed;
            }
            yield return null;
        }
    }
}