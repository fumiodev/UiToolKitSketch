using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.AddressableAssets;
using System;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SimpleDialogBehaviour : MonoBehaviour
{
	private UIDocument m_UiDocument;

	public async UniTask InitializeAsync(PanelSettings settings, string title, string message)
	{
		// ここもVisualTreeAssetを与えるようにして、各要素の代入もKeyValueなListで与えれば汎用な仕組みに出来そう
		// Memo: UXML の <ui:Template name="XXX" src="project://..." 指定は 
		// Addressableでビルドしたアセットバンドルからも問題なく参照できている
		var treeAsset = await Addressables.LoadAssetAsync<VisualTreeAsset>("Assets/UI Toolkit/UXML/SimpleDialog.uxml");

		// TODO: 表示時のアニメーション

		m_UiDocument = this.gameObject.AddComponent<UIDocument>();
		m_UiDocument.panelSettings = settings;
		m_UiDocument.visualTreeAsset = treeAsset;

		var button = m_UiDocument.rootVisualElement.Q<Button>("OkButton");
		button.RegisterCallback<ClickEvent>(OnOkClick);

		var messageText = m_UiDocument.rootVisualElement.Q<Label>("Message");
		messageText.text = message;

		var titleText = m_UiDocument.rootVisualElement.Q<Label>("Title");
		titleText.text = title;
	}

	private void OnOkClick(ClickEvent evt)
	{
		Debug.Log("On Ok Clicked");

		// TODO: 非表示時のアニメーション
		GameObject.Destroy(this.gameObject);
	}
}
