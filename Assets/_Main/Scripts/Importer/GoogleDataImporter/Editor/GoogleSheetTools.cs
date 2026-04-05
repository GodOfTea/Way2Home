using System;
using System.IO;
using Cathei.BakingSheet;
using Cathei.BakingSheet.Internal;
using Cathei.BakingSheet.Unity;
using UnityEditor;
using UnityEngine;
using Newtonsoft.Json;

namespace Editor.GoogleDataImporter
{
    public static class GoogleSheetTools
    {
        private static readonly string ImportPath = "Assets/_Main/Resources/Import";
        
        // unit test google account credential
        private static readonly string GoogleCredential = File.ReadAllText("way2-439914-6d4de463d0be.json");
        private static readonly string GoogleSheetId = "1yqQXXLMUcvLiZL3Gfb8WZwmH3uiRDuOtthl9ohYwNT8";

        public class PrettyJsonConverter : JsonSheetConverter
        {
            public PrettyJsonConverter(string path, IFileSystem fileSystem = null) : base(path, fileSystem)
            { }

            public override JsonSerializerSettings GetSettings(Microsoft.Extensions.Logging.ILogger logError)
            {
                var settings = base.GetSettings(logError);

                settings.Formatting = Formatting.Indented;

                return settings;
            }
        }

        [MenuItem("Import/Events")]
        public static async void ConvertFromGoogle()
        {
            var jsonPath = Path.Combine(Application.streamingAssetsPath, "Way2HomeData");

            var googleConverter = new GoogleSheetConverter(GoogleSheetId, GoogleCredential);

            var sheetContainer = new SheetContainer(UnityLogger.Default);

            await sheetContainer.Bake(googleConverter);

            var exporter = new ScriptableObjectSheetExporter(ImportPath);

            await sheetContainer.Store(exporter);

            AssetDatabase.Refresh();

            Debug.Log("Google sheet converted.");
        }
    }
}