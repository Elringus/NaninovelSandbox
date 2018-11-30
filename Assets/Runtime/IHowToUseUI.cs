using Naninovel.UI;
using System;
using System.Threading.Tasks;

public interface IHowToUseUI : IManagedUI
{
    event Action<float> OnUploadProgress;

    float UploadProgress { get; }

    Task<bool> UploadDataAsync ();
}
