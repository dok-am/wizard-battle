using IT.WizardBattle.Data;
using UnityEngine;
using UnityEngine.UI;

namespace IT.WizardBattle.UI
{
    public class UISpellsView : MonoBehaviour
    {
        [SerializeField] private Image _selectedSpellIcon;

        public void SelectSpell(SpellData spell)
        {
            _selectedSpellIcon.sprite = spell.Icon;
            _selectedSpellIcon.color = spell.IconTint;
        }
    }
}