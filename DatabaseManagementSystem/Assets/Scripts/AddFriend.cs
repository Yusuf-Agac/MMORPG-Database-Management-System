using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using WWWForm = UnityEngine.WWWForm;

public class AddFriend : MonoBehaviour
{
    private DBManager _dbManager;
    private TMP_InputField _friendIF;
    private FriendList _friendList;
    private PlayerInfo _playerInfo;

    private void Start()
    {
        _dbManager = GetComponent<DBManager>();
        _playerInfo = GetComponent<PlayerInfo>();
        _friendList = GetComponent<FriendList>();
        _friendIF = transform.Find("InGame").Find("FriendsView").Find("UsernameFriend").GetComponent<TMP_InputField>();
    }

    public void AddFriendFunc()
    {
        StartCoroutine(AddFriendCo(_friendIF.text));
    }

    IEnumerator AddFriendCo(string FriendUsername)
    {
        Debug.Log("Username: " + FriendUsername + " ID: " + _playerInfo.ID);
        WWWForm form = new WWWForm();
        form.AddField("FriendUsername", FriendUsername);
        form.AddField("ID", _playerInfo.ID);
        
        UnityWebRequest req = UnityWebRequest.Post("http://localhost/sqlconnect/addFriend.php", form);
        req.downloadHandler = new DownloadHandlerBuffer();
        yield return req.SendWebRequest();
        
        if (req.downloadHandler.text != "400")
        {
            Debug.Log("Add Friend Request: " + req.downloadHandler.text);
            var reqResult = req.downloadHandler.text.Split('/');
            _friendList.LoadFriendToUI(int.Parse(reqResult[0]), FriendUsername, reqResult[1]);
            _friendList.AddToFriendList(int.Parse(reqResult[0]), FriendUsername, reqResult[1]);
            Debug.Log("AddFriend successfully");
        }
        else
        {
            Debug.LogWarning("AddFriend failed: # " + req.downloadHandler.text);
        }
    }
}
