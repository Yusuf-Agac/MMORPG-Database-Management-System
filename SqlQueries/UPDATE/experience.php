<?php
    $con = mysqli_connect('localhost','root','root','unitydatabase');

    if(mysqli_connect_errno()){
        echo "Connection failed";
        exit();
    }

    $ID = $_POST["ID"];
    $NewXP = $_POST["NewXP"];

    $UpdateExperienceQuery = "UPDATE players SET Experience = " . $NewXP . " WHERE ID = " . $ID . ";";

    $queryResult = mysqli_query($con, $UpdateExperienceQuery) or die("Experience Update failed");

    echo("0");
?>