<?php
    $con = mysqli_connect('localhost','root','root','unitydatabase');

    if(mysqli_connect_errno()){
        echo "Connection failed";
        exit();
    }

    $username = $_POST["username"];
    $password = $_POST["password"];

    $namecheckquery = "SELECT username FROM players WHERE username='" . $username . "';";

    $namecheck = mysqli_query($con, $namecheckquery) or die("Name check query failed");

    if(mysqli_num_rows($namecheck) > 0){
        echo "Name already exists";
        exit();
    }

    //add user to the table
    $salt = "\$5\$rounds=5000\$" . "steamedhams" . $username . "\$";
    $hash = crypt($password, $salt);
    $insertuserquery = "INSERT INTO players (username, hash, salt) VALUES ('" . $username . "', '" . $hash . "', '" . $salt . "');";
    mysqli_query($con, $insertuserquery) or die("Insert player query failed");

    echo("0");
?>