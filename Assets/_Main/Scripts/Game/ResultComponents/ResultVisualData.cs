using Editor.GoogleDataImporter;

namespace Game.ResultComponents
{
    public class ResultVisualData
    {
        public GameResult.Reference ResultData;
        public bool IsWin;
        
        public ResultVisualData(GameResult.Reference resultData, bool isWin)
        {
            ResultData = resultData;
            IsWin = isWin;
        }
    }
}