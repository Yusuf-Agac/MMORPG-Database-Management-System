<?php
    $con = mysqli_connect('localhost','root','root','unitydatabase');

    if(mysqli_connect_errno()){
        echo "Connection failed";
        exit();
    }

    $SkillName = $_POST["SkillName"];
    $SkillBarIndex = $_POST["SkillBarIndex"];
    $ID = $_POST["ID"];

    $CheckSkillQuery = "SELECT SkillName FROM skillbar WHERE SkillBarIndex = " . $SkillBarIndex . " AND ID = " . $ID . ";";

    $Check = mysqli_query($con, $CheckSkillQuery) or die("CheckSkillQuery failed");

    if(mysqli_num_rows($Check) == 0){
        $InsertSkillQuery = "INSERT INTO skillbar (SkillName, SkillBarIndex, ID) VALUES ('" . $SkillName . "', CAST('" . $SkillBarIndex . "' AS SIGNED), CAST('" . $ID . "' AS SIGNED));";
        mysqli_query($con, $InsertSkillQuery) or die("InsertSkillQuery failed");
    }
    elseif(mysqli_num_rows($Check) == 1){
        $UpdateSkillQuery = "UPDATE skillbar SET SkillName = '" . $SkillName . "' WHERE SkillBarIndex = " . $SkillBarIndex . " AND ID = " . $ID . ";";
        mysqli_query($con, $UpdateSkillQuery) or die("UpdateSkillQuery failed");
    }
    else{
        echo "There is a problem with the database";
        exit();
    }

    echo("0");
?>