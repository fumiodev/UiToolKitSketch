using UnityEngine;
using UnityEngine.UIElements;
using UniRx;
using UniRx.Triggers;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

public class DialogOpener : MonoBehaviour
{
	// [SerializeField] private UIDocument m_BackGroundUiDocument;
	[SerializeField] private AssetReference m_PanelSettingsRef;
	[SerializeField] private string title = "Title";
	[SerializeField, TextArea] private string message = "Message";

	private GameObject dialogObj;
	private PanelSettings m_PanelSettings;


	private void Start()
	{
		this.UpdateAsObservable().Where(_ => Input.anyKeyDown)
			.Subscribe(_ => OpenDialogAsync().Forget());
	}

	private void OnDestroy()
	{
		m_PanelSettingsRef.ReleaseAsset();
	}

	private async UniTask OpenDialogAsync()
	{
		if(dialogObj)
		{
			return;
		}
		dialogObj = new GameObject("SimpleDialog");
		dialogObj.transform.SetParent(this.gameObject.transform.parent);
		var simpleDialogBehavior = dialogObj.AddComponent<SimpleDialogBehaviour>();
		if(m_PanelSettings == null)
		{	
			m_PanelSettings = await m_PanelSettingsRef.LoadAssetAsync<PanelSettings>();
		}
		Debug.Assert(simpleDialogBehavior != null);
		simpleDialogBehavior.InitializeAsync(m_PanelSettings, title, message).Forget();

		// FIXME:
		// ダイアログを閉じた後に、m_BackGroundUiDocument の VisualElementsをクリックすると
		// "FocusController has unprocessed focus events. Clearing."
		// が出るので解消しておく。
	}
}
