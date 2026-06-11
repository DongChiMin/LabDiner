using UnityEngine;

namespace LabDiner.Shared
{
    [System.Serializable]
    public static class LevelProgressSaveFile 
    {
        public const string PROGRESS_FILE_NAME = "level_progress.dat";

        public static LevelProgressSave LoadFromFile() 
        {
            string json = JSONExecutor.ReadFromFile(PROGRESS_FILE_NAME, true);
            if (string.IsNullOrEmpty(json)) return new LevelProgressSave();
            return json.FromJson<LevelProgressSave>();
        }

        public static void SaveToFile(LevelProgressSave progress) 
        {
            JSONExecutor.WriteToFile(progress.ToJson(), PROGRESS_FILE_NAME, true);
        }
    }
}
