using System;
using System.Collections.Generic;

namespace Runtime.GameState
{
    [Serializable]
    public class HighScores
    {
        public List<Score> Scores { get; private set; } = new List<Score>();

        public void AddScore(Score newScore)
        {
            if (!IsHighScore(newScore.value))
            {
                return;
            }
            
            Scores.Add(newScore);
            Scores.Sort((scoreA, scoreB) => scoreA.value.CompareTo(scoreB.value));

            int scoresLenght = Scores.Count > 10 ? 10 : Scores.Count;

            Scores = Scores.GetRange(0, scoresLenght);
        }

        public bool IsHighScore(int scoreValue)
        {
            foreach (Score score in Scores)
            {
                if (scoreValue > score.value)
                {
                    return true;
                }
            }
            
            return Scores.Count == 0;
        }
    }
}