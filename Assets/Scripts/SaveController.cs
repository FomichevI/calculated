using UnityEngine;
using System.Xml;
using System.IO;

public class SaveController : MonoBehaviour
{
    public static SaveController S;
    private XmlDocument _saveX;

    void Awake()
    {
        S = this;
        if (!File.Exists(Application.persistentDataPath + "/SaveXML.xml"))
        {
            float f = 0.5f;
            _saveX = new XmlDocument();
            XmlElement saveElem = _saveX.CreateElement("save");
            XmlAttribute levelAtt = _saveX.CreateAttribute("level");
            XmlText levelText = _saveX.CreateTextNode("0");
            levelAtt.AppendChild(levelText);
            XmlAttribute maxLevelAtt = _saveX.CreateAttribute("maxLevel");
            XmlText maxLevelText = _saveX.CreateTextNode("1");
            maxLevelAtt.AppendChild(maxLevelText);
            XmlAttribute volumeAtt = _saveX.CreateAttribute("volume");
            XmlText volumeText = _saveX.CreateTextNode(f.ToString());
            volumeAtt.AppendChild(volumeText);
            XmlAttribute musicAtt = _saveX.CreateAttribute("music");
            XmlText musicText = _saveX.CreateTextNode(f.ToString());
            musicAtt.AppendChild(musicText);

            XmlAttribute volumeOnAtt = _saveX.CreateAttribute("volumeOn");
            XmlText volumeOnText = _saveX.CreateTextNode("1");
            volumeOnAtt.AppendChild(volumeOnText);
            XmlAttribute musicOnAtt = _saveX.CreateAttribute("musicOn");
            XmlText musicOnText = _saveX.CreateTextNode("1");
            musicOnAtt.AppendChild(musicOnText);

            XmlAttribute themeAtt = _saveX.CreateAttribute("theme");
            XmlText themeText = _saveX.CreateTextNode("1");
            themeAtt.AppendChild(themeText);
            XmlAttribute languageAtt = _saveX.CreateAttribute("language");
            XmlText languageText = _saveX.CreateTextNode("rus");
            languageAtt.AppendChild(languageText);

            saveElem.Attributes.Append(levelAtt);
            saveElem.Attributes.Append(maxLevelAtt);
            saveElem.Attributes.Append(volumeAtt);
            saveElem.Attributes.Append(musicAtt);

            saveElem.Attributes.Append(volumeOnAtt);
            saveElem.Attributes.Append(musicOnAtt);

            saveElem.Attributes.Append(themeAtt);
            saveElem.Attributes.Append(languageAtt);

            _saveX.AppendChild(saveElem);
            _saveX.Save(Application.persistentDataPath + "/SaveXML.xml");
            Debug.Log("SaveXML is created!");
        }
        //else
        //    File.Delete(Application.persistentDataPath + "/SaveXML.xml");
    }

