using Naninovel;
using UnityEngine;

public class SpriteRootBackground : SpriteBackground
{
    public SpriteRootBackground (string id, BackgroundMetadata metadata)
        : base(id, metadata) { }

    protected override LocalizableResourceLoader<Texture2D> ConstructAppearanceLoader (OrthoActorMetadata metadata)
    {
        // Override the path prefix of the loader so that background sprites
        // can be loaded from the root of the `/Backgrounds` folder.
        var providerMngr = Engine.GetService<ResourceProviderManager>();
        var localeMngr = Engine.GetService<LocalizationManager>();
        var appearanceLoader = new LocalizableResourceLoader<Texture2D>(
            providerMngr.GetProviderList(metadata.LoaderConfiguration.ProviderTypes),
            localeMngr, metadata.LoaderConfiguration.PathPrefix);

        return appearanceLoader;
    }
}
