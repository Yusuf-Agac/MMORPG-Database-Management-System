<?php
    $con = mysqli_connect('localhost','root','root','unitydatabase');

    if(mysqli_connect_errno()){
    echo "Connection failed";
        exit();
    }

    $ID = $_POST["ID"];

    $GetLevelQuery = "SELECT Level FROM players WHERE ID='" . $ID . "';";

    $GetLevel = mysqli_query($con, $GetLevelQuery) or die("400");


    echo (mysqli_fetch_assoc($GetLevel)["Level"]);
?>