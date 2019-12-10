using System;
using UnityEditor;
using UniRx;
using UnityEngine;

[CustomEditor(typeof(BuffManagerIMGUI))]
public class BuffManagerIMGUIEditor : Editor
{
    public Texture headerImg;
    private BuffManagerIMGUI buffManager;
    private bool isSpeedBuffActive = false;
    private bool isJumpBuffActive = false;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        buffManager = (BuffManagerIMGUI)target;
        GUILayout.Box(AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Resources/UIElement_Workshop/BuffManager/BuffManager_Header.png"));

        GUILayout.Space(20f);

        GUILayout.Label("Speed Buff");
        GUILayout.Label("Status : " + isSpeedBuffActive);

        if (GUILayout.Button("Add Speed Buff"))
        {
            if (!isSpeedBuffActive)
            {
                var time = 3f;
                buffManager.AddBuff(BuffType.SpeedBuff, time);
                isSpeedBuffActive = true;
                Observable.Timer(TimeSpan.FromSeconds(time)).Subscribe(value =>
                {
                    isSpeedBuffActive = false;
                    this.Repaint();
                });
            }
        }

        GUILayout.Space(20f);

        GUILayout.Label("Jump Boost Buff");
        GUILayout.Label("Status : " + isJumpBuffActive);

        if (GUILayout.Button("Add Speed Buff"))
        {
            if (!isJumpBuffActive)
            {
                var time = 3f;
                buffManager.AddBuff(BuffType.JumpBoostBuff, time);
                isJumpBuffActive = true;
                Observable.Timer(TimeSpan.FromSeconds(time)).Subscribe(value =>
                {
                    isJumpBuffActive = false;
                    this.Repaint();
                });
            }
        }
    }
}
