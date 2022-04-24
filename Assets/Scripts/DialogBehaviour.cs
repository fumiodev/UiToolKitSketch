using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogBehaviour : MonoBehaviour
{
	[SerializeField] private UIDocument m_UiDocument;
	[SerializeField] private string title = "Title";
	[SerializeField] private string message = "Message";


	void Start()
	{
		// TODO: そもそものInstantiateとOKクリック後のDestory
		// TODO: 表示/非表示時のアニメーション
		var button = m_UiDocument.rootVisualElement.Q<Button>("OkButton");
		button.clickable.clicked += () =>
		{
			Debug.Log("On Ok Clicked");
		};

		var messageText = m_UiDocument.rootVisualElement.Q<Label>("Message");
		messageText.text = message;

		var titleText = m_UiDocument.rootVisualElement.Q<Label>("Title");
		titleText.text = title;
	}

}
