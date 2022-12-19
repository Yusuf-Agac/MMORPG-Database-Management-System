<?php
    $con = mysqli_connect('localhost','root','root','unitydatabase');

    if(mysqli_connect_errno()){
        echo "Connection failed";
        exit();
    }

    $ID = $_POST["ID"];
    $NewSkillPoint = $_POST["NewSkillPoint"];

    $UpdateSkillPointQuery = "UPDATE players SET SkillPoint = " . $NewSkillPoint . " WHERE ID = " . $ID . ";";

    $queryResult = mysqli_query($con, $UpdateSkillPointQuery) or die("SkillPoint Update failed");

    echo("0");
?>