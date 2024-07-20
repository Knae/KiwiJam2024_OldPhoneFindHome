using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameMessageSystem : MonoBehaviour
{
    public struct GameMessageObject
    {
        public GameMessageObject(string speakerName = "Anonymous", string text = "nothing", float lifeTime = 0)
        {
            sourceName = speakerName;
            messageText = text;
            messageLifeTime = lifeTime;
        }

        public string sourceName { get; }
        public string messageText { get; }
        public float messageLifeTime { get; }

        public override string ToString()
        {
            return $"{sourceName}[{messageLifeTime} seconds]: {messageText}";
        }
    }

    public static GameMessageSystem _instance { get; private set; }

    public delegate void MessageHiddenDelegate();
    public static event MessageHiddenDelegate OnMessagePanelHidden;

    [Header("Settings")]
    [SerializeField] private GameObject _messageBoxObject;
    [SerializeField] private GameObject _objectiveBoxObject;
    [SerializeField] private GameObject _messageLog;
    [SerializeField] private GameObject _objectPressE;
    [SerializeField] private TMP_Text _messageLogText;
    [SerializeField] private TMP_Text _speakerNameLabel;
    [SerializeField] private TMP_Text _messageText;
    [SerializeField] private TMP_Text _objectiveText;
   // [SerializeField] private PauseScreen _pauseMenuRef;

    private float _lifeCountDown = 0;
    private bool _externalControl = false;
    private Coroutine _messageCororutine = null;

    #region Monobehaviour
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        //if(_pauseMenuRef==null)
        //{
        //    Debug.LogError("ERR[GameMsgSys]: System missing references to pause menu");
        //}

        if (_speakerNameLabel == null || _messageText == null || _messageBoxObject == null)
        {
            Debug.LogError("ERR[GameMsgSys]: System missing references to text objects");
        }

        if(_messageLog==null || _messageLogText==null)
        {
            Debug.LogError("ERR[GameMsgSys]: System missing references to message log elements");
        }
        else
        {
            _messageLogText.text = string.Empty;
            HideLog();
        }

        if(_objectPressE == null)
        {
            Debug.LogError("ERR[GameMsgSys]: System missing references to \"Press E\" message object");
        }
    }

    // Start is called before the first frame update
    //Hide the message object at the start
    void Start()
    {
        _messageBoxObject.SetActive(false);
        _objectiveBoxObject.SetActive(false);
        _objectPressE.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Pressing any key will hide the message box early. Unless its a manual
        //message box that has to be closed . If the messaageBox is still active,
        //then the coroutine hasn't finished and needs to be closed early
        if (_messageBoxObject.activeSelf && !_externalControl &&
        //!_pauseMenuRef.pauseMenuShown &&
        Input.GetKeyDown(KeyCode.E))
        {
            HideMessagePanel();
        }

        //if(!_pauseMenuRef.pauseMenuShown &&
        //Input.GetKeyDown(KeyCode.P))
        //{
        //    if(_messageLog.activeSelf)
        //    {
        //        HideLog();
        //        SceneReferences.instance.UnpauseGame();
        //    }
        //    else
        //    {
        //        ShowLog();
        //        SceneReferences.instance.PauseGame();
        //    }
        //}


    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }
    #endregion

    #region Messages
    /// <summary>
    /// Display a message that disappear after a set time of if a key is pressed
    /// </summary>
    /// <param name="messageData"></param>
    /// <returns></returns>
    public bool DisplayMessage_Timed(GameMessageObject messageData)
    {
        _objectPressE.SetActive(true);
        _speakerNameLabel.text = messageData.sourceName;
        _messageText.text = messageData.messageText;
        _lifeCountDown = messageData.messageLifeTime;
        _messageCororutine = StartCoroutine(ShowMessageBox(messageData.messageLifeTime));
        AddToLog(messageData.sourceName, messageData.messageText);
        return true;
    }

    public bool DisplayMessage_Timed_Voiced(GameMessageObject messageData)
    {

        return false;
    }

    public bool DisplayMessage_PauseUntilInput(GameMessageObject messageData)
    {
        return false;
    }

    public bool DisplayMessage_PauseVoiced(GameMessageObject messageData)
    {
        return false;
    }

    /// <summary>
    /// Displays a message  that has to be manually closed
    /// by external modules using HideMessagePanel
    /// </summary>
    /// <param name="messageData"></param>
    /// <returns></returns>
    public bool DisplayMessage_Manual(GameMessageObject messageData)
    {
        _objectPressE.SetActive(false);
        _speakerNameLabel.text = messageData.sourceName;
        _messageText.text = messageData.messageText;
        _messageBoxObject.SetActive(true);
        _externalControl = true;
        AddToLog(messageData.sourceName, messageData.messageText);
        return true;
    }

    /// <summary>
    /// Displays a message  that can be manually closed
    /// by external modules using HideMessagePanel and also has 
    /// a lifetime, but cannot be closed through pressing input keys
    /// </summary>
    /// <param name="messageData"></param>
    /// <returns></returns>
    public bool DisplayMessage_ManualTimed(GameMessageObject messageData)
    {
        _objectPressE.SetActive(false);
        _speakerNameLabel.text = messageData.sourceName;
        _messageText.text = messageData.messageText;
        _lifeCountDown = messageData.messageLifeTime;
        _messageCororutine = StartCoroutine(ShowMessageBox(messageData.messageLifeTime));
        _externalControl = true;
        AddToLog(messageData.sourceName, messageData.messageText);
        return true;
    }

    /// <summary>
    /// Hides the whold panel and triggers the OnMessagePanelHidden event
    /// TODO:Have this publicly accessible? Make another specifically for public use
    /// with checks?
    /// </summary>
    public void HideMessagePanel()
    {
        //Set lifedown to less than 0 so that 
        //it doesn't trigger the countdown checks
        //at update
        _lifeCountDown = -1;
        _messageBoxObject.SetActive(false);
        _objectPressE.SetActive(false);
        _externalControl = false;

        if (_messageCororutine!= null)
        {
            StopCoroutine(_messageCororutine);
            _messageCororutine = null; 
        }

        OnMessagePanelHidden?.Invoke();
    }

    IEnumerator ShowMessageBox(float time)
    {
        _messageBoxObject.SetActive(true);
        //TODO:: Unscaled time to ignore timescale,
        //so will need to check how lag spikes will 
        //affect it
        yield return new WaitForSecondsRealtime(time);
        HideMessagePanel();
        yield return null;
    }
    #endregion

    #region Objectives
    public bool DisplayObjectives(string text)
    {
        _objectiveText.text = text;
        if(!_objectiveBoxObject.activeSelf)
        {
            _objectiveBoxObject.SetActive(true);
        }
        return true;
    }

    public void ClearAndHideObjectives()
    {
        _objectiveText.text = string.Empty;
        if (_objectiveBoxObject.activeSelf)
        {
            _objectiveBoxObject.SetActive(false); 
        }
    }
    #endregion

    #region MessageLog
    public void ShowLog()
    {
        _messageLog?.SetActive(true);
    }

    public void HideLog()
    {
        _messageLog?.SetActive(false);
    }

    /// <summary>
    /// Add an message entry to the message log
    /// </summary>
    /// <param name="speaker">Name of the speaker</param>
    /// <param name="message">The message content</param>
    private void AddToLog(string speaker, string message)
    {
        if (_messageLogText != null)
        {
            string textToAdd = $"\n<b>[{speaker}]</b>\n{message}\n";
            _messageLogText.text += textToAdd; 
        }
    }
    #endregion
}
