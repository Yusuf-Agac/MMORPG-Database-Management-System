<?php
    $con = mysqli_connect('localhost','root','root','unitydatabase');

    if(mysqli_connect_errno()){
        echo "Connection failed";
        exit();
    }

    $username = $_POST["username"];
    $password = $_POST["password"];

    $namecheckquery = "SELECT username, salt, hash FROM players WHERE username='" . $username . "';";

    $namecheck = mysqli_query($con, $namecheckquery) or die("Name check query failed");

    if(mysqli_num_rows($namecheck) != 1){
        echo "There is problem with name check query";
        exit();
    }

    $existinginfo = mysqli_fetch_assoc($namecheck);
    $salt = $existinginfo["salt"];
    $hash = $existinginfo["hash"];

    $loginhash = crypt($password, $salt);
    if($hash != $loginhash){
        echo "Incorrect password";
        exit();
    }

    echo("0");
?>