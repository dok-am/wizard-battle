using UnityEngine;

namespace IT.WizardBattle.Game
{
    public class SpawnPoint : MonoBehaviour
    {
        public bool PlayerSpawnPoint => _isPlayerSpawnPoint;

        [SerializeField] private bool _isPlayerSpawnPoint;
    }
}
