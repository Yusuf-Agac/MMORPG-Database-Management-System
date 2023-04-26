<?php
$con = mysqli_connect('localhost', 'root', 'root', 'unitydatabase');

if (mysqli_connect_errno()) {
    echo "Connection failed";
    exit();
}

$ID = $_GET["ID"];

$sql = "SELECT * FROM items WHERE ID = CAST('" . $ID . "' AS SIGNED);";

$result = mysqli_query($con, $sql);

if ($result) {
    while ($row = mysqli_fetch_assoc($result)) {
        echo("" . $row["ItemID"] . "," . $row["ItemName"] . "," . $row["ItemIndex"] . "," . $row["ID"] . "/");
    }
}
else {
    echo "400";
}
?>