<?php
    $con = mysqli_connect('localhost','root','root','unitydatabase');

    if(mysqli_connect_errno()){
        echo "Connection failed";
        exit();
    }

    $ID = $_POST["ID"];
    $NewHealth = $_POST["NewHealth"];
    $NewMana = $_POST["NewMana"];


    $UpdateHealthQuery = "UPDATE players SET Health = " . $NewHealth . " WHERE ID = " . $ID . ";";
    $UpdateManaQuery = "UPDATE players SET Mana = " . $NewMana . " WHERE ID = " . $ID . ";";

    $query1Result = mysqli_query($con, $UpdateHealthQuery) or die("Health Update failed");
    $query2Result = mysqli_query($con, $UpdateManaQuery) or die("Mana Update failed");

    echo("0");
?>