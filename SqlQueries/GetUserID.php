<?php
    $con = mysqli_connect('localhost','root','root','unitydatabase');

    if(mysqli_connect_errno()){
    echo "Connection failed";
        exit();
    }

    $Username = $_POST["Username"];

    $namecheckquery = "SELECT ID FROM players WHERE username='" . $Username . "';";

    $namecheck = mysqli_query($con, $namecheckquery) or die("400");
    
    if(mysqli_num_rows($namecheck) != 1){
        echo "400";
        exit();
    }

    echo (mysqli_fetch_assoc($namecheck)["ID"]);
?>