using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UniRx;

[CustomEditor(typeof(BuffManagerUIElementFinished))]
public class BuffManagerUIElementsFinishedEditor : Editor
{
    private BuffManagerUIElementFinished buffManager;
    private VisualElement rootElement;
    private VisualTreeAsset visualTree;

    private Label speedBuffActiveText;
    private Label jumpBuffActiveText;
    private void OnEnable()
    {
        buffManager = (BuffManagerUIElementFinished)target;

        rootElement = new VisualElement();
        visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
            "Assets/_UIElement_Workshop/Scripts/Buff/Manager/Finished_UIElement_Editor/Editor/BuffManagerUIElementFinishedEditorTemplateV2.uxml"
        );

        StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(
            "Assets/_UIElement_Workshop/Scripts/Buff/Manager/Finished_UIElement_Editor/Editor/BuffManagerUIElementFinishedEditorStyleV2.uss"
        );

        Debug.Log("hello");

        rootElement.styleSheets.Add(styleSheet);
    }

    public override VisualElement CreateInspectorGUI()
    {
        Debug.Log(buffManager.player);

        rootElement.Clear();
        visualTree.CloneTree(rootElement);

        SetHeaderImage();
        SetDescription();
        CreatePlayerPropertyField();
        SetBuffImage();
        GetBuffActiveText();
        SetBuffButton();

        return rootElement;
    }

    private void SetHeaderImage()
    {
        var headerImageElement = rootElement.Q("header");
        headerImageElement.style.backgroundImage = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Resources/UIElement_Workshop/BuffManager/BuffManager_Header.png");
    }

    private void SetDescription()
    {
        Label descriptionElement = rootElement.Q("description") as Label;
        descriptionElement.text = "A Wonderful Buff Manager \n Working in Wonderful UIElements Workshop";
    }

    private void CreatePlayerPropertyField()
    {
        var objectField = rootElement.Q<ObjectField>("player-field");
        objectField.objectType = typeof(Player);
        objectField.RegisterCallback<ChangeEvent<UnityEngine.Object>>(evt =>
        {
            buffManager.player = evt.newValue as Player;
        });
    }

    private void SetBuffImage()
    {
        var speedBuffImageElement = rootElement.Q("speed-buff-image");
        speedBuffImageElement.style.backgroundImage = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Resources/UIElement_Workshop/BuffManager/Buff/Ability-Icon.png");

        var jumpBuffImageElement = rootElement.Q("jump-buff-image");
        jumpBuffImageElement.style.backgroundImage = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Resources/UIElement_Workshop/BuffManager/Buff/Wing-Icon.png");
    }

    private void GetBuffActiveText()
    {
        speedBuffActiveText = rootElement.Q("speed-buff-activate-text") as Label;
        jumpBuffActiveText = rootElement.Q("jump-buff-activate-text") as Label;
    }

    private void SetBuffButton()
    {
        var speedBuffButton = rootElement.Q("speedbuff-button") as Button;
        var jumpBuffButton = rootElement.Q("jumpbuff-button") as Button;

        speedBuffButton.clickable.clicked += () =>
        {
            addSpeedBuff();
        };
        jumpBuffButton.clickable.clicked += () =>
        {
            addJumpBuff();
        };
    }

    private void addSpeedBuff()
    {
        var time = 3f;
        speedBuffActiveText.text = "Activate : true";
        buffManager.AddBuff(BuffType.SpeedBuff, time);
        Observable.Timer(TimeSpan.FromSeconds(time)).Subscribe(_ =>
        {
            speedBuffActiveText.text = "Activate : false";
        });
    }

    private void addJumpBuff()
    {
        var time = 3f;
        jumpBuffActiveText.text = "Activate : true";
        buffManager.AddBuff(BuffType.JumpBoostBuff, time);
        Observable.Timer(TimeSpan.FromSeconds(time)).Subscribe(_ =>
        {
            jumpBuffActiveText.text = "Activate : false";
        });
    }
}
