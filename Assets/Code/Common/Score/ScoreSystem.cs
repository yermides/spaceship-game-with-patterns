using System.Collections.Generic;
using Code.Common.Events;
using Code.Core.DataStorage;
using Code.Ships.Common;
using Code.Util;

namespace Code.Common.Score
{
    public class ScoreSystemImpl : IScoreSystem, IEventReceiver<ShipDestroyedEvent>, IEventReceiver<VictoryEvent>
    {
        private readonly IDataStorage _dataStorage;
        private const string KeyUserData = "UserData";

        public ScoreSystemImpl(IDataStorage dataStorage)
        {
            _dataStorage = dataStorage;
                
            var eventQueue = ServiceLocator.Instance.GetService<IEventQueue>();
            eventQueue.Subscribe<ShipDestroyedEvent>(OnEvent);
            eventQueue.Subscribe<VictoryEvent>(OnEvent);
        }
        
        public void Reset()
        {
            CurrentScore = 0;
        }

        public int[] GetBestScores()
        {
            var userData = _dataStorage.GetData<UserData>(KeyUserData) ?? new UserData();
            return userData.BestScores;
        }

        public int CurrentScore { get; private set; }
        
        public void OnEvent(ShipDestroyedEvent signal)
        {
            AddScore(signal);
        }

        private void AddScore(ShipDestroyedEvent signal)
        {
            if (signal.team != Teams.Enemy) return;

            CurrentScore += signal.scoreToAdd;
        }

        public void OnEvent(VictoryEvent signal)
        {
            UpdateBestScores(CurrentScore);
        }

        private void UpdateBestScores(int newScore)
        {
            var scoreBoard = GetBestScores();
            
            // Add and trim list to only 10 numbers
            var scoreList = new List<int>(scoreBoard) { newScore };
            
            scoreList.Sort();
            scoreList.Reverse();

            while (scoreList.Count > 10)
            {
                scoreList.RemoveAt(10);
            }

            var finalScoreBoard = scoreList.ToArray();
            
            // Persist result
            var userData = new UserData() { BestScores = finalScoreBoard };
            _dataStorage.SetData(userData, KeyUserData);
        }
    }
}