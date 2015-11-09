$(document).ready(function(){
	var x1;var y1;var x2;var y2;

	$("#step1").tooltip('show');
	getXY("#flag",1);
	getXY("#step1",2);
	move('#flag')
		.add('margin-left', x2-x1+20).duration('1s')
  		.then(function(){
			$("#step2").tooltip('show');
			getXY("#flag",1);
			getXY("#step2",2);
			moveTo2();
		})
  		.end();
function moveTo2(){
  	move('#flag')
		.add('margin-left', x2-x1+20).duration('1.5s')
		.then(function(){
			$("#step3").tooltip('show');
			getXY("#flag",1);
			getXY("#step3",2);
			moveTo3();
		})
  		.end();
}
function moveTo3(){
	$('html,body').animate({scrollTop:$('#step3').offset().top-60}, 800);
	move('#flag')
		.add('margin-top', y2-y1+10).duration('1.5s')
		.then(function(){
			$("#step4").tooltip('show');
			getXY("#flag",1);
			getXY("#step4",2);
			moveTo4();
		})
  		.end();
}
function moveTo4(){
	move('#flag')
		.add('margin-left', x2-x1+20).duration('1.5s')
		.then(function(){
			$("#step5").tooltip('show');
			getXY("#flag",1);
			getXY("#step5",2);
			moveTo5();
		})
  		.end();
}
function moveTo5(){
	$('html,body').animate({scrollTop:$('#step5').offset().top-60}, 800);
	move('#flag')
		.add('margin-top', y2-y1+10).duration('1.5s')
		.then(function(){
			$("#step6").tooltip('show');
			getXY("#flag",1);
			getXY("#step6",2);
			moveTo6();
		})
  		.end();
}
function moveTo6(){
	move('#flag')
		.add('margin-left', x2-x1+20).duration('1.5s')
		.then(function(){
			$("#step7").tooltip('show');
			getXY("#flag",1);
			getXY("#step7",2);
			moveTo7();
		})
  		.end();
}
function moveTo7(){
	$('html,body').animate({scrollTop:$('#step7').offset().top-60}, 800);
	move('#flag')
		.add('margin-top', y2-y1+10).duration('1.5s')
  		.end();
}
function getXY(elem,sort){
	if(sort==1){
		x1=$(elem).offset().left;
		y1=$(elem).offset().top;
	}
	else if(sort==2){
		x2=$(elem).offset().left;
		y2=$(elem).offset().top;
	}
}
});