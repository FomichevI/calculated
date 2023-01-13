
using UnityEngine;
using System.Xml;
using System.IO;

public class SaveController : MonoBehaviour
{
    public static SaveController S;
    private XmlDocument saveX;

    void Awake()
    {
        S = this;
        if (!File.Exists(Application.persistentDataPath + "/SaveXML.xml"))
        {
            float f = 0.5f;
            saveX = new XmlDocument();
            XmlElement saveElem = saveX.CreateElement("save");
            XmlAttribute levelAtt = saveX.CreateAttribute("level");
            XmlText levelText = saveX.CreateTextNode("0");
            levelAtt.AppendChild(levelText);
            XmlAttribute maxLevelAtt = saveX.CreateAttribute("maxLevel");
            XmlText maxLevelText = saveX.CreateTextNode("1");
            maxLevelAtt.AppendChild(maxLevelText);
            XmlAttribute volumeAtt = saveX.CreateAttribute("volume");
            XmlText volumeText = saveX.CreateTextNode(f.ToString());
            volumeAtt.AppendChild(volumeText);
            XmlAttribute musicAtt = saveX.CreateAttribute("music");
            XmlText musicText = saveX.CreateTextNode(f.ToString());
            musicAtt.AppendChild(musicText);

            XmlAttribute volumeOnAtt = saveX.CreateAttribute("volumeOn");
            XmlText volumeOnText = saveX.CreateTextNode("1");
            volumeOnAtt.AppendChild(volumeOnText);
            XmlAttribute musicOnAtt = saveX.CreateAttribute("musicOn");
            XmlText musicOnText = saveX.CreateTextNode("1");
            musicOnAtt.AppendChild(musicOnText);

            XmlAttribute themeAtt = saveX.CreateAttribute("theme");
            XmlText themeText = saveX.CreateTextNode("1");
            themeAtt.AppendChild(themeText);
            XmlAttribute languageAtt = saveX.CreateAttribute("language");
            XmlText languageText = saveX.CreateTextNode("rus");
            languageAtt.AppendChild(languageText);

            saveElem.Attributes.Append(levelAtt);
            saveElem.Attributes.Append(maxLevelAtt);
            saveElem.Attributes.Append(volumeAtt);
            saveElem.Attributes.Append(musicAtt);

            saveElem.Attributes.Append(volumeOnAtt);
            saveElem.Attributes.Append(musicOnAtt);

            saveElem.Attributes.Append(themeAtt);
            saveElem.Attributes.Append(languageAtt);

            saveX.AppendChild(saveElem);
            saveX.Save(Application.persistentDataPath + "/SaveXML.xml");
            Debug.Log("SaveXML is created!");
        }
        //else
        //    File.Delete(Application.persistentDataPath + "/SaveXML.xml");
    }

