<?php
    $con = mysqli_connect('localhost','root','root','unitydatabase');

    if(mysqli_connect_errno()){
        echo "Connection failed";
        exit();
    }

    $SkillName = $_POST["SkillName"];
    $ID = $_POST["ID"];

    $InsertSkillQuery = "INSERT INTO skills (SkillName, ID) VALUES ('" . $SkillName . "', " . $ID . ");";
    mysqli_query($con, $InsertSkillQuery) or die("Skill query failed");

    echo("0");
?>