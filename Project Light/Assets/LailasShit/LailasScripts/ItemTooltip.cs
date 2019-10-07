using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ItemTooltip : MonoBehaviour
{
    [SerializeField] Text ItemNameText;
    [SerializeField] Text ItemSlotText;
    [SerializeField] Text ItemStatsText;

    private StringBuilder sb = new StringBuilder();

    public void ShowToolTip(EquippableItem item)
    {
        ItemNameText.text = item.name;
        ItemSlotText.text = item.EquipmentType.ToString();
        sb.Length = 0;
        AddStat(item.Damage, "Damage");
        AddStat(item.Defense, "Defense");
        ItemStatsText.text = sb.ToString();

        gameObject.SetActive(true);

    }
    public void HideToolTip()
    {
        gameObject.SetActive(false);
    }
    private void AddStat(float value, string statName)
    {
        if(value != 0)
        {
            if(sb.Length > 0)
            {
                sb.AppendLine();
            }
            if(value > 0)
            {
                sb.Append(" + ");
            }
            sb.Append(value);
            sb.Append(" ");
            sb.Append(statName);
        }
    }
}
