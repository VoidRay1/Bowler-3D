using System;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

public class BallTemplate : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private Transform _previewParent;
    [SerializeField] private Button _selectBallButton;
    [SerializeField] private Image _ballUnlockedImage;

    private readonly LocalizedString _localizedName = new LocalizedString();

    private BallUI _ballUI;
    private Action<BallUI> _onBallSelected;

    private void OnDisable()
    {
        _selectBallButton.onClick.RemoveListener(SelectBall);
        _localizedName.Clear();
        _localizedName.StringChanged -= LocalizedNameChanged;
    }

    public void Init(string name, BallUI ballUI, Action<BallUI> onBallSelected)
    {
        InitializeNameLocalization(name);
        _ballUI = Instantiate(ballUI, _previewParent);
        _ballUnlockedImage.enabled = ballUI.Data.IsUnlocked == false;
        _onBallSelected = onBallSelected;
        _selectBallButton.onClick.AddListener(SelectBall);
    }

    public void Init(string name, BallUI ballUI)
    {
        InitializeNameLocalization(name);
        _ballUI = Instantiate(ballUI, _previewParent);
        _ballUnlockedImage.enabled = ballUI.Data.IsUnlocked == false;
    }

    private void SelectBall()
    {
        _onBallSelected?.Invoke(_ballUI);
    }

    private void InitializeNameLocalization(string name)
    {
        _localizedName.TableReference = Constants.MAIN_LOCALIZATION_TABLE;
        _localizedName.TableEntryReference = name;
        _localizedName.StringChanged += LocalizedNameChanged;
    }

    private void LocalizedNameChanged(string name)
    {
        _nameText.text = name;
    }
}