    public void SetCurrentLevel(int level)
    {
        saveX = new XmlDocument();
        saveX.Load(Application.persistentDataPath + "/SaveXML.xml");
        XmlNodeList nodeList = saveX.SelectNodes("save");
        nodeList[0].Attributes["level"].Value = level.ToString();
        saveX.Save(Application.persistentDataPath + "/SaveXML.xml");
    }
    public void SetMaxLevel(int level)
    {
        if (level < 100)
        {
            saveX = new XmlDocument();
            saveX.Load(Application.persistentDataPath + "/SaveXML.xml");
            XmlNodeList nodeList = saveX.SelectNodes("save");
            if (int.Parse(nodeList[0].Attributes["maxLevel"].Value) < level)
                nodeList[0].Attributes["maxLevel"].Value = level.ToString();
            saveX.Save(Application.persistentDataPath + "/SaveXML.xml");
        }
    }
    public void SetVolume(float volume)
    {
        saveX = new XmlDocument();
        saveX.Load(Application.persistentDataPath + "/SaveXML.xml");
        XmlNodeList nodeList = saveX.SelectNodes("save");
        nodeList[0].Attributes["volume"].Value = volume.ToString();
        saveX.Save(Application.persistentDataPath + "/SaveXML.xml");
    }
    public void SetMusic(float volume)
    {
        
        saveX = new XmlDocument();
        saveX.Load(Application.persistentDataPath + "/SaveXML.xml");
        XmlNodeList nodeList = saveX.SelectNodes("save");
        nodeList[0].Attributes["music"].Value = volume.ToString();
        saveX.Save(Application.persistentDataPath + "/SaveXML.xml");
    }
    public void SetTheme(int themeNum)
    {
        saveX = new XmlDocument();
        saveX.Load(Application.persistentDataPath + "/SaveXML.xml");
        XmlNodeList nodeList = saveX.SelectNodes("save");
        nodeList[0].Attributes["theme"].Value = themeNum.ToString();
        saveX.Save(Application.persistentDataPath + "/SaveXML.xml");
    }
    public void SetLanguade(string lang)
    {
        saveX = new XmlDocument();
        saveX.Load(Application.persistentDataPath + "/SaveXML.xml");
        XmlNodeList nodeList = saveX.SelectNodes("save");
        nodeList[0].Attributes["language"].Value = lang;
        saveX.Save(Application.persistentDataPath + "/SaveXML.xml");
    }
    public int GetMaxLevel()
    {
        saveX = new XmlDocument();
        saveX.Load(Application.persistentDataPath + "/SaveXML.xml");
        XmlNodeList nodeList = saveX.SelectNodes("save");
        return int.Parse(nodeList[0].Attributes["maxLevel"].Value);
    }
    public int GetCurrentLevel()
    {
        saveX = new XmlDocument();
        saveX.Load(Application.persistentDataPath + "/SaveXML.xml");
        XmlNodeList nodeList = saveX.SelectNodes("save");
        return int.Parse(nodeList[0].Attributes["level"].Value);
    }
    public int GetTheme()
    {
        saveX = new XmlDocument();
        saveX.Load(Application.persistentDataPath + "/SaveXML.xml");
        XmlNodeList nodeList = saveX.SelectNodes("save");
        return int.Parse(nodeList[0].Attributes["theme"].Value);
    }
    public float GetVolume()
    {
        saveX = new XmlDocument();
        saveX.Load(Application.persistentDataPath + "/SaveXML.xml");
        XmlNodeList nodeList = saveX.SelectNodes("save");
        return float.Parse(nodeList[0].Attributes["volume"].Value);
    }
    public float GetMusic()
    {
        saveX = new XmlDocument();
        saveX.Load(Application.persistentDataPath + "/SaveXML.xml");
        XmlNodeList nodeList = saveX.SelectNodes("save");
        return float.Parse(nodeList[0].Attributes["music"].Value);
    }
    public void SetVolumeOn(int isOn)
    {
        saveX = new XmlDocument();
        saveX.Load(Application.persistentDataPath + "/SaveXML.xml");
        XmlNodeList nodeList = saveX.SelectNodes("save");
        nodeList[0].Attributes["volumeOn"].Value = isOn.ToString();
        saveX.Save(Application.persistentDataPath + "/SaveXML.xml");
    }
    public void SetMusicOn(int isOn)
    {
        saveX = new XmlDocument();
        saveX.Load(Application.persistentDataPath + "/SaveXML.xml");
        XmlNodeList nodeList = saveX.SelectNodes("save");
        nodeList[0].Attributes["musicOn"].Value = isOn.ToString();
        saveX.Save(Application.persistentDataPath + "/SaveXML.xml");
    }
    public int GetVolumeOn()
    {
        saveX = new XmlDocument();
        saveX.Load(Application.persistentDataPath + "/SaveXML.xml");
        XmlNodeList nodeList = saveX.SelectNodes("save");
        return int.Parse(nodeList[0].Attributes["volumeOn"].Value);
    }
    public int GetMusicOn()
    {
        saveX = new XmlDocument();
        saveX.Load(Application.persistentDataPath + "/SaveXML.xml");
        XmlNodeList nodeList = saveX.SelectNodes("save");
        return int.Parse(nodeList[0].Attributes["musicOn"].Value);
    }
    public string GetLanguage()
    {
        saveX = new XmlDocument();
        saveX.Load(Application.persistentDataPath + "/SaveXML.xml");
        XmlNodeList nodeList = saveX.SelectNodes("save");
        return nodeList[0].Attributes["language"].Value;

    }

}
