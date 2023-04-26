<?php
    $con = mysqli_connect('localhost','root','root','unitydatabase');

    if(mysqli_connect_errno()){
    echo "Connection failed";
        exit();
    }

    $ID = $_GET["ID"];

    $GetExperienceQuery = "SELECT Experience FROM players WHERE ID='" . $ID . "';";

    $GetExperience = mysqli_query($con, $GetExperienceQuery) or die("400");


    echo (mysqli_fetch_assoc($GetExperience)["Experience"]);
?>