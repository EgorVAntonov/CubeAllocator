using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

interface IPauseInteractor
{
    void SetPause();
    void SetUnpause();
}

public class PauseBlocker : MonoBehaviour
{
    [SerializeField] private GameObject blocker;
    public bool pauseMode = false;

    private void Start()
    {
        blocker.SetActive(false);
    }

    public void SwitchPauseMode()
    {
        pauseMode = !pauseMode;
        blocker.SetActive(pauseMode);

        if (pauseMode)
        {
            Debug.Log("set pause");
            SetPause();
        }
        else
        {
            Debug.Log("unpause");
            SetUnpause();
        }
        
    }

    private void SetPause()
    {

        foreach (Square square in FindObjectsOfType<Square>())
        {
            square.SetPause();
            square.GetComponent<BucketInteractor>().SetPause();
        }

        foreach (Bucket bucket in FindObjectsOfType<Bucket>())
        {
            bucket.SetPause();
        }
    }

    private void SetUnpause()
    {

        foreach (Square square in FindObjectsOfType<Square>())
        {
            square.SetUnpause();
            square.GetComponent<BucketInteractor>().SetUnpause();
        }

        foreach (Bucket bucket in FindObjectsOfType<Bucket>())
        {
            bucket.SetUnpause();
        }
    }
    
}
