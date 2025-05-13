using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToogleSkillTree : MonoBehaviour
{
    public CanvasGroup skillsCanvas;
    private bool skillTreeOpen = false;

    private void Start()
    {
        skillsCanvas.alpha = 0;
        skillsCanvas.blocksRaycasts = false;
        skillTreeOpen = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("ToggleSkillTree"))
        {
            if (skillTreeOpen)
            {
                Time.timeScale = 1;
                skillsCanvas.alpha = 0;
                skillsCanvas.blocksRaycasts = false;
                skillTreeOpen = false;
            }
            else
            {
                Time.timeScale = 0;
                skillsCanvas.alpha = 1;
                skillsCanvas.blocksRaycasts = true;
                skillTreeOpen = true;
            }
        }
    }
}
