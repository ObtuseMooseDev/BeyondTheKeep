
void planningPhase(){
   
  
   fill(200);
   int p1L1Count = 1;
   int p1L2Count = 1;
   int p1L3Count = 1;
   int p2L1Count = 1;
   int p2L2Count = 1;
   int p2L3Count = 1;
   text("PLANNING PHASE", width/2 - 100, height/2);
   pushMatrix();
     stroke(200);
     line(width/4,0, width/4, height);
     line(width/2,0,width/2,height);
     line(width/4*3,0, width/4*3,height);
   popMatrix();
   
   if(!player1Turn && !player2Turn){ player1Turn = true; } //if its no ones turn, set it to player 1's turn.
   if(player1Turn){ 
     //if its player 1 turn draw the player 1 planning UI
     fill(25);
     //Available Cash Display
     text("P1 Cash: " + p1Cash, 30, height - 50);
     pushMatrix();
       fill(200);
       rect(0,0,540,960/2);
       text("PLAYER 1 TURN", width/2 + 100, height/2);
       fill(200,25,25);
       noStroke();
       rect(0, height/2, width, 50);
       fill(200);
       text("END YOUR TURN", width - 200, height/2 + 37.5);
     popMatrix();
     
     previewMinions(p1L1Count,p1L2Count,p1L3Count,p2L1Count,p2L2Count,p2L3Count); //Displays all the minions you've already placed
     
     image(selectMinionSmall, 50, 25);
     image(selectMinionMediumBlue, 200, 25);
     image(selectMinionHeavy, 350, 25);
     
     image(coinFive, 50 + 75/2, 25 + 75/2);
     image(coinTen, 200 + 75/2, 25 + 75/2);
     image(coinFifteen, 350 + 75/2, 25 + 75/2);
     
     if(typeSelected){
        //if the type is selected... select unit elemental property / display elemental ui
        fill(25);
        text("Select Your Unit's Elemental Type", 25, 150);
        image(selectElementWater, 50, 175);
        image(selectMinionSmallRed, 200, 175);
        image(selectMinionSmallGreen, 350, 175);
     }
     if(elementSelected){
       //if the element is selected... select lane / display select lane ui
       text("Select the Lane where you want to place the Unit", 25, 300);
       image(selectLane1, 50, 325);
       image(selectLane2, 200, 325);
       image(selectLane3, 350, 325);
     }
     if(laneSelected){
       //if everything is selected... display the confirm button
       text("REMEMBER TO CONFIRM YOUR SELECTION", 25, 430);
       fill(200);
       rect(50,450, 100, 50);
       fill(25);
       text("CONFIRM", 50+12.5, 475+12.5);
     }
   }
   
   else if(player2Turn){
     //player 2 equivelent //////////////////////////////////////////////////////////////////////////
     pushMatrix();
       fill(200);
       rect(0,height/2,540,960/2);
     fill(200,25,25);
       noStroke();
       rect(0, height/2 - 50, width, 50);
       fill(200);
       text("END YOUR TURN", width - 200, height/2 - 12.5);
     popMatrix();
     fill(25);
     text("P2 Cash: " + p2Cash, 30, height - 50);
     pushMatrix();
       translate(125, 50 + 76 + height/2);
       rotate(PI);
       image(selectMinionSmall, 0, 0);
 
       image(selectMinionMediumBlue,-150, 0);
       image(selectMinionHeavy, -300,0);
       rotate(PI);
       image(coinFive, -75/2, -75/2);
       image(coinTen, 150 - 75/2 , -75/2);
       image(coinFifteen, 300 -75/2 , -75/2);
     popMatrix();
     
     previewMinions(p1L1Count,p1L2Count,p1L3Count,p2L1Count,p2L2Count,p2L3Count);
     
     if(typeSelected){
        pushMatrix();
          
          image(selectElementWater, 50,175 + height/2);
          image(selectMinionSmallRed, 200,175 + height/2);
          image(selectMinionSmallGreen,350,175 + height/2);
        popMatrix();
     }
     if(elementSelected){
       image(selectLane1, 50, 325  + height/2);
       image(selectLane2, 200, 325  + height/2);
       image(selectLane3, 350, 325  + height/2);
     }
     if(laneSelected){
       pushMatrix();
         fill(200);
         rect(50,425, 100, 50);
         fill(25);
         text("CONFIRM", 50+12.5, 450+12.5);
       popMatrix();
     }
   }
}

