using UnityEngine;

public class OpenClosePanel : MonoBehaviour
{
    public GameObject pennel;
    
    public bool isOpened = false;

    private void Update()
    {
        if (RobloxStyleController.instance.isCursorEnabled == false)
        {
            if (isOpened == true)
            {
                OpenClose();
            }
        }
    }

    public void OpenClose()
    {
        isOpened = !isOpened;

        if (isOpened)
        {
            pennel.SetActive(true);
        }
        else
        {
            pennel.SetActive(false);
        }
    }
}
