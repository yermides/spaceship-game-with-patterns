using TMPro;
using UnityEngine;

namespace Code.UI
{
    public class LeaderboardEntryView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI entryNumberText;
        [SerializeField] private TextMeshProUGUI entryPointsText;

        public void Configure(int number, int points)
        {
            entryNumberText.text = $"{number}.";
            entryPointsText.text = $"{points}";
        }
    }
}