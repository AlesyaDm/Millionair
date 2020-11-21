using System;
using System.Collections.Generic;
using System.Text;

namespace Millionair
{
    class Score
    {
       public Score (int totalScore = 0)
        {
            this.totalScore = totalScore;
        }

        private int totalScore;
        public int TotalScore { get { return totalScore; } }
        public void Increase()
        {
            totalScore = totalScore == 0
                  ? 100
                  : totalScore *= 2;
        }
        public void Reset()
        {
            totalScore = 0;
        }
    }
}
