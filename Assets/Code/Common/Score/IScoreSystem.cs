namespace Code.Common.Score
{
    public interface IScoreSystem
    {
        void Reset();
        int[] GetBestScores();
        int CurrentScore { get; }
    }
}