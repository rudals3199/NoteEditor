using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using SimpleFileBrowser;

public class AudioExplorer: MonoBehaviour
{
    public Text audioName;
    public AudioSource audioSource;
    public Button openExplorerButton;

    void OnEnable()
    {
        openExplorerButton.onClick.AddListener(delegate { OpenExplorer(); });
    }


    void OpenExplorer()
    {
        FileBrowser.SetFilters(true, new FileBrowser.Filter("wav,mp3", ".wav", ".mp3"));
        FileBrowser.SetDefaultFilter(".wav");
        FileBrowser.AddQuickLink("Users", "C:\\Users", null);

        StartCoroutine(ShowLoadDialogCoroutine());
    }

    IEnumerator ShowLoadDialogCoroutine()
    {
        yield return FileBrowser.WaitForLoadDialog(false, true, null, "Load File", "Load");
        if (FileBrowser.Success)
        {
            StartCoroutine(ReadClip(FileBrowser.Result[0]));
        }
        else
        {
            openExplorerButton.interactable = true;
        }
    }

    IEnumerator ReadClip(string path)
    {
        DestroyImmediate(audioSource.clip);
        WWW www = new WWW("file://" + path);

        int startNameIndex = path.LastIndexOf("\\") + 1;
        int endNameIndex = path.LastIndexOf(".");
        audioName.text = path.Substring(startNameIndex, endNameIndex - startNameIndex);
        string fileDir = path.Substring(0, startNameIndex);

        while (!www.isDone)
            yield return null;

        if (path.Contains(".mp3"))
            audioSource.clip = NAudioPlayer.FromMp3Data(www.bytes);
        else
            audioSource.clip = www.GetAudioClip(false);

        onAudioChange.Invoke(audioSource.clip.length);
        onAudioLoaded.Invoke(audioName.text, fileDir);
        openExplorerButton.interactable = true;
    }

    public InitializeEvent onAudioChange = new InitializeEvent();
    public AudioLoadEvent onAudioLoaded = new AudioLoadEvent();
}


public class InitializeEvent : UnityEvent<float> { }
public class AudioLoadEvent : UnityEvent<string, string> { }
