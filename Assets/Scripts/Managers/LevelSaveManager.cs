using System.Collections.Generic;
using LMO;
using UnityEngine;

namespace NR {
    /// <summary>
    /// Singleton that handles saving level data.
    /// This is used for keeping track of what gems the player has collected and unloading those that have been.
    /// </summary>
    public class LevelSaveManager : MonoBehaviour {
        public static LevelSaveManager Instance { get; private set; }

        [SerializeField] private List<Collectible> gemsInLevel;

        [SerializeField] private LevelSave levelSaveObject;

        [SerializeField] private GemCounter gemCounter;
        [SerializeField] private FloatVariable coinCounter;

        private void Awake() {
            if (Instance == null) { 
                Instance = this;
            } else {
                Destroy(this);
            }
        }

        private void Start() {
            if (levelSaveObject == null) {
                Debug.LogError("No Level Save scripable object is assigned to the level save manager.");
                return;
            }

            if (!PlayerSaveLoader.Instance.GetSaves().Contains(levelSaveObject)) {
                PlayerSaveLoader.Instance.AddLevel(levelSaveObject);
            }
            gemCounter.SetMaxGems(gemsInLevel.Count);

            foreach (int savedGem in levelSaveObject.gemsCollected) {
                gemsInLevel[savedGem].gameObject.SetActive(false);
            }

            foreach (var gem in gemsInLevel) {
                gem.OnInstanceCollected += GemCollected;
            }
        }

        public void ClearData() {
            coinCounter.value = 0;
            ClearSavedGems();
        }

        public void ClearSavedGems() {
            foreach (var gem in gemsInLevel) {
                //gem.Reset();
            }
            levelSaveObject.gemsCollected.Clear();
        }

        private void GemCollected(Collectible gem) {
            if (gemsInLevel.Contains(gem)) {
                levelSaveObject.gemsCollected.Add(gemsInLevel.IndexOf(gem));
            }
        }

        public int GetLevelGems() {
            return levelSaveObject.gemsCollected.Count;
        }
    }
}

