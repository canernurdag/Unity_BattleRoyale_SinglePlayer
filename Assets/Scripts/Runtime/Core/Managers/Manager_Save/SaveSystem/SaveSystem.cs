using UnityEngine;

namespace Assets.Scripts.Runtime.Systems.SaveSystem
{
    /// <summary>
    /// This class handles all save/load systems with one single class which is named as "SaveState".
    /// This class can be extended by adding more security.
    /// This class is using Easy Save System 3 as provider. It can be replaced by other systems.
    ///
    /// Tip: Please do not use constructor is "SaveSystem". It causes some errors.
    /// </summary>

    [DefaultExecutionOrder(-10)]
    public class SaveSystem : MonoBehaviour
    {
        #region Fields
        public SaveState SaveState = new SaveState();
        public bool IsLoadFinished { get; private set; }
        private const string _firstSaveCompletedString = "FirstSaveCompleted";
        #endregion

        private void Start()
        {
            Load();
        }

        /// <summary>
        /// Save the current state of SAVESTATE
        /// </summary>
        public void Save()
        {
            ES3.Save(_firstSaveCompletedString, true); // Save the "first Save"
            ES3.Save("state", SaveState); // Save the "State"

        }

        /// <summary>
        /// Load and Update the SAVESTATE
        /// </summary>
        public void Load()
        {
            bool isExist = ES3.KeyExists(_firstSaveCompletedString);
            if (!isExist)
            {
                SaveState = new SaveState();
                SaveState.SetInitialValues();
                Save();

            }
            else if (isExist)
            {
                SaveState = ES3.Load("state", new SaveState());
                MigrateToTheLatestBuildVersion();

            }

            SetIsLoadFinished(true);
        }

        public void SetIsLoadFinished(bool isLoadFinished)
        {
            IsLoadFinished = isLoadFinished;
        }

        private void MigrateToTheLatestBuildVersion()
        {

        }
    }
}

