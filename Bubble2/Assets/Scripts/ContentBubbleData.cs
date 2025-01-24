using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBubbleData", menuName = "Bubble/ContentBubbleData", order = 1)]
public class ContentBubbleData : ScriptableObject
{
    [Header("Content Settings")]
    public string bubbleText; // 泡泡的文字内容

    [Header("Values")]
    public int value1; // 数值 1
    public int value2; // 数值 2
    public int value3; // 数值 3
    
    //生成Json文件储存初始data，在退出playmode时将该SO的data revert到初始data
#if UNITY_EDITOR
    [SerializeField] private bool _revert;
    private string _initialJson = string.Empty;
#endif

    private void OnEnable ( )
    {
#if UNITY_EDITOR
        EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
#endif
    }

#if UNITY_EDITOR
    private void OnPlayModeStateChanged ( PlayModeStateChange obj )
    {
        switch ( obj )
        {
            case PlayModeStateChange.EnteredPlayMode:
                _initialJson = EditorJsonUtility.ToJson ( this );
                break;

            case PlayModeStateChange.ExitingPlayMode:
                EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
                if ( _revert )
                    EditorJsonUtility.FromJsonOverwrite ( _initialJson, this );
                break;
        }
    }
#endif
}
