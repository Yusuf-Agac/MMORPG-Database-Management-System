<?php
$con = mysqli_connect('localhost', 'root', 'root', 'unitydatabase');

if (mysqli_connect_errno()) {
    echo "Connection failed";
    exit();
}

$ID = $_POST["ID"];

$sql = "SELECT * FROM skillbar WHERE ID = CAST('" . $ID . "' AS SIGNED);";

$result = mysqli_query($con, $sql);

if ($result) {
    while ($row = mysqli_fetch_assoc($result)) {
        echo("" . $row["SkillName"] . ", " . $row["SkillBarIndex"] . "/");
    }
}
else {
    echo "400";
}
?>