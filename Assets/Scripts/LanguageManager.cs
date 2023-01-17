using UnityEngine;
using TMPro;
using System.Xml;

public class LanguageManager : MonoBehaviour
{
    public static LanguageManager S;
    private XmlDocument _languageXml;

    private void Awake()
    {
        S = this;
    }

    public void RefreshAllTexts()
    {
        string lang = SaveController.S.GetLanguage();
        _languageXml = new XmlDocument();
        GameObject[] switchableTexts = GameObject.FindGameObjectsWithTag("SwitchableText");
        for (int i = 0; i < switchableTexts.Length; i++)
        {
            string[] name = switchableTexts[i].name.Split('_');
            TextAsset languageTextAsset = Resources.Load<TextAsset>("XML/LanguageXML");
            _languageXml.LoadXml(languageTextAsset.text);
            XmlNode xmlNode = _languageXml.SelectSingleNode("xml");
            XmlNode langNode = xmlNode.SelectSingleNode(lang);
            XmlNodeList textNodes = langNode.SelectNodes("text");
            foreach (XmlNode node in textNodes)
                if (node.Attributes["name"].Value == name[0])
                    switchableTexts[i].GetComponent<TMP_Text>().text = node.Attributes["transcription"].Value;
        }
    }
}
