using ICities;

namespace AssetImporterIndexDebug
{
    public class Mod : IUserMod
    {
        public string Name => "Asset Importer Texture Loader Fix";
        public string Description => "This mod fixes the ArrayOutOfIndex error bug that bricked the asset editor in the RaceDay DLC";
    }
}
