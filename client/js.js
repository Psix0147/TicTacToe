	//Создание поля//	
	for (var i=0; i<25; i++){
		document.getElementById('game').innerHTML+='<div class="block"></div>';
	}
	
	//Счетчик//
	var hod = 0;
	
	//Основная функция программы//
	document.getElementById('game').onclick = function(event){
		if (event.target.className == 'block'){
			if (event.target.innerHTML == ''){
				if(hod % 2 == 0)
					event.target.innerHTML = 'X';
				else event.target.innerHTML = 'O';
			hod++;			
			}
				
		}

		CheckWinner();
	}
	
	

	function CheckWinner(){
		var AB = document.getElementsByClassName('block');
		
			if (AB[0].innerHTML=="X" && AB[1].innerHTML=="X" && AB[2].innerHTML=="X" && AB[3].innerHTML=="X") alert('X Winner');
			if (AB[1].innerHTML=="X" && AB[2].innerHTML=="X" && AB[3].innerHTML=="X" && AB[4].innerHTML=="X") alert('X Winner');
			if (AB[5].innerHTML=="X" && AB[6].innerHTML=="X" && AB[7].innerHTML=="X" && AB[8].innerHTML=="X") alert('X Winner');
			if (AB[6].innerHTML=="X" && AB[7].innerHTML=="X" && AB[8].innerHTML=="X" && AB[9].innerHTML=="X") alert('X Winner');
			if (AB[10].innerHTML=="X" && AB[11].innerHTML=="X" && AB[12].innerHTML=="X" && AB[13].innerHTML=="X") alert('X Winner');
			if (AB[11].innerHTML=="X" && AB[12].innerHTML=="X" && AB[13].innerHTML=="X" && AB[14].innerHTML=="X") alert('X Winner');
			if (AB[15].innerHTML=="X" && AB[16].innerHTML=="X" && AB[17].innerHTML=="X" && AB[18].innerHTML=="X") alert('X Winner');
			if (AB[16].innerHTML=="X" && AB[17].innerHTML=="X" && AB[18].innerHTML=="X" && AB[19].innerHTML=="X") alert('X Winner');
			if (AB[20].innerHTML=="X" && AB[21].innerHTML=="X" && AB[22].innerHTML=="X" && AB[23].innerHTML=="X") alert('X Winner');
			if (AB[21].innerHTML=="X" && AB[22].innerHTML=="X" && AB[23].innerHTML=="X" && AB[24].innerHTML=="X") alert('X Winner');
			if (AB[0].innerHTML=="X" && AB[5].innerHTML=="X" && AB[10].innerHTML=="X" && AB[15].innerHTML=="X") alert('X Winner');	
			if (AB[1].innerHTML=="X" && AB[6].innerHTML=="X" && AB[11].innerHTML=="X" && AB[16].innerHTML=="X") alert('X Winner');	
			if (AB[2].innerHTML=="X" && AB[7].innerHTML=="X" && AB[12].innerHTML=="X" && AB[17].innerHTML=="X") alert('X Winner');
			if (AB[3].innerHTML=="X" && AB[8].innerHTML=="X" && AB[13].innerHTML=="X" && AB[18].innerHTML=="X") alert('X Winner');
			if (AB[4].innerHTML=="X" && AB[9].innerHTML=="X" && AB[14].innerHTML=="X" && AB[19].innerHTML=="X") alert('X Winner');
			if (AB[5].innerHTML=="X" && AB[10].innerHTML=="X" && AB[15].innerHTML=="X" && AB[20].innerHTML=="X") alert('X Winner');
			if (AB[6].innerHTML=="X" && AB[11].innerHTML=="X" && AB[16].innerHTML=="X" && AB[21].innerHTML=="X") alert('X Winner');
			if (AB[7].innerHTML=="X" && AB[12].innerHTML=="X" && AB[17].innerHTML=="X" && AB[22].innerHTML=="X") alert('X Winner');
			if (AB[8].innerHTML=="X" && AB[13].innerHTML=="X" && AB[18].innerHTML=="X" && AB[23].innerHTML=="X") alert('X Winner');
			if (AB[9].innerHTML=="X" && AB[14].innerHTML=="X" && AB[19].innerHTML=="X" && AB[24].innerHTML=="X") alert('X Winner');

			
			//Проверка Ноликов//
			if (AB[0].innerHTML=="O" && AB[1].innerHTML=="O" && AB[2].innerHTML=="O" && AB[3].innerHTML=="O") alert('O Winner');
			if (AB[1].innerHTML=="O" && AB[2].innerHTML=="O" && AB[3].innerHTML=="O" && AB[4].innerHTML=="O") alert('O Winner');
			if (AB[5].innerHTML=="O" && AB[6].innerHTML=="O" && AB[7].innerHTML=="O" && AB[8].innerHTML=="O") alert('O Winner');
			if (AB[6].innerHTML=="O" && AB[7].innerHTML=="O" && AB[8].innerHTML=="O" && AB[9].innerHTML=="O") alert('O Winner');
			if (AB[10].innerHTML=="O" && AB[11].innerHTML=="O" && AB[12].innerHTML=="O" && AB[13].innerHTML=="O") alert('O Winner');
			if (AB[11].innerHTML=="O" && AB[12].innerHTML=="O" && AB[13].innerHTML=="O" && AB[14].innerHTML=="O") alert('O Winner');
			if (AB[15].innerHTML=="O" && AB[16].innerHTML=="O" && AB[17].innerHTML=="O" && AB[18].innerHTML=="O") alert('O Winner');
			if (AB[16].innerHTML=="O" && AB[17].innerHTML=="O" && AB[18].innerHTML=="O" && AB[19].innerHTML=="O") alert('O Winner');
			if (AB[20].innerHTML=="O" && AB[21].innerHTML=="O" && AB[22].innerHTML=="O" && AB[23].innerHTML=="O") alert('O Winner');
			if (AB[21].innerHTML=="O" && AB[22].innerHTML=="O" && AB[23].innerHTML=="O" && AB[24].innerHTML=="O") alert('O Winner');
			if (AB[0].innerHTML=="O" && AB[5].innerHTML=="O" && AB[10].innerHTML=="O" && AB[15].innerHTML=="O") alert('O Winner');	
			if (AB[1].innerHTML=="O" && AB[6].innerHTML=="O" && AB[11].innerHTML=="O" && AB[16].innerHTML=="O") alert('O Winner');	
			if (AB[2].innerHTML=="O" && AB[7].innerHTML=="O" && AB[12].innerHTML=="O" && AB[17].innerHTML=="O") alert('O Winner');
			if (AB[3].innerHTML=="O" && AB[8].innerHTML=="O" && AB[13].innerHTML=="O" && AB[18].innerHTML=="O") alert('O Winner');
			if (AB[4].innerHTML=="O" && AB[9].innerHTML=="O" && AB[14].innerHTML=="O" && AB[19].innerHTML=="O") alert('O Winner');
			if (AB[5].innerHTML=="O" && AB[10].innerHTML=="O" && AB[15].innerHTML=="O" && AB[20].innerHTML=="O") alert('O Winner');
			if (AB[6].innerHTML=="O" && AB[11].innerHTML=="O" && AB[16].innerHTML=="O" && AB[21].innerHTML=="O") alert('O Winner');
			if (AB[7].innerHTML=="O" && AB[12].innerHTML=="O" && AB[17].innerHTML=="O" && AB[22].innerHTML=="O") alert('O Winner');
			if (AB[8].innerHTML=="O" && AB[13].innerHTML=="O" && AB[18].innerHTML=="O" && AB[23].innerHTML=="O") alert('O Winner');
			if (AB[9].innerHTML=="O" && AB[14].innerHTML=="O" && AB[19].innerHTML=="O" && AB[24].innerHTML=="O") alert('O Winner');	
	}
	
