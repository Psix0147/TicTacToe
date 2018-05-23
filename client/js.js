//Создание поля//
for (var i=0; i<25; i++){
	document.getElementById("game").innerHTML+="<div class=\"block\"></div>";
}

//Счетчик//
var hod = 0;

//Основная функция программы//
document.getElementById("game").onclick = function(event){
	if (event.target.className == "block"){
		if (event.target.innerHTML == ""){
			if(hod % 2 == 0)
				event.target.innerHTML = "X";
			else event.target.innerHTML = "O";
			hod++;
		}

	}

	CheckWinner();
};

function CheckWinner(){
	var ActB = document.getElementsByClassName("Act");
	//Проверка Крестиков//
	for (var j=0; j<25; j=j+5){
		for (i=0; i<=1; i++){
			if (ActB[i+j].innerHTML=="X" && ActB[i+j+1].innerHTML=="X" && ActB[i+j+2].innerHTML=="X" && ActB[i+j+3].innerHTML=="X")
				alert("X Winner");
		}
	}	
	for (j=0; j<10; j++){
		for (i=0; i<=1; i++){
			if (ActB[i+j].innerHTML=="X" && ActB[i+j+5].innerHTML=="X" && ActB[i+j+10].innerHTML=="X" && ActB[i+j+15].innerHTML=="X")
				alert("X Winner");	
		}
	}
	for (j=0; j<10; j=j+5){
		for (i=0; i<=1; i++){
			if (ActB[i+j].innerHTML=="X" && ActB[i+j+6].innerHTML=="X" && ActB[i+j+12].innerHTML=="X" && ActB[i+j+18].innerHTML=="X")
				alert("X Winner");	
		}
	}
		
	for (j=0; j<10; j=j+5){
		for (i=3; i<=4; i++){
			if (ActB[i+j].innerHTML=="X" && ActB[i+j+4].innerHTML=="X" && ActB[i+j+8].innerHTML=="X" && ActB[i+j+12].innerHTML=="X")
				alert("X Winner");	
		}
	}
			
			
	//Проверка Ноликов//
		
	for (j=0; j<25; j=j+5){
		for (i=0; i<=1; i++){
			if (ActB[i+j].innerHTML=="O" && ActB[i+j+1].innerHTML=="O" && ActB[i+j+2].innerHTML=="O" && ActB[i+j+3].innerHTML=="O") 
				alert("O Winner");
		}
	}	
	for (j=0; j<10; j++){
		for (i=0; i<=1; i++){
			if (ActB[i+j].innerHTML=="O" && ActB[i+j+5].innerHTML=="O" && ActB[i+j+10].innerHTML=="O" && ActB[i+j+15].innerHTML=="O") 
				alert("O Winner");	
		}
	}
	for (j=0; j<10; j=j+5){
		for (i=0; i<=1; i++){
			if (ActB[i+j].innerHTML=="O" && ActB[i+j+6].innerHTML=="O" && ActB[i+j+12].innerHTML=="O" && ActB[i+j+18].innerHTML=="O") 
				alert("O Winner");	
		}
	}
		
	for (j=0; j<10; j=j+5){
		for (i=3; i<=4; i++){
			if (ActB[i+j].innerHTML=="O" && ActB[i+j+4].innerHTML=="O" && ActB[i+j+8].innerHTML=="O" && ActB[i+j+12].innerHTML=="O") 
				alert("O Winner");	
		}
	}
}
	
