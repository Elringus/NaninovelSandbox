using Naninovel.UI;
using System;
using UniRx.Async;

public interface IHowToUseUI : IManagedUI
{
    event Action<float> OnUploadProgress;

    float UploadProgress { get; }

    UniTask<bool> UploadDataAsync ();
}
