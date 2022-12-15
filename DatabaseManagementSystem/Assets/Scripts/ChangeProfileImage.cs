using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeProfileImage : MonoBehaviour
{
    private Image _profileImage;
    private TMP_Dropdown _selectedImage;
    private PlayerInfo _playerInfo;
    private DBManager _dbManager;

    private void Start()
    {
        _profileImage = GameObject.Find("Canvas").transform.Find("InGame").Find("CharacterProfile").Find("ImageWrap").Find("UserPic").GetComponent<Image>();
        _selectedImage = GameObject.Find("Canvas").transform.Find("InGame").Find("CharacterProfile").Find("DropdownProfile").GetComponent<TMP_Dropdown>();
        _playerInfo = GetComponent<PlayerInfo>();
        _dbManager = GetComponent<DBManager>();
    }

    public void ChangeProfilePicture()
    {
        Debug.Log("Selected Image -> " + _selectedImage.options[_selectedImage.value].image);
        _profileImage.sprite = _selectedImage.options[_selectedImage.value].image;
        _playerInfo.ProfilePicture = _profileImage.sprite.name;
        StartCoroutine(_dbManager.UpdateProfilePictureCo());
    }
    
    public void UpdateProfilePicture()
    {
        _profileImage.sprite = Resources.Load<Sprite>("Avatars/" + _playerInfo.ProfilePicture);
    }
}
