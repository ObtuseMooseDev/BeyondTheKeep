
void battlePhase(){
   
   text("BATTLE PHASE", width/2 - 50, height/2);
   stroke(200);
   line(width/4,0, width/4, height);
   line(width/2,0,width/2,height);
   line(width/4*3,0, width/4*3,height);
   
   for(Minion m : p1Minions){
       //for every minion on player1's team call update() and render() so long as they're not dead
       if(m != null || deadP1Minions.contains(m) ){
         //println(m.lane);
         m.update();
         m.render();
       }
       else{
          //println("null"); 
       }
   }
    for(Minion m : p2Minions){
      //for every minion on player2's team call update() and render() so long as they're not dead
      if(m != null || deadP2Minions.contains(m)){
       m.update();
       m.render();
      }
    }
    if(buffer == 60){
       turn++;
       buffer = 0; 
    }
    else{
       buffer++; 
    }
}


