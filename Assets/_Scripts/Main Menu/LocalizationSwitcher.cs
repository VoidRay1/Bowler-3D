using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocalizationSwitcher : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _localesDropdown;

    private void OnEnable()
    {
        _localesDropdown.value = GameContext.Instance.SaveData.LocaleIndex;
        _localesDropdown.onValueChanged.AddListener(SwitchLocalization);
    }

    private void OnDisable()
    {
        _localesDropdown.onValueChanged.RemoveListener(SwitchLocalization);
    }

    public void SwitchLocalization(int index)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
        GameContext.Instance.SaveData.LocaleIndex = (byte)index;
        GameContext.Instance.BinarySaveSystem.Save(GameContext.Instance.SaveData);
    }
}