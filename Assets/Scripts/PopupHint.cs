using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PopupHint : MonoBehaviour
{
    [Header("Hint Setting")]
    [SerializeField] private string _hintText;
    [SerializeField] private TMP_Text _textDisplay;
    [SerializeField] private GameObject _textBaseRect;
    [SerializeField] private bool _enabled = true;

    #region Monobehaviour
    // Start is called before the first frame update
    void Start()
    {
        //Check if hints are enabled
        CheckSettings();

        _textDisplay.text = _hintText;
        _textBaseRect.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        //PlayerSettings.OnSettingsUpdated += CheckSettings;
    }

    private void OnDisable()
    {
        //PlayerSettings.OnSettingsUpdated -= CheckSettings;
    }
    #endregion

    public void ToggleHint()
    {
        if (_enabled)
        {
            bool isActive = _textBaseRect.activeSelf;
            _textBaseRect.SetActive(!isActive); 
        }
    }

    private void CheckSettings()
    {
        //_enabled = PlayerSettings.instance.GetIfShowHints();
        gameObject.SetActive(_enabled);
    }
}