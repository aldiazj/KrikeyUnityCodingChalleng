namespace Runtime.GameState
{
    public class ScoringSystem
    {
        public int Score { get; private set; }

        public void Reset()
        {
            Score = 0;
        }

        public void Increase(int points)
        {
            Score += points;
        }
    }
}