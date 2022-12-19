<?php
    $con = mysqli_connect('localhost','root','root','unitydatabase');

    if(mysqli_connect_errno()){
        echo "Connection failed";
        exit();
    }

    $ID = $_POST["ID"];
    $NewLevel = $_POST["NewLevel"];

    $LevelUpQuery = "UPDATE players SET Level = " . $NewLevel . " WHERE ID = " . $ID . ";";

    $queryResult = mysqli_query($con, $LevelUpQuery) or die("Level up failed");

    echo("0");
?>