<?php
    $con = mysqli_connect('localhost','root','root','unitydatabase');

    if(mysqli_connect_errno()){
    echo "400";
        exit();
    }

    $ID = $_GET["ID"];

    $ProfilePictureQuery = "SELECT ProfilePicture FROM players WHERE ID = '" . $ID . "';";

    $ProfilePictureResult = mysqli_query($con, $ProfilePictureQuery) or die("400");
    
    echo (mysqli_fetch_assoc($ProfilePictureResult)["ProfilePicture"]);
?>