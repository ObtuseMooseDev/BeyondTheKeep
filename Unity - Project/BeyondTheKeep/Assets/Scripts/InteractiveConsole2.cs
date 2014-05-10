using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class InteractiveConsole2 : MonoBehaviour
{
	public Texture2D fbLogin;
	public Texture2D fbLogout;
	public Texture2D fbScreenshot;
	public GUIStyle labelStyle;
	
	#region FB.Init() example
	
	private bool isInit = true;
	
	private void OnInitComplete()
	{
		Debug.Log("FB.Init completed: Is user logged in? " + FB.IsLoggedIn);
		isInit = true;
	}
	
	private void OnHideUnity(bool isGameShown)
	{
		Debug.Log("Is game showing? " + isGameShown);
	}
	
	
	void Start(){
		FB.Init(OnInitComplete, OnHideUnity);
		labelStyle = GUI.skin.label;
	}
	
	
	#endregion
	
	#region FB.Login() example
	
	private void CallFBLogin()
	{
		FB.Login("email,publish_actions");
	}
	
	private void CallFBLogout()
	{
		FB.Logout();
	}
	#endregion
	
	
	#region FB.AppRequest() Friend Selector
	
	public string FriendSelectorTitle = "";
	public string FriendSelectorMessage = "Derp";
	public string FriendSelectorFilters = "[\"all\",\"app_users\",\"app_non_users\"]";
	public string FriendSelectorData = "{}";
	public string FriendSelectorExcludeIds = "";
	public string FriendSelectorMax = "";
	
	private void CallAppRequestAsFriendSelector()
	{
		// If there's a Max Recipients specified, include it
		int? maxRecipients = null;
		if (FriendSelectorMax != "")
		{
			try
			{
				maxRecipients = Int32.Parse(FriendSelectorMax);
			}
			catch (Exception e)
			{
				//status = e.Message;
			}
		}
		
		// include the exclude ids
		string[] excludeIds = (FriendSelectorExcludeIds == "") ? null : FriendSelectorExcludeIds.Split(',');
		
		FB.AppRequest(
			message: FriendSelectorMessage,
			filters: FriendSelectorFilters,
			excludeIds: excludeIds,
			maxRecipients: maxRecipients,
			data: FriendSelectorData,
			title: FriendSelectorTitle,
			callback: Callback
			);
	}
	#endregion
	
	#region FB.AppRequest() Direct Request
	
	public string DirectRequestTitle = "";
	public string DirectRequestMessage = "Herp";
	private string DirectRequestTo = "";
	
	private void CallAppRequestAsDirectRequest()
	{
		if (DirectRequestTo == "")
		{
			throw new ArgumentException("\"To Comma Ids\" must be specificed", "to");
		}
		FB.AppRequest(
			message: DirectRequestMessage,
			to: DirectRequestTo.Split(','),
			title: DirectRequestTitle,
			callback: Callback
			);
	}
	
	#endregion
	
	#region FB.Feed() example
	
	public string FeedToId = "";
	public string FeedLink = "";
	public string FeedLinkName = "";
	public string FeedLinkCaption = "";
	public string FeedLinkDescription = "";
	public string FeedPicture = "";
	public string FeedMediaSource = "";
	public string FeedActionName = "";
	public string FeedActionLink = "";
	public string FeedReference = "";
	public bool IncludeFeedProperties = false;
	private Dictionary<string, string[]> FeedProperties = new Dictionary<string, string[]>();
	
	private void CallFBFeed()
	{
		Dictionary<string, string[]> feedProperties = null;
		if (IncludeFeedProperties)
		{
			feedProperties = FeedProperties;
		}
		FB.Feed(
			toId: FeedToId,
			link: FeedLink,
			linkName: FeedLinkName,
			linkCaption: FeedLinkCaption,
			linkDescription: FeedLinkDescription,
			picture: FeedPicture,
			mediaSource: FeedMediaSource,
			actionName: FeedActionName,
			actionLink: FeedActionLink,
			reference: FeedReference,
			properties: feedProperties,
			callback: Callback
			);
	}
	
	#endregion
	
	
	#region FB.API() example
	
	public string ApiQuery = "";
	
	private void CallFBAPI()
	{
		FB.API(ApiQuery, Facebook.HttpMethod.GET, Callback);
	}
	
	#endregion
	
	
	#region FB.Canvas.SetResolution example
	
	public string Width = "800";
	public string Height = "600";
	public bool CenterHorizontal = true;
	public bool CenterVertical = false;
	public string Top = "10";
	public string Left = "10";
	
	public void CallCanvasSetResolution()
	{
		int width;
		if (!Int32.TryParse(Width, out width))
		{
			width = 800;
		}
		int height;
		if (!Int32.TryParse(Height, out height))
		{
			height = 600;
		}
		float top;
		if (!float.TryParse(Top, out top))
		{
			top = 0.0f;
		}
		float left;
		if (!float.TryParse(Left, out left))
		{
			left = 0.0f;
		}
		if (CenterHorizontal && CenterVertical)
		{
			FB.Canvas.SetResolution(width, height, false, 0, FBScreen.CenterVertical(), FBScreen.CenterHorizontal());
		} 
		else if (CenterHorizontal) 
		{
			FB.Canvas.SetResolution(width, height, false, 0, FBScreen.Top(top), FBScreen.CenterHorizontal());
		} 
		else if (CenterVertical)
		{
			FB.Canvas.SetResolution(width, height, false, 0, FBScreen.CenterVertical(), FBScreen.Left(left));
		}
		else
		{
			FB.Canvas.SetResolution(width, height, false, 0, FBScreen.Top(top), FBScreen.Left(left));
		}
	}
	
	#endregion
	
	#region GUI
	
	//private string status = "Ready";
	
	//private string lastResponse = "";
	public GUIStyle textStyle = new GUIStyle();
	//private Texture2D lastResponseTexture;
	
	private Vector2 scrollPosition = Vector2.zero;
	#if UNITY_IOS || UNITY_ANDROID || UNITY_WP8
	int buttonHeight = 60;
	int mainWindowWidth = Screen.width - 30;
	int mainWindowFullWidth = Screen.width;
	#else
	
	#endif
	
	private int TextWindowHeight
	{
		get
		{
			#if UNITY_IOS || UNITY_ANDROID || UNITY_WP8
			return IsHorizontalLayout() ? Screen.height : 85;
			#else
			return Screen.height;
			#endif
		}
	}
	
	
	void Awake()
	{
		textStyle.alignment = TextAnchor.UpperLeft;
		textStyle.wordWrap = true;
		textStyle.padding = new RectOffset(10, 10, 10, 10);
		textStyle.stretchHeight = true;
		textStyle.stretchWidth = false;
		
		FeedProperties.Add("key1", new[] { "valueString1" });
		FeedProperties.Add("key2", new[] { "valueString2", "http://www.facebook.com" });
	}
	
	
	void OnGUI()
	{ 
		DontDestroyOnLoad (this);
		if (IsHorizontalLayout())
		{
			GUILayout.BeginHorizontal();
			GUILayout.BeginVertical();
		}
		//GUILayout.Space(5);
		//GUILayout.Box("Status: " + status, GUILayout.MinWidth(mainWindowWidth));
		
		#if UNITY_IOS || UNITY_ANDROID || UNITY_WP8
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
		{
			scrollPosition.y += Input.GetTouch(0).deltaPosition.y;
		}
		#endif
		
		//scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.MinWidth(mainWindowFullWidth));
		GUILayout.BeginVertical();
		
		GUILayout.BeginHorizontal();
		
		GUI.enabled = isInit;
		if (Application.loadedLevel == 0) {
			if (GUI.Button(new Rect(10, 10, 100, 100), fbLogin, labelStyle)) {  
				//FB.Init(OnInitComplete, OnHideUnity);
				CallFBLogin ();
				//status = "Login called";
			}
			
		}
		
		#if UNITY_IOS || UNITY_ANDROID || UNITY_WP8
		GUI.enabled = FB.IsLoggedIn;
		if (Application.loadedLevel == 0) {
			if (GUI.Button(new Rect(110, 10, 100, 100), fbLogout, labelStyle))
			{
				CallFBLogout();
				//status = "Logout called";
			}
		}
		GUI.enabled = isInit;
		#endif
		GUILayout.EndHorizontal();
		
		//#if UNITY_IOS || UNITY_ANDROID
		//if (Button("Publish Install"))
		//{
		//	CallFBPublishInstall();
		//status = "Install Published";
		//}
		//#endif
		
		GUI.enabled = FB.IsLoggedIn;
		//GUILayout.Space(10);
		//LabelAndTextField("Title (optional): ", ref FriendSelectorTitle);
		//LabelAndTextField("Message: ", ref FriendSelectorMessage);
		//LabelAndTextField("Exclude Ids (optional): ", ref FriendSelectorExcludeIds);
		//LabelAndTextField("Filters (optional): ", ref FriendSelectorFilters);
		//LabelAndTextField("Max Recipients (optional): ", ref FriendSelectorMax);
		//LabelAndTextField("Data (optional): ", ref FriendSelectorData);
		
		
		
		GUILayout.Space(10);
		//LabelAndTextField("Title (optional): ", ref DirectRequestTitle);
		//LabelAndTextField("Message: ", ref DirectRequestMessage);
		//LabelAndTextField("To Comma Ids: ", ref DirectRequestTo);
		//if (Button("Open Direct Request"))
		//{
		//	try
		//	{
		//		CallAppRequestAsDirectRequest();
		//		status = "Direct Request called";
		//	}
		//	catch (Exception e)
		//	{
		//		status = e.Message;
		//	}
		//}
		//GUILayout.Space(10);
		//LabelAndTextField("To Id (optional): ", ref FeedToId);
		//LabelAndTextField("Link (optional): ", ref FeedLink);
		//LabelAndTextField("Link Name (optional): ", ref FeedLinkName);
		//LabelAndTextField("Link Desc (optional): ", ref FeedLinkDescription);
		//LabelAndTextField("Link Caption (optional): ", ref FeedLinkCaption);
		//LabelAndTextField("Picture (optional): ", ref FeedPicture);
		//LabelAndTextField("Media Source (optional): ", ref FeedMediaSource);
		//LabelAndTextField("Action Name (optional): ", ref FeedActionName);
		//LabelAndTextField("Action Link (optional): ", ref FeedActionLink);
		//LabelAndTextField("Reference (optional): ", ref FeedReference);
		//GUILayout.BeginHorizontal();
		//GUILayout.Label("Properties (optional)", GUILayout.Width(150));
		//IncludeFeedProperties = GUILayout.Toggle(IncludeFeedProperties, "Include");
		//GUILayout.EndHorizontal();
		//if (Button("Open Feed Dialog"))
		//{
		//	try
		//	{
		//		CallFBFeed();
		//		status = "Feed dialog called";
		//	}
		//	catch (Exception e)
		//	{
		//		status = e.Message;
		//	}
		//}
		//GUILayout.Space(10);
		
		#if UNITY_WEBPLAYER
		LabelAndTextField("Product: ", ref PayProduct);
		if (Button("Call Pay"))
		{
			CallFBPay();
		}
		GUILayout.Space(10);
		#endif
		
		//LabelAndTextField("API: ", ref ApiQuery);
		//if (Button("Call API"))
		//{
		//	status = "API called";
		//	CallFBAPI();
		//}
		//GUILayout.Space(10);
		if( Application.loadedLevel == 1 ){
			if (GUI.Button(new Rect(0, 25, 65, 65), fbScreenshot, labelStyle))
			{
				//status = "Take screenshot";
				
				StartCoroutine(TakeScreenshot());
			}
		}
		
		//if (Button("Get Deep Link"))
		//{
		//	CallFBGetDeepLink();
		//}
		#if UNITY_IOS || UNITY_ANDROID
		//if (Button("Log FB App Event"))
		//{
		//	status = "Logged FB.AppEvent";
		//	CallAppEventLogEvent();
		//}
		#endif
		
		#if UNITY_WEBPLAYER
		GUILayout.Space(10);
		
		LabelAndTextField("Game Width: ", ref Width);
		LabelAndTextField("Game Height: ", ref Height);
		GUILayout.BeginHorizontal();
		GUILayout.Label("Center Game:", GUILayout.Width(150));
		CenterVertical = GUILayout.Toggle(CenterVertical, "Vertically");
		CenterHorizontal = GUILayout.Toggle(CenterHorizontal, "Horizontally");
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal();
		LabelAndTextField("or set Padding Top: ", ref Top);
		LabelAndTextField("set Padding Left: ", ref Left);
		GUILayout.EndHorizontal();
		if (Button("Set Resolution"))
		{
			status = "Set to new Resolution";
			CallCanvasSetResolution();
		}
		#endif
		
		//GUILayout.Space(10);
		
		GUILayout.EndVertical();
		GUILayout.EndScrollView();
		
		//if (IsHorizontalLayout())
		//{
		//	GUILayout.EndVertical();
		//}
		//GUI.enabled = true;
		
		//var textAreaSize = GUILayoutUtility.GetRect(640, TextWindowHeight);
		
		//GUI.TextArea(
		//	textAreaSize,
		//	string.Format(
		//	" AppId: {0} \n Facebook Dll: {1} \n UserId: {2}\n IsLoggedIn: {3}\n AccessToken: {4}\n AccessTokenExpiresAt: {5}\n {6}",
		//	FB.AppId,
		//	(isInit) ? "Loaded Successfully" : "Not Loaded",
		//	FB.UserId,
		//	FB.IsLoggedIn,
		//	FB.AccessToken,
		//	FB.AccessTokenExpiresAt,
		//	lastResponse
		//	), textStyle);
		
		//if (lastResponseTexture != null)
		//{
		//	var texHeight = textAreaSize.y + 200;
		//	if (Screen.height - lastResponseTexture.height < texHeight)
		//	{
		//		texHeight = Screen.height - lastResponseTexture.height;
		//	}
		//		GUI.Label(new Rect(textAreaSize.x + 5, texHeight, lastResponseTexture.width, lastResponseTexture.height), lastResponseTexture);
		//}
		
		if (IsHorizontalLayout())
		{
			GUILayout.EndHorizontal();
		}
	}
	
	void Callback(FBResult result)
	{
		
	}
	
	private IEnumerator TakeScreenshot() 
	{
		yield return new WaitForEndOfFrame();
		
		var width = Screen.width;
		var height = Screen.height;
		var tex = new Texture2D(width, height, TextureFormat.RGB24, false);
		// Read screen contents into the texture
		tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
		tex.Apply();
		byte[] screenshot = tex.EncodeToPNG();
		
		var wwwForm = new WWWForm();
		wwwForm.AddBinaryData("image", screenshot, "InteractiveConsole.png");
		wwwForm.AddField("message", "Beyond The Keep, coming soon to Android and iOS");
		
		FB.API("me/photos", Facebook.HttpMethod.POST, Callback, wwwForm);
	}
	
	
	
	private void LabelAndTextField(string label, ref string text)
	{
		GUILayout.BeginHorizontal();
		GUILayout.Label(label, GUILayout.MaxWidth(150));
		text = GUILayout.TextField(text);
		GUILayout.EndHorizontal();
	}
	
	private bool IsHorizontalLayout()
	{
		#if UNITY_IOS || UNITY_ANDROID || UNITY_WP8
		return Screen.orientation == ScreenOrientation.Landscape;
		#else
		return true;
		#endif
	}
	
	#endregion
}
