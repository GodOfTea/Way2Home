using UnityEditor;

namespace Data.Scripts.Editor.GoogleImporter
{
    public class ConfigImportsMenu
    {
        private const string SPREADSHWWT_ID = "1yqQXXLMUcvLiZL3Gfb8WZwmH3uiRDuOtthl9ohYwNT8";
        private const string SHEETS_NAME = "Events";
        private const string CREDENTIALS_PATH = "way2-439914-6d4de463d0be.json";
        
        //[MenuItem("Import/Events Settings")]
        private static async void LoadEventsSettings()
        {
            var sheetsImporter = new GoogleSheetsImporter(CREDENTIALS_PATH, SPREADSHWWT_ID);

            //await sheetsImporter.DownloadAndParseSheet(SHEETS_NAME);
        }
    }
}