void mousePressed(){
  //mouse hit detection
  if(player1Turn){
        
        if(mouseX >= 50 && mouseX <= (50+76) && mouseY >= 25 && mouseY <= (25+76)){
          System.out.println("Minion Type Selected: Small");
          if(p1Cash >= SMALLCOST){
            tempType = 1;
            typeSelected = true;
          } else {
            pushMatrix();
              text("NOT ENOUGH CASH", 50, 50);
            popMatrix();
          }
        }
        if(mouseX >= 200 && mouseX <= 276 && mouseY >= 25 && mouseY <= (25+76)){
          System.out.println("Minion Type Selected: Med");
          if(p1Cash >= MEDIUMCOST){
            tempType = 2;
            typeSelected = true;
          } else {
            pushMatrix();
              text("NOT ENOUGH CASH", 50, 50);
            popMatrix();
          }
        }
        if(mouseX >= 350 && mouseX <= (350+76) && mouseY >= 25 && mouseY <= (25+76)){
          System.out.println("Minion Type Selected: Heavy");
          if(p1Cash >= HEAVYCOST){
            tempType = 3;
            typeSelected = true;
          } else {
            pushMatrix();
              text("NOT ENOUGH CASH", 50, 50);
            popMatrix();
          }
        }
    
    if(typeSelected){
        if(mouseX >= 50 && mouseX <= (50+76) && mouseY >= 175 && mouseY <= 175+76){
          System.out.println("Minion Element Selected: Water");
          tempElement = 1;
          elementSelected = true;
        }
        if(mouseX >= 200 && mouseX <= 276 && mouseY >= 175 && mouseY <= 175+76){
          System.out.println("Minion Element Selected: Fire");
          tempElement = 2;
          elementSelected = true;
        }
        if(mouseX >= 350 && mouseX <= (350+76) && mouseY >= 175 && mouseY <= 175+76){
          System.out.println("Minion Element Selected: Earth");
          tempElement = 3;
          elementSelected = true;
        }
    }
    if(typeSelected && elementSelected){
        
        if(mouseX >= 50 && mouseX <= (50+76) && mouseY >= 350 && mouseY <= 350+76){
          System.out.println("Lane Selected: 1");
          tempLane = 1;
          laneSelected = true;
        }
        if(mouseX >= 200 && mouseX <= 276 && mouseY >= 350 && mouseY <= 350+76){
          System.out.println("Lane Selected: 2");
          tempLane = 2;
          laneSelected = true;
        }
        if(mouseX >= 350 && mouseX <= (350+76) && mouseY >= 350 && mouseY <= 350+76){
          System.out.println("Lane Selected: 3");
          tempLane = 3;
          laneSelected = true;
        }
    }
    if(typeSelected && elementSelected && laneSelected){
       if(mouseX >= 50 && mouseX <= 150 && mouseY >= 450 && mouseY <= 500){
           if(player1Turn){
             
             Minion tempMinion = new Minion(1,tempLane,tempElement,tempType);
             p1Minions.add(tempMinion);
             if(tempMinion.type == 1){
               p1Cash = p1Cash - SMALLCOST;
             }
             else if(tempMinion.type == 2){
               p1Cash = p1Cash - MEDIUMCOST;
             }
             else if(tempMinion.type == 3){
               p1Cash = p1Cash - HEAVYCOST;
             }
            
           }
           else if(player2Turn){
             Minion tempMinion = new Minion(2,tempLane,tempElement,tempType);
             p2Minions.add(tempMinion);
             if(tempMinion.type == 1){
               p2Cash = p2Cash - SMALLCOST;
             }
             else if(tempMinion.type == 2){
               p2Cash = p2Cash - MEDIUMCOST;
             }
             else if(tempMinion.type == 3){
               p2Cash = p2Cash - HEAVYCOST;
             }
           }
           else{
              println("something went terribly wrong - Error related to turn booleans"); 
           }
           typeSelected = false;
           elementSelected = false;
           laneSelected = false;
       }
    }
    if(mouseX >= 150 && mouseX <= width && mouseY >= height/2 && mouseY <= height/2 + 50){
        if(player1Turn){
           typeSelected = false;
           elementSelected = false;
           laneSelected = false;
           player1Turn = false;
           player2Turn = true;
           println("P1 END TURN " + typeSelected); 
        } else if (player2Turn){
           typeSelected = false;
           elementSelected = false;
           laneSelected = false;
           player2Turn = false;
           planning = false;
           deadP1Minions.removeAll(deadP1Minions);
           deadP2Minions.removeAll(deadP2Minions);
           for(Minion m : p1Minions){
              m.myTurn = false;
              m.dead = false;
              m.combat = false;
              m.battleStart = true;
           }
           for(Minion m : p2Minions){
              m.myTurn = false;
              m.dead = false;
              m.combat = false;
              m.battleStart = true;
           }
           battle = true;
        }
    }
  }
  
  if(player2Turn){
        
        if(mouseX >= 50 && mouseX <= (50+76) && mouseY >= 50 + height/2 && mouseY <= 125 + height/2){
          System.out.println("Minion Type Selected: Small");
          if(p2Cash >= SMALLCOST){
            tempType = 1;
            typeSelected = true;
          } else {
            pushMatrix();
              text("NOT ENOUGH CASH", 50, 50);
            popMatrix();
          }
        }
        if(mouseX >= 200 && mouseX <= 276 && mouseY >= 50 + height/2 && mouseY <= 125 + height/2){
          System.out.println("Minion Type Selected: Med");
          if(p2Cash >= MEDIUMCOST){
            tempType = 2;
            typeSelected = true;
          } else {
            pushMatrix();
              text("NOT ENOUGH CASH", 50, 50);
            popMatrix();
          }
        }
        if(mouseX >= 350 && mouseX <= (350+76) && mouseY >= 50 + height/2 && mouseY <= 125 + height/2){
          System.out.println("Minion Type Selected: Heavy");
           if(p2Cash >= HEAVYCOST){
            tempType = 3;
            typeSelected = true;
          } else {
            pushMatrix();
              text("NOT ENOUGH CASH", 50, 50);
            popMatrix();
          }
        }
    
    if(typeSelected){
        if(mouseX >= 50 && mouseX <= (50+76) && mouseY >= 175 + height/2 && mouseY <= 175+76 + height/2){
          System.out.println("Minion Element Selected: Water");
          tempElement = 1;
          elementSelected = true;
        }
        if(mouseX >= 200 && mouseX <= 276 && mouseY >= 175 + height/2 && mouseY <= 175+76 + height/2){
          System.out.println("Minion Element Selected: Fire");
          tempElement = 2;
          elementSelected = true;
        }
        if(mouseX >= 350 && mouseX <= (350+76) && mouseY >= 175 + height/2 && mouseY <= 175+76 + height/2){
          System.out.println("Minion Element Selected: Earth");
          tempElement = 3;
          elementSelected = true;
        }
    }
    if(typeSelected && elementSelected){
        
        if(mouseX >= 50 && mouseX <= (50+76) && mouseY >= 350 + height/2 && mouseY <= 350 + 76 + height/2){
          System.out.println("Lane Selected: 1");
          tempLane = 1;
          laneSelected = true;
        }
        if(mouseX >= 200 && mouseX <= 276 && mouseY >= 350 + height/2 && mouseY <= 350 + 76 + height/2){
          System.out.println("Lane Selected: 2");
          tempLane = 2;
          laneSelected = true;
        }
        if(mouseX >= 350 && mouseX <= (350+76) && mouseY >= 350 + height/2 && mouseY <= 350 + 76 + height/2){
          System.out.println("Lane Selected: 3");
          tempLane = 3;
          laneSelected = true;
        }
    }
    if(typeSelected && elementSelected && laneSelected){
       if(mouseX >= 50 && mouseX <= 150 && mouseY >= 425 && mouseY <= 475){
           if(player1Turn){
             
             Minion tempMinion = new Minion(1,tempLane,tempElement,tempType);
             p1Minions.add(tempMinion);
             if(tempMinion.type == 1){
               p1Cash = p1Cash - SMALLCOST;
             }
             else if(tempMinion.type == 2){
               p1Cash = p1Cash - MEDIUMCOST;
             }
             else if(tempMinion.type == 3){
               p1Cash = p1Cash - HEAVYCOST;
             }
            
           }
           else if(player2Turn){
             Minion tempMinion = new Minion(2,tempLane,tempElement,tempType);
             p2Minions.add(tempMinion);
             if(tempMinion.type == 1){
               p2Cash = p2Cash - SMALLCOST;
             }
             else if(tempMinion.type == 2){
               p2Cash = p2Cash - MEDIUMCOST;
             }
             else if(tempMinion.type == 3){
               p2Cash = p2Cash - HEAVYCOST;
             }
           }
           else{
              println("something went terribly wrong - Error related to turn booleans"); 
           }
           typeSelected = false;
           elementSelected = false;
           laneSelected = false;
       }
    }
    if(mouseX >= 150 && mouseX <= width && mouseY >= height/2 - 50 && mouseY <= height/2){
        if(player1Turn){
           typeSelected = false;
           elementSelected = false;
           laneSelected = false;
           player1Turn = false;
           player2Turn = true;
           println("P1 END TURN"); 
        } else if (player2Turn){
           typeSelected = false;
           elementSelected = false;
           laneSelected = false;
           player2Turn = false;
           planning = false;
           deadP1Minions.removeAll(deadP1Minions);
           deadP2Minions.removeAll(deadP2Minions);
           turn = 0;
           for(Minion m : p1Minions){
              
              m.myTurn = false;
              m.dead = false;
              m.combat = false;
              m.battleStart = true;
           }
           for(Minion m : p2Minions){
              m.myTurn = false;
              m.dead = false;
              m.combat = false;
              m.battleStart = true;
           }
           battle = true;
        }
    }
  }
}
