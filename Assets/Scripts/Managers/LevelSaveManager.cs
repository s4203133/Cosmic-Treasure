using System.Collections.Generic;
using LMO;
using UnityEngine;

namespace NR {
    public class LevelSaveManager : MonoBehaviour {
        public static LevelSaveManager Instance { get; private set; }

        [SerializeField] private List<Collectible> gemsInLevel;

        [SerializeField] private LevelSave levelSaveObject;

        [SerializeField] private GemCounter gemCounter;

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

        public void ClearSavedGems() {
            foreach (var gem in gemsInLevel) {
                gem.Reset();
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

