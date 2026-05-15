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
        private static readonly string GoogleCredential = File.ReadAllText("way2-439914-9904c6ab648f.json");
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
            GoogleSheetConverter googleConverter = new GoogleSheetConverter(GoogleSheetId, GoogleCredential);
            SheetContainer sheetContainer = new SheetContainer(UnityLogger.Default);

            await sheetContainer.Bake(googleConverter);

            ScriptableObjectSheetExporter exporter = new ScriptableObjectSheetExporter(ImportPath);

            await sheetContainer.Store(exporter);

            AssetDatabase.Refresh();
            Debug.Log("Google sheet converted.");
        }
    }
}