    public void SetCurrentLevel(int level)
    {
        _saveX = new XmlDocument();
        _saveX.Load(Application.persistentDataPath + "/SaveXML.xml");
        XmlNodeList nodeList = _saveX.SelectNodes("save");
        nodeList[0].Attributes["level"].Value = level.ToString();
        _saveX.Save(Application.persistentDataPath + "/SaveXML.xml");
    }
    public void SetMaxLevel(int level)
    {
        if (level < 100)
        {
            _saveX = new XmlDocument();
            _saveX.Load(Application.persistentDataPath + "/SaveXML.xml");
            XmlNodeList nodeList = _saveX.SelectNodes("save");
            if (int.Parse(nodeList[0].Attributes["maxLevel"].Value) < level)
                nodeList[0].Attributes["maxLevel"].Value = level.ToString();
            _saveX.Save(Application.persistentDataPath + "/SaveXML.xml");
        }
    }
    public void SetVolume(float volume)
    {
        _saveX = new XmlDocument();
        _saveX.Load(Application.persistentDataPath + "/SaveXML.xml");
        XmlNodeList nodeList = _saveX.SelectNodes("save");
        nodeList[0].Attributes["volume"].Value = volume.ToString();
        _saveX.Save(Application.persistentDataPath + "/SaveXML.xml");
    }
    public void SetMusic(float volume)
    {

        _saveX = new XmlDocument();
        _saveX.Load(Application.persistentDataPath + "/SaveXML.xml");
        XmlNodeList nodeList = _saveX.SelectNodes("save");
        nodeList[0].Attributes["music"].Value = volume.ToString();
        _saveX.Save(Application.persistentDataPath + "/SaveXML.xml");
    }
    public void SetTheme(int themeNum)
    {
        _saveX = new XmlDocument();
        _saveX.Load(Application.persistentDataPath + "/SaveXML.xml");
        XmlNodeList nodeList = _saveX.SelectNodes("save");
        nodeList[0].Attributes["theme"].Value = themeNum.ToString();
        _saveX.Save(Application.persistentDataPath + "/SaveXML.xml");
    }
    public void SetLanguade(string lang)
    {
        _saveX = new XmlDocument();
        _saveX.Load(Application.persistentDataPath + "/SaveXML.xml");
        XmlNodeList nodeList = _saveX.SelectNodes("save");
        nodeList[0].Attributes["language"].Value = lang;
        _saveX.Save(Application.persistentDataPath + "/SaveXML.xml");
    }
    public int GetMaxLevel()
    {
        _saveX = new XmlDocument();
        _saveX.Load(Application.persistentDataPath + "/SaveXML.xml");
        XmlNodeList nodeList = _saveX.SelectNodes("save");
        return int.Parse(nodeList[0].Attributes["maxLevel"].Value);
    }
    public int GetCurrentLevel()
    {
        _saveX = new XmlDocument();
        _saveX.Load(Application.persistentDataPath + "/SaveXML.xml");
        XmlNodeList nodeList = _saveX.SelectNodes("save");
        return int.Parse(nodeList[0].Attributes["level"].Value);
    }
    public int GetTheme()
    {
        _saveX = new XmlDocument();
        _saveX.Load(Application.persistentDataPath + "/SaveXML.xml");
        XmlNodeList nodeList = _saveX.SelectNodes("save");
        return int.Parse(nodeList[0].Attributes["theme"].Value);
    }
    public float GetVolume()
    {
        _saveX = new XmlDocument();
        _saveX.Load(Application.persistentDataPath + "/SaveXML.xml");
        XmlNodeList nodeList = _saveX.SelectNodes("save");
        return float.Parse(nodeList[0].Attributes["volume"].Value);
    }
    public float GetMusic()
    {
        _saveX = new XmlDocument();
        _saveX.Load(Application.persistentDataPath + "/SaveXML.xml");
        XmlNodeList nodeList = _saveX.SelectNodes("save");
        return float.Parse(nodeList[0].Attributes["music"].Value);
    }
    public void SetVolumeOn(int isOn)
    {
        _saveX = new XmlDocument();
        _saveX.Load(Application.persistentDataPath + "/SaveXML.xml");
        XmlNodeList nodeList = _saveX.SelectNodes("save");
        nodeList[0].Attributes["volumeOn"].Value = isOn.ToString();
        _saveX.Save(Application.persistentDataPath + "/SaveXML.xml");
    }
    public void SetMusicOn(int isOn)
    {
        _saveX = new XmlDocument();
        _saveX.Load(Application.persistentDataPath + "/SaveXML.xml");
        XmlNodeList nodeList = _saveX.SelectNodes("save");
        nodeList[0].Attributes["musicOn"].Value = isOn.ToString();
        _saveX.Save(Application.persistentDataPath + "/SaveXML.xml");
    }
    public int GetVolumeOn()
    {
        _saveX = new XmlDocument();
        _saveX.Load(Application.persistentDataPath + "/SaveXML.xml");
        XmlNodeList nodeList = _saveX.SelectNodes("save");
        return int.Parse(nodeList[0].Attributes["volumeOn"].Value);
    }
    public int GetMusicOn()
    {
        _saveX = new XmlDocument();
        _saveX.Load(Application.persistentDataPath + "/SaveXML.xml");
        XmlNodeList nodeList = _saveX.SelectNodes("save");
        return int.Parse(nodeList[0].Attributes["musicOn"].Value);
    }
    public string GetLanguage()
    {
        _saveX = new XmlDocument();
        _saveX.Load(Application.persistentDataPath + "/SaveXML.xml");
        XmlNodeList nodeList = _saveX.SelectNodes("save");
        return nodeList[0].Attributes["language"].Value;
    }
}
