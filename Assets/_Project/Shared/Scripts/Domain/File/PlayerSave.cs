using System;
using UnityEngine;

namespace LabDiner.Shared
{
    [Serializable]
    public class PlayerSave
    {
        public PlayerSave()
        {
            currentLevelIndex = 1;
        }

        public int currentLevelIndex = 1;
        public bool isDirty = false;

        // Cậu có thể thêm các thông tin khác sau này như:
        // public int totalMoney;

        public void SetDirty(bool dirty)
        {
            isDirty = dirty;
        }

        public void SetCurrentLevelIndex(int index)
        {
            currentLevelIndex = index;
            isDirty = true;
        }
    }
}