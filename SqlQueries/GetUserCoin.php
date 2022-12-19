<?php
    $con = mysqli_connect('localhost','root','root','unitydatabase');

    if(mysqli_connect_errno()){
    echo "Connection failed";
        exit();
    }

    $ID = $_POST["ID"];

    $GetCoinQuery = "SELECT Coin FROM players WHERE ID='" . $ID . "';";

    $GetCoin = mysqli_query($con, $GetCoinQuery) or die("400");


    echo (mysqli_fetch_assoc($GetCoin)["Coin"]);
?>