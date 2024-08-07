using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceCategory : MonoBehaviour
{
    [SerializeField] private List<GameObject> _buttons;
    [SerializeField] private List<GameObject> _categoriesMenu;

    public static string currentCategory = "BallsCategoryMenu";

    private void Start()
    {
        ActivateCategoryMenu(GameObject.Find(currentCategory));
    }

    public void ActivateCategoryMenu(GameObject choicedCategoryMenu)
    {
        foreach (GameObject categoryMenu in _categoriesMenu)
        {
            if (categoryMenu == choicedCategoryMenu)
            {
                categoryMenu.SetActive(true);
                currentCategory = categoryMenu.name;
            }
            else
            {
                categoryMenu.SetActive(false);
            }
        }
    }

    public void ChangeButtonColor(GameObject choicedButton)
    {
        foreach (GameObject button in _buttons)
        {
            Image image = button.transform.GetChild(0).gameObject.GetComponent<Image>();
            Color buttonColor = button.GetComponent<Image>().color;
            Color imageColor = image.color;

            if (button == choicedButton)
            {
                buttonColor.a = .7f;
                imageColor.a = .6f;
            }
            else
            {
                buttonColor.a = 1f;
                imageColor.a = 1f;
            }

            button.GetComponent<Image>().color = buttonColor;
            image.color = imageColor;
        }
    }
}
