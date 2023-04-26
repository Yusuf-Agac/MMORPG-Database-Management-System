<?php
    $con = mysqli_connect('localhost','root','root','unitydatabase');

    if(mysqli_connect_errno()){
        echo "Connection failed";
        exit();
    }

    $ID = $_POST["ID"];
    $NewCoin = $_POST["NewCoin"];

    $UpdateCoinQuery = "UPDATE players SET Coin = " . $NewCoin . " WHERE ID = " . $ID . ";";

    $queryResult = mysqli_query($con, $UpdateCoinQuery) or die("Coin Update failed");

    echo("0");
?>