using Naninovel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityCommon;
using UnityEngine;
using UnityGoogleDrive;

[RequireComponent(typeof(CanvasGroup))]
public class HowToUsePanel : ScriptableUIBehaviour, IHowToUseUI
{
    private readonly struct UploadFile
    {
        public byte[] ByteContent => Encoding.UTF8.GetBytes(StrConent);
        public readonly string Path, StrConent;
        private const string pathPrefix = "NaninovelSandbox";
        private const string fileName = "readme.txt";

        public UploadFile (string folder, string content)
        {
            Path = $"{pathPrefix}/{folder}/{fileName}";
            StrConent = content;
        }
    }

    public event Action<float> OnUploadProgress;

    public float UploadProgress { get; private set; } = 0f;

    private StateManager stateManager;

    protected override void Awake ()
    {
        base.Awake();

        stateManager = Engine.GetService<StateManager>();
    }

    protected override void OnEnable ()
    {
        base.OnEnable();

        stateManager.OnGameLoadStart += Hide;
    }

    protected override void OnDisable ()
    {
        base.OnDisable();

        stateManager.OnGameLoadStart -= Hide;
    }

    public Task InitializeAsync () => Task.CompletedTask;

    private static readonly List<UploadFile> uploadFiles = new List<UploadFile> {
        new UploadFile("Scripts", "Create Google Documents in this folder to use them as novel scripts.\n\nValid scripts will be avilable at the Script Navigator and you can use `@goto DocumentName` in scripts to load them."),
        new UploadFile("Characters", "Create folders with character names in this folder. Inside each character folder place .png files to use them as character appearances.\n\nUse `@char FolderName.FileName` in novel scripts to show character with a specific appearance."),
        new UploadFile("Backgrounds", "Place .png or .jpg files in this folder to use them as backgrounds.\n\nUse `@back FileName` in novel scripts to show backgrounds."),
        new UploadFile("Audio", "Place .mp3 files in this folder to use them as background music (bgm) and sound effects (sfx).\n\nUse `@bgm FileName` and `@sfx FileName` in novel scripts to use them.")
    };

    public async Task<bool> UploadDataAsync ()
    {
        var success = true;

        for (int i = 0; i < uploadFiles.Count; i++)
        {
            UploadProgress = i / (float)uploadFiles.Count;
            OnUploadProgress?.Invoke(UploadProgress);

            var uploadFile = uploadFiles[i];
            var googleFile = new UnityGoogleDrive.Data.File { Content = uploadFile.ByteContent };
            var fileId = await Helpers.CreateOrUpdateFileAtPathAsync(googleFile, uploadFile.Path, uploadMimeType: "text/plain");
            if (string.IsNullOrEmpty(fileId)) { success = false; break; }
        }

        OnUploadProgress?.Invoke(1f);
        UploadProgress = 0f;
        return success;
    }
}
