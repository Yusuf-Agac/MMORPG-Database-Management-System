<?php
$con = mysqli_connect('localhost', 'root', 'root', 'unitydatabase');

if (mysqli_connect_errno()) {
    echo "Connection failed";
    exit();
}

$ID = $_POST["ID"];

$sql = "SELECT SkillName FROM skills WHERE ID = CAST('" . $ID . "' AS SIGNED);";

$result = mysqli_query($con, $sql);

if ($result) {
    while ($row = mysqli_fetch_assoc($result)) {
        echo("" . $row["SkillName"] . "/");
    }
}
else {
    echo "400";
}
?>