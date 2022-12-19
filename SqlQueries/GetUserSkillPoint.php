<?php
    $con = mysqli_connect('localhost','root','root','unitydatabase');

    if(mysqli_connect_errno()){
    echo "Connection failed";
        exit();
    }

    $ID = $_POST["ID"];

    $GetSkillPointQuery = "SELECT SkillPoint FROM players WHERE ID='" . $ID . "';";

    $GetSkillPoint = mysqli_query($con, $GetSkillPointQuery) or die("400");


    echo (mysqli_fetch_assoc($GetSkillPoint)["SkillPoint"]);
?>