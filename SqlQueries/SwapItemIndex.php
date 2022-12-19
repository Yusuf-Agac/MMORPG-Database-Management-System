<?php
    $con = mysqli_connect('localhost','root','root','unitydatabase');

    if(mysqli_connect_errno()){
        echo "Connection failed";
        exit();
    }

    $ItemID = $_POST["ItemID"];
    $ItemIndex = $_POST["ItemIndex"];

    $SetItemIndexQuery = "UPDATE items SET ItemIndex = " . $ItemIndex . " WHERE ItemID = " . $ItemID . ";";

    $queryResult = mysqli_query($con, $SetItemIndexQuery) or die("Swap '" . $ItemID . "' item, '" . $ItemIndex . "' index query failed");

    echo("0");
?>