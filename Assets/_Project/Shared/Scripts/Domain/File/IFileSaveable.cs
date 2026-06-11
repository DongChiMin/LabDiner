using UnityEngine;

namespace LabDiner.Shared
{
    public interface IFileSaveable
    {
        public const string PROGRESS_FILE_NAME = "level_progress.dat";

        public LevelProgressSave LoadFromFile();

        public void SaveToFile(LevelProgressSave progress);
    }
}
