<?php
$con = mysqli_connect('localhost', 'root', 'root', 'unitydatabase');

if (mysqli_connect_errno()) {
    echo "400";
    exit();
}

$ID = $_GET["ID"];

$sql = "SELECT FriendID FROM friends WHERE ID = CAST('" . $ID . "' AS SIGNED);";

$result = mysqli_query($con, $sql);

if ($result) {
    while ($row = mysqli_fetch_assoc($result)) {
        
        $friendID = $row["FriendID"];

        $sql3 = "SELECT * FROM players WHERE ID = CAST('" . $friendID . "' AS SIGNED);";
        $result3 = mysqli_query($con, $sql3);
        $row2 = mysqli_fetch_assoc($result3);
        $username = $row2["Username"];
        $profilePicture = $row2["ProfilePicture"];

        echo("" . $friendID . "," . $username . "," . $profilePicture . "/");
    }
}
else {
    echo "400";
}
?>