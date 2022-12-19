<?php
    $con = mysqli_connect('localhost','root','root','unitydatabase');

    if(mysqli_connect_errno()){
        echo "400";
        exit();
    }

    $FriendUsername = $_POST["FriendUsername"];
    $ID = $_POST["ID"];

    // Get the friend's ID
    $CheckFriendQuery = "SELECT ID FROM players WHERE Username = '" . $FriendUsername . "';";

    $Result = mysqli_query($con, $CheckFriendQuery) or die("400");

    if(mysqli_num_rows($Result) != 1){
        echo "400";
        exit();
    }

    $FriendID = mysqli_fetch_assoc($Result)["ID"];

    if($FriendID == $ID){
        echo "400";
        exit();
    }

    // Check if the friend is already added
    $CheckFriendEmptyQuery = "SELECT FriendID FROM friends WHERE FriendID = " . $FriendID . " AND ID = " . $ID . ";";

    $Check = mysqli_query($con, $CheckFriendEmptyQuery) or die("400");

    if(mysqli_num_rows($Check) > 0){
        echo "400";
        exit();
    }

    $str1 = $FriendID;

    // Add the friend
    $InsertFriendQuery = "INSERT INTO friends (FriendID, ID) VALUES (" . $FriendID . ", " . $ID . ");";
    mysqli_query($con, $InsertFriendQuery) or die("400");
    $InsertFriendQuery2 = "INSERT INTO friends (FriendID, ID) VALUES (" . $ID . ", " . $FriendID . ");";
    mysqli_query($con, $InsertFriendQuery2) or die("400");



    // Get the friend's profile picture
    $GetProfilePictureQuery = "SELECT ProfilePicture FROM players WHERE ID = " . $FriendID . ";";

    $ProfilePictureCheck = mysqli_query($con, $GetProfilePictureQuery) or die("400");
    
    if(mysqli_num_rows($ProfilePictureCheck) != 1){
        echo "400";
        exit();
    }
    // Print the profile picture
    $str1 .= "/";
    $ProfilePictureCheck2 = mysqli_query($con, $GetProfilePictureQuery) or die("400");
    $str1 .= mysqli_fetch_assoc($ProfilePictureCheck2)["ProfilePicture"];
    echo ($str1);
?>