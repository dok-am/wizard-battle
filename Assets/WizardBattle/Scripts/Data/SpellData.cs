using IT.CoreLib.Interfaces;
using UnityEngine;

namespace IT.WizardBattle.Data
{
    [CreateAssetMenu(fileName = "Spell", menuName = "Wizard Battle/Spell")]
    public class SpellData : ScriptableObject, IStaticModel
    {
        public string Id => _id;
        public int Damage => _damage;
        public Sprite Icon => _icon;
        public GameObject ProjectileVisual => _projectileVisual;

        [SerializeField] private string _id;
        [SerializeField] private int _damage;

        [SerializeField] private Sprite _icon;
        [SerializeField] private GameObject _projectileVisual;
    }
}
