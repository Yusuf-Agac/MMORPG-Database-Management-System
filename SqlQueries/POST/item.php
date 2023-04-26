<?php
    $con = mysqli_connect('localhost','root','root','unitydatabase');

    if(mysqli_connect_errno()){
        echo "Connection failed";
        exit();
    }

    $ItemName = $_POST["ItemName"];
    $ItemIndex = $_POST["ItemIndex"];
    $ID = $_POST["ID"];

    $CheckItemQuery = "SELECT ItemID FROM items WHERE ItemIndex = " . $ItemIndex . " AND ID = " . $ID . ";";

    $Check = mysqli_query($con, $CheckItemQuery) or die("CheckItemQuery failed");

    if(mysqli_num_rows($Check) > 0){
        echo "Grid is not empty";
        exit();
    }

    $InsertItemQuery = "INSERT INTO items (ItemName, ItemIndex, ID) VALUES ('" . $ItemName . "', CAST('" . $ItemIndex . "' AS SIGNED), CAST('" . $ID . "' AS SIGNED));";
    $str = "4: Insert Item query failed / type of itemindex -> ";
    $str .= $ItemIndex;
    $str .= "/ type of ID -> ";
    $str .= $ID;
    mysqli_query($con, $InsertItemQuery) or die($str);

    echo("0");
?>