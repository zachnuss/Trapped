using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bugReport : MonoBehaviour
{
    //Function to open the bug report form for people to fill in any bugs they come across
    public void openLink()
    {
        Application.OpenURL("https://forms.gle/o16qGudCkgj947CZ6");
    }
}
