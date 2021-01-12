using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogSysX : MonoBehaviour
{
    public StringListProviderX Provider;
    public StringPropertyDisplay Output;
    public int CharsPerSecond = 5;

    private int _currString = 0;
    private float _startTypigTick;
    private int _currChar;

    private bool _isTyping;
    private IReadOnlyList<string> _strings;

    public UnityEvent OnType;
    public UnityEvent OnTypingCompleted;

    // Start is called before the first frame update
    void Start()
    {
        _strings = Provider.GetTranslatedStringList();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isTyping) return;

        if (_currChar == _strings[_currString].Length)
        {
            _isTyping = false;
            OnTypingCompleted.Invoke();
            return;
        }

        if (Time.time > _startTypigTick + _currChar * 1.0f / CharsPerSecond)
        {
            Output.SetItemToDisplay(_strings[_currString].Substring(0, _currChar));
            OnType?.Invoke();
            ++_currChar;
        }
    }

    public void MoveNext()
    {
        _currString = (int) Mathf.PingPong(_currString + 1, _strings.Count - 1);
    }

    public void StartTyping()
    {
        _isTyping = true;
        _currChar = 0;
        _startTypigTick = Time.time;
    }
}