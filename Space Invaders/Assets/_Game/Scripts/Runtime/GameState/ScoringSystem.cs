using System;

namespace Runtime.GameState
{
    public class ScoringSystem
    {
        public int Score { get; private set; }

        public Action<int> scoreUpdate;
        
        public void Reset()
        {
            Score = 0;
        }

        public void Increase(int points)
        {
            Score += points;
            scoreUpdate?.Invoke(Score);
        }
    }
}