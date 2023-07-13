using Minesweeper.Core.Enums;
using Minesweeper.Core.Models;

namespace Minesweeper.Core
{
    public static class DifficultyManager
    {
        private static readonly Dictionary<DifficultyLevel, GameSettings> _settingsByDifficultyLevel = new Dictionary<DifficultyLevel, GameSettings>
        {
            [DifficultyLevel.Beginner] = new GameSettings(9, 9, 10),
            [DifficultyLevel.Intermediate] = new GameSettings(16, 16, 40),
            [DifficultyLevel.Expert] = new GameSettings(16, 30, 99),
        };

        public static GameSettings GetGameSettingsByDifficultylevel(DifficultyLevel difficultyLevel)
        {
            return _settingsByDifficultyLevel[difficultyLevel];
        }
    }
}
