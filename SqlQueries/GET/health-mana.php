<?php
    $con = mysqli_connect('localhost','root','root','unitydatabase');

    if(mysqli_connect_errno()){
    echo "Connection failed";
        exit();
    }

    $ID = $_GET["ID"];

    $GetHealthQuery = "SELECT Health FROM players WHERE ID='" . $ID . "';";
    $GetHealth = mysqli_query($con, $GetHealthQuery) or die("400");

    $GetManaQuery = "SELECT Mana FROM players WHERE ID='" . $ID . "';";
    $GetMana = mysqli_query($con, $GetManaQuery) or die("400");

    $GetMaxHealthQuery = "SELECT MaxHealth FROM players WHERE ID='" . $ID . "';";
    $GetMaxHealth = mysqli_query($con, $GetMaxHealthQuery) or die("400");

    $GetMaxManaQuery = "SELECT MaxMana FROM players WHERE ID='" . $ID . "';";
    $GetMaxMana = mysqli_query($con, $GetMaxManaQuery) or die("400");

    $result = mysqli_fetch_assoc($GetHealth)["Health"];
    $result .= "/";
    $result .= mysqli_fetch_assoc($GetMana)["Mana"];
    $result .= "/";
    $result .= mysqli_fetch_assoc($GetMaxHealth)["MaxHealth"];
    $result .= "/";
    $result .= mysqli_fetch_assoc($GetMaxMana)["MaxMana"];

    echo ($result);
?>