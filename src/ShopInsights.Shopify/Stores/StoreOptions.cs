using System;
using System.IO;

namespace ShopInsights.Shopify.Stores
{
    public class StoreOptions
    {
        [Obsolete("Use GetFilePath() instead of Filepath")]
        public string FilePath { get; set; }

        public string GetFilePath()
        {
            var filePath =  FilePath;

            if (Path.IsPathRooted(filePath))
            {
                return filePath;
            }

            var localFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            filePath = Path.Combine(localFolder, filePath);
            return filePath;
        }

    }
}
