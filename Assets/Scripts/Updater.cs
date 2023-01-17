using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Updater : MonoBehaviour
{
    private float _currentBundleVersion;
    private float _actualBungleVersion;

    private void Start()
    {
        //Debug.Log(PlayerSettings.bundleVersion);
        //currentBundleVersion = float.Parse(Application.version, System.Globalization.CultureInfo.InvariantCulture);
        //Debug.Log(currentBundleVersion);
        //StartCoroutine(GetText());
    }

    IEnumerator GetText()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://play.google.com/store/apps/details?id=com.HappyRussianGames.Calculated");
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(www.error);
        }
        else
        {
            string response = www.downloadHandler.text;
            string t = System.Text.RegularExpressions.Regex.Match(response, @"<span class=""htlgb"">[0-9]+\.[0-9]").Groups[0].Value;
            string[] values = t.Split('>');

            _actualBungleVersion = float.Parse(values[1], System.Globalization.CultureInfo.InvariantCulture);
            if (_currentBundleVersion < _actualBungleVersion)
            {
                GetComponent<MenuInterface>().ShowUpdatePanel();
            }
        }
    }
}
