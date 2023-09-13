using System;

namespace Runtime.Health
{
    public class HealthSystem
    {
        private readonly int maxLives;
        public int Lives { get; private set; }

        public Action onLifeLost;
        public Action onAllLivesLost;

        public HealthSystem(int lives)
        {
            Lives     = lives;
            maxLives  = lives;
        }

        public void TakeDamage()
        {
            Lives--;
            onLifeLost?.Invoke();

            if (Lives <= 0)
            {
                onAllLivesLost?.Invoke();
            }
        }

        public void Reset()
        {
            Lives  = maxLives;
        }
    }
}