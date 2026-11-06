using System.Text;
using TMPro;
using UnityEngine;

public class AbilityHUDController : MonoBehaviour
{
    public PlayerInventory inventory;
    public TextMeshProUGUI abilityText;
    
    private void Start()
    {
        Refresh();
    }

    private void Update()
    {
        
    }

    private void OnEnable()
    {
        inventory.changed += Refresh;
        Refresh();
    }

    private void OnDisable()
    {
        inventory.changed -= Refresh;
    }

    private void Refresh()
    {
        var sb = new StringBuilder();
        for (int i = 0; i < PlayerInventory.slots.Length; i++)
        {
            AbilityType ability = PlayerInventory.slots[i];
            if (inventory.HasAbility(ability))
            {
                string line = i == inventory.selectedIndex ? $"<b><u>{i + 1}:{ability}</u></b> " : $"{i + 1}:{ability} ";
                sb.Append(line);
            }
        }
        abilityText.text = sb.ToString();
    }
}
