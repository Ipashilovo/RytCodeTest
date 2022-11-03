namespace Balance.Data
{
    public class ScoreData
    {
        public int ScoreOnPoint { get;  }
        public int ScoreOnFinish { get; }

        public ScoreData(int scoreOnPoint, int scoreOnFinish)
        {
            ScoreOnPoint = scoreOnPoint;
            ScoreOnFinish = scoreOnFinish;
        }
    }
}