namespace Balance.Data
{
    public class BalanceData
    {
        public PlayerData PlayerData { get; }
        public LevelGeneratorData LevelGeneratorData { get; }
        public ScoreData ScoreData { get; }

        public BalanceData(ScoreData scoreData, LevelGeneratorData levelGeneratorData, PlayerData playerData)
        {
            ScoreData = scoreData;
            LevelGeneratorData = levelGeneratorData;
            PlayerData = playerData;
        }
    }
}