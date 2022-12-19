<?php
    $con = mysqli_connect('localhost','root','root','unitydatabase');

    if(mysqli_connect_errno()){
        echo "Connection failed";
        exit();
    }

    $ID = $_POST["ID"];
    $NewHealth = $_POST["NewHealth"];
    $NewMana = $_POST["NewMana"];


    $UpdateMaxHealthQuery = "UPDATE players SET MaxHealth = " . $NewHealth . " WHERE ID = " . $ID . ";";
    $UpdateMaxManaQuery = "UPDATE players SET MaxMana = " . $NewMana . " WHERE ID = " . $ID . ";";

    $query1Result = mysqli_query($con, $UpdateMaxHealthQuery) or die("MaxHealth Update failed");
    $query2Result = mysqli_query($con, $UpdateMaxManaQuery) or die("MaxMana Update failed");

    echo("0");
?>