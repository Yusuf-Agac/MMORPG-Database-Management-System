<?php
    $con = mysqli_connect('localhost','root','root','unitydatabase');

    if(mysqli_connect_errno()){
    echo "400";
        exit();
    }

    $ID = $_GET["ID"];
    $ItemIndex = $_GET["ItemIndex"];

    //check if name exists
    $itemcheckquery = "SELECT ItemID FROM items WHERE ID = '" . $ID . "' AND ItemIndex = '" . $ItemIndex . "';";

    $itemIdCheck = mysqli_query($con, $itemcheckquery) or die("400");
    
    if(mysqli_num_rows($itemIdCheck) != 1){
        echo "400";
        exit();
    }

    echo (mysqli_fetch_assoc($itemIdCheck)["ItemID"]);
?>