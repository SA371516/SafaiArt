<?php
#値が送られてこない場合があるため
if(isset($_POST["name"])){
	$name=$_POST["name"];
	$score=$_POST["point"];
}
#並べる基準
$select=$_POST["select"];
$rank=1;

$host="localhost";
//$host="mysql1.php.xdomain.ne.jp";
$user="dbtext5b";
//$user="saserver_hostsa";
$pass="pass";
//$pass="syunta1998";

$dbname="dbtest5b";
//$dbname="saserver_test";
#接続開始
$mysqli=new mysqli($host,$user,$pass,$dbname);

#値が入っていたら
if(isset($name)){
	$query="INSERT INTO `ranking_KD` (`NAME`,`SCORE`) VALUES ('$name','$score');";

	$ret=$mysqli->query($query);
}

#表示させるもの/スコア
if($select=='No')$query = "SELECT * FROM ranking_KD ORDER BY SCORE DESC LIMIT 5";
#50音順
else if($select=='alphabet')$query = "SELECT * FROM ranking_KD ORDER BY NAME DESC LIMIT 5";
#登録した日付
else if($select=='date')$query = "SELECT * FROM ranking_KD ORDER BY DAY DESC LIMIT 5";
$ret=$mysqli->query($query);

if($ret){
	$count=0;
	$S=0;$A=null;$Date=null;
	while($row=$ret->fetch_assoc()){
		#横の数字の順位区分
		if($row["Score"]!=$S&&$select=="No"){
			$count+=1;
			$S=$row["Score"];
		}
		else if ($select=='alphabet'&&$row["Name"]!=$A){
			$count+=1;
			$A=$row["Name"];
		}
		else if ($select=='date'&&$row["Day"]!=$Date){
			$count+=1;
			$Date=$row["Day"];
		}


		#ここでは大文字小文字関係なし
		#A non well formed numeric value encountered inで悩むことになるから[一度UNIXへの変換]
		$D=strtotime($row["Day"]);
		$DD=date("Y年m月d日 H時m分",$D);
		echo $count.", ". $row["Name"]."\n ".$DD."		".$row["Score"],"\n";
	}
}

if(isset($name)){
	#rankingテーブルでscoreが$scoreより大きいレコードの数を数える
	$query="SELECT count(*)+1 AS RANK FROM ranking_KD Where SCORE>$score";
	$ret=$mysqli->query($query);
	$row=$ret->fetch_assoc();
	$rank=$row['RANK'];
	$rank = str_replace("\r\n", '', $rank);


	echo	"貴方は{$rank}位でした";
//EOT;
}
#接続終了
$mysqli->close();
?>