<?php
	$servername = "";
	$username = "";
	$password = "";
	$dbName = "";
	
	$group_number = $_POST["idPost"];
	
	//Make Connection
	$conn = new mysqli($servername, $username, $password, $dbName);
	$conn->set_charset("utf8");
	//Check Connection
	if(!$conn) {
		die("Connection Faild.". mysqli_connect_error());
	}

	$sql = "SELECT Monday, Tuesday, Wednesday, Thursday, Friday, Saturday FROM group_rp WHERE GroupNumber = '".$group_number."' ";
	$result = mysqli_query($conn ,$sql);
	
	if(mysqli_num_rows($result) > 0) {
		//show data for each row
		$row = mysqli_fetch_assoc($result);
		
		echo mb_convert_encoding("Monday:".$row['Monday'] . "|Tuesday:".$row['Tuesday'] . "|Wednesday:".$row['Wednesday'] . "|Thursday:".$row['Thursday'] . "|Friday:".$row['Friday'] . "|Saturday:".$row['Saturday'],'utf-8', mb_detect_encoding($row['Monday']));
	}else{
		echo "ERROR*";
	}
?>