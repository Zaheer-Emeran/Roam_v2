using UnityEngine;

public class Building_Menu : MonoBehaviour
{
    [SerializeField] private GameObject Build_Menu_UI;
    [SerializeField] private GameObject Core_Buildings_Sub_Menu;
    [SerializeField] private GameObject Villager_Housing_Sub_Menu;
    [SerializeField] private GameObject Villager_Life_Sub_Menu;
    [SerializeField] private GameObject Gathering_Sub_Menu;
    [SerializeField] private GameObject Crafting_Sub_Menu;
    [SerializeField] private GameObject Storage_Sub_Menu;
    [SerializeField] private GameObject Misc_Sub_Menu;
    [SerializeField] private GameObject Defence_Sub_Menu;

    [SerializeField] private GameObject Building_Grid;


    private void Update()
{
    if (Input.GetKeyDown(KeyCode.Tab))
    {
        if (Build_Menu_UI.activeSelf)
            Hide_Menu();   // closes the menu AND all sub menus
        else
            Show_Menu();
    }
}

    public void Open_Sub_Menu(GameObject sub_Menu)
    {
    // Hide every sub menu first, so only one is open at a time
    Core_Buildings_Sub_Menu.SetActive(false);
    Villager_Housing_Sub_Menu.SetActive(false);
    Villager_Life_Sub_Menu.SetActive(false);
    Gathering_Sub_Menu.SetActive(false);
    Crafting_Sub_Menu.SetActive(false);
    Storage_Sub_Menu.SetActive(false);
    Defence_Sub_Menu.SetActive(false);
    Misc_Sub_Menu.SetActive(false);

    // Then show the one that was dragged into the button
    sub_Menu.SetActive(true);
    }


    public void Show_Menu()
    {
        Build_Menu_UI.SetActive(true);
        // Building_Grid.SetActive(true);
    }

    public void Hide_Menu()
    {
        Build_Menu_UI.SetActive(false);
        Core_Buildings_Sub_Menu.SetActive(false);
        Villager_Housing_Sub_Menu.SetActive(false);
        Villager_Life_Sub_Menu.SetActive(false);
        Gathering_Sub_Menu.SetActive(false);
        Crafting_Sub_Menu.SetActive(false);
        Storage_Sub_Menu.SetActive(false);
        Misc_Sub_Menu.SetActive(false);
        Defence_Sub_Menu.SetActive(false);

        // Building_Grid.SetActive(false);

    }
}