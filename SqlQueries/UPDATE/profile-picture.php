<?php
    $con = mysqli_connect('localhost','root','root','unitydatabase');

    if(mysqli_connect_errno()){
        echo "Connection failed";
        exit();
    }

    $ID = $_POST["ID"];
    $NewProfilePicture = $_POST["NewProfilePicture"];

    $UpdateProfilePictureQuery = "UPDATE players SET ProfilePicture = '" . $NewProfilePicture . "' WHERE ID = " . $ID . ";";

    $queryResult = mysqli_query($con, $UpdateProfilePictureQuery) or die("ProfilePicture Update failed");

    echo("0");
?>