using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocaleSelector : MonoBehaviour
{
    private bool _isActive = false;

    public static int localeID = 0;

    private void Start()
    {
        ChangeLocale(localeID);
    }

    public void ChangeLocale(int ID)
    {
        if (_isActive)
            return;
        
        StartCoroutine(SetLocale(ID));
    }

    IEnumerator SetLocale(int ID)
    {
        _isActive = true;
        
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[ID];
        
        localeID = ID;
        Options.Instance.optionsData.localeID = localeID;
        Options.Instance.Save();

        _isActive = false;
    }
}
