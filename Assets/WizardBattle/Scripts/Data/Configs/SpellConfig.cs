using IT.CoreLib.Interfaces;
using IT.WizardBattle.Game;
using IT.WizardBattle.Interfaces;
using UnityEngine;

namespace IT.WizardBattle.Data
{
    [CreateAssetMenu(fileName = "Spell", menuName = "Wizard Battle/Spell")]
    public class SpellConfig : ScriptableObject, IStaticConfig
    {
        public string Id => _id;
        public int Damage => _damage;
        public IDamageAction DamageAction => _damageAction;
        public float Speed => _speed;
        public float Radius => _radius;

        public Sprite Icon => _icon;
        public Color IconTint => _iconTint;
        public GameObject ProjectileVisual => _projectileVisual;
        public VFX HitVFX => _hitVFX;

        [Header("Data")]
        [SerializeField] private string _id;
        [SerializeField] private int _damage;
        [SerializeField] private DamageActionBase _damageAction;
        [SerializeField] private float _speed;
        [SerializeField] private float _radius;

        [Header("Visuals")]
        [SerializeField] private Sprite _icon;
        [SerializeField] private Color _iconTint = Color.white;
        [SerializeField] private GameObject _projectileVisual;
        [SerializeField] private VFX _hitVFX;
    }
}
