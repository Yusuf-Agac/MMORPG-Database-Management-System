using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class FriendList : MonoBehaviour
{
    [System.Serializable]
    public class Friend
    {
        public string username;
        public int friendID;
        public string profilePicture;

        public Friend(int friendID, string username, string profilePicture)
        {
            this.friendID = friendID;
            this.username = username;
            this.profilePicture = profilePicture;
        }
    }
    
    private ArrayList _friends;
    private Transform _content;
    private PlayerInfo _playerInfo;

    private void Start()
    {
        _friends = new ArrayList();
        _content = transform.Find("InGame").Find("FriendsView").Find("Viewport").Find("Content");
        _playerInfo = GetComponent<PlayerInfo>();
    }

    public void AddToFriendList(int FriendID, string Username, string ProfilePicture)
    {
        var newFriend = new Friend(FriendID, Username, ProfilePicture);
        _friends.Add(newFriend);
    }
    
    public void LoadAllFriendsToUI()
    {
        foreach (Friend friend in _friends)
        {
            if (friend != null)
            {
                LoadFriendToUI(friend.friendID, friend.username, friend.profilePicture);
            }
        }
    }
    public void LoadFriendToUI(int friendID, string username, string profilePicture)
    {
        GameObject temp = Instantiate(Resources.Load<GameObject>("Prefabs/Friend"), _content);
        temp.transform.GetChild(0).GetComponent<TMP_Text>().text = username;
        temp.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Avatars/" + profilePicture);
        temp.transform.GetChild(2).GetComponent<TMP_Text>().text = friendID.ToString();
    }
    
    public void LoadFriendList()
    {
        StartCoroutine(LoadFriendListCo());
    }
    
    IEnumerator LoadFriendListCo()
    {
        WWWForm form = new WWWForm();
        form.AddField("ID", _playerInfo.ID);
        
        UnityWebRequest req = UnityWebRequest.Post("http://localhost/sqlconnect/GetFriendList.php", form);
        
        yield return req.SendWebRequest();

        
        if (req.downloadHandler.text != "400")
        {
            var friendListResult = req.downloadHandler.text.Split('/');
            foreach (var friendResult in friendListResult)
            {
                var friend = friendResult.Split(',');
                var result = int.TryParse(friend[0], out var friendID);
                if(result)
                {
                    Debug.Log("ProfilePicture: " + friend[2]);
                    AddToFriendList(friendID, friend[1], friend[2]);
                }
                
            }
            LoadAllFriendsToUI();
            Debug.Log("FriendList loaded successfully");
        }
        else
        {
            Debug.LogWarning("FriendList loading failed: # " + req.downloadHandler.text);
        }
    }
}
