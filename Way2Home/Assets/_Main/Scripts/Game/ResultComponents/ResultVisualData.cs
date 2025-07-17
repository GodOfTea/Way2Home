using Editor.GoogleDataImporter;

namespace Game.ResultComponents
{
    public class ResultVisualData
    {
        public ResultSheet.Reference ResultData;
        public bool IsWin;
        
        public ResultVisualData(ResultSheet.Reference resultData, bool isWin)
        {
            ResultData = resultData;
            IsWin = isWin;
        }
    }
}