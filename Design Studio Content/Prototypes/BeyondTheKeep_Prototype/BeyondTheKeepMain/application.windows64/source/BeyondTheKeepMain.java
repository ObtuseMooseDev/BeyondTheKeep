import processing.core.*; 
import processing.data.*; 
import processing.event.*; 
import processing.opengl.*; 

import java.util.HashMap; 
import java.util.ArrayList; 
import java.io.File; 
import java.io.BufferedReader; 
import java.io.PrintWriter; 
import java.io.InputStream; 
import java.io.OutputStream; 
import java.io.IOException; 

public class BeyondTheKeepMain extends PApplet {

/* ********************************************************************
Beyond The Keep - Prototype
Main Class
LAST MODIFIED: February 06th 2014
CREATED BY: JAKE DEUGO, TYLER DINARDO, CHRIS HOSMAR & ZACHARY SULLIVAN
******************************************************************** */

/** Global Variables **/
boolean planning, battle; //phase Booleans
boolean player1Turn = true, player2Turn = false, typeSelected = false, elementSelected = false, laneSelected = false;
boolean loaded = false;
int tempType, tempElement, tempLane;
int buffer = 0;
int turn;
int p1BaseHealth, p2BaseHealth, p1Cash, p2Cash;
/** Global Arrays **/
ArrayList<Minion> p1Minions, p2Minions, deadP1Minions, deadP2Minions;
/** Global Class Variable Declarations **/
PFont mono;
PImage selectMinionSmall,selectMinionSmallRed, selectMinionSmallGreen, selectMinionMediumBlue, selectMinionHeavy, selectElementWater, selectLane1, selectLane2, selectLane3, explosion, blast, base;
PImage smallRed, smallBlue, smallGreen, mediumRed, mediumBlue, mediumGreen, heavyRed, heavyBlue, heavyGreen, coinFive, coinTen, coinFifteen;

/** Global Constants **/
int SMALLCOST = 5;
int MEDIUMCOST = 10;
int HEAVYCOST = 15;

public void setup(){
  /** Load Assets **/
  
  mono = loadFont("AgencyFB-Reg-48.vlw");
  selectMinionSmall = loadImage("selectMinionLight.png");
  selectMinionMediumBlue = loadImage("selectMinionMedium.png");
  selectMinionHeavy = loadImage("selectMinionHeavy.png");
  selectMinionSmallRed = loadImage("selectMinion_Fire.png");
  selectMinionSmallGreen = loadImage("selectMinion_Earth.png");
  selectElementWater = loadImage("selectMinion_Water.png");
  selectLane1 = loadImage("selectLane1.png");
  selectLane2 = loadImage("selectLane2.png");
  selectLane3 = loadImage("selectLane3.png");
  explosion = loadImage("explosion.png");
  blast = loadImage("blast.png");
  base = loadImage("base.png");
  smallBlue = loadImage("minionLight_Water.png");
  smallRed = loadImage("minionLight_Fire.png");
  smallGreen = loadImage("minionLight_Earth.png");
  mediumBlue = loadImage("minionMedium_Water.png");
  mediumRed = loadImage("minionMedium_Fire.png");
  mediumGreen = loadImage("minionMedium_Earth.png");
  heavyBlue = loadImage("minionHeavy_Water.png");
  heavyRed = loadImage("minionHeavy_Fire.png");
  heavyGreen = loadImage("minionHeavy_Earth.png");
  coinFive = loadImage("5Coin.png");
  coinTen = loadImage("10Coin.png");
  coinFifteen = loadImage("15Coin.png");
  
  /** Local Variables **/
  
  planning = false;
  battle = false;
  p1BaseHealth = 10;
  p2BaseHealth = 10;
  p1Cash = 30;
  p2Cash = 30;
  /** Global Array Declarations **/
  p1Minions = new ArrayList<Minion>();
  p2Minions = new ArrayList<Minion>();
  deadP1Minions = new ArrayList<Minion>();
  deadP2Minions = new ArrayList<Minion>();
   
  /** Properties**/
  size(540,940, P2D);
  frameRate(60);
  
}

public void draw(){
  background(25);
  if(planning){
     //if it is the planning phase calls the planning phase function (planningPhase.pde)
     image(base,0,0);
     planningPhase();
     
  } else if(battle){
     //if it is the battle phase calls the battle phase function (battlePhase.pde)
     battlePhase();
     image(base,0,0);
     pushMatrix();
     fill(200);
       text("Base Health: " + p1BaseHealth, 50, 125);
       text("Base Health: " + p2BaseHealth, 50, height - 100);
     popMatrix();
  } else {
     textFont(mono, 30);
     text("PRESS ENTER TO BEGIN", width/2 - 100, height/2 - 100);
  }
   
}

public void keyPressed(){ // Key based interaction

  if (key == ENTER || key == RETURN) {
          if(!planning && !battle){
             planning = true; 
          } else if( !battle ){
             //planning = false;
             //battle = true;
          } else if( !planning ){
             battle = false;
             planning = true;
             p1Cash += 30;
             p2Cash += 30;
          }
  }
  switch(key){
    default:
      break;
  }
}
/* ********************************************************************
Beyond The Keep - Prototype
Minion Class
LAST MODIFIED: February 06th 2014
CREATED BY: JAKE DEUGO, TYLER DINARDO, CHRIS HOSMAR & ZACHARY SULLIVAN
******************************************************************** */
class Minion{
  /** Local Variables **/
  int id, uID;
  int team, lane, element, type;
  PVector position;
  int health, atk, spd; // Unit Stats (assigned using assignStats function)
  boolean myTurn, combat, dead = false, battleStart = true;
  /** Local Arrays **/
  ArrayList<Minion> friendlyMinions;
  ArrayList<Minion> enemyMinions;
  Minion combatTarget;
  
  
  Minion(int tm, int ln, int el, int ty){
     if(tm == 0 || ln == 0 || el == 0 || ty == 0) { println("something went terribly wrong; minion was created with default values or incorrect values!");
     }
     //Minion Constructor
     team = tm; lane = ln; element = el; type = ty;
     assignID(team, lane, element, type);
     assignStats(type);
     position = new PVector(0,0);
     friendlyMinions = new ArrayList<Minion>();
     enemyMinions = new ArrayList<Minion>();
     position.x = (width/4) * lane;
     position.y = height - (height*(team-1));
     println("minion created");
  }
  
  public void update(){
    if(battleStart){
      //reset certain varaibles at the beginning of the turn.
       assignStats(type);
       position.y = height - (height*(team-1));
       battleStart = false;
    }
    combat = false;
    if(team == 1){
      if(dead && !deadP1Minions.contains(this) ){
         //if this minion is dead add it to deadMinions
         deadP1Minions.add(this);
      }
      if ( deadP1Minions.isEmpty() || !deadP1Minions.contains(this) ){
        //println("not dead == true");
        getLaneRef();
        getEnemyRef();
        checkBase();
        if(!myTurn){
          println("checking my turn");
          checkTurn();
        }
        else if(myTurn){
          if(!combat){
             //println("moving");
             move();
          } else if(combat){
             //println("fighting");
             attack(); 
          }
        }
        combat = false;
        checkHealth();
      }
    }
    else if(team == 2){
      if(dead && !deadP2Minions.contains(this)){
         deadP2Minions.add(this);
         //println("p2 added to deadMinions");
      } 
      if( deadP2Minions.isEmpty()  || !deadP2Minions.contains(this) ) {
        
        getLaneRef();
        getEnemyRef();
        checkBase();
        if(!myTurn){ 
          checkTurn();
        }
        else if(myTurn){
          if(!combat){
             move();
          } else if(combat){
             attack(); 
          }
        }
        
        checkHealth();
      }
    }
  }
  public void render(){
    if(!dead){
    
    if(element == 1)
      fill(0,0,255);
    else if( element == 2 )
      fill(255, 0 , 0);
    else if ( element == 3 )
      fill(0,255,0);
    if(type == 1){
      if(element == 1 ){
        if(team == 2){
           pushMatrix();
             translate(position.x + 23.5f,position.y);
             rotate(PI);
             image(smallBlue,0,0);
           popMatrix();
        } else {
           pushMatrix();
             translate(position.x - 23.5f,position.y);
             image(smallBlue,0,0);
           popMatrix();
        }
      }
      if(element == 2 ){
        if(team == 2){
           pushMatrix();
             translate(position.x + 23.5f,position.y);
             rotate(PI);
             image(smallRed,0,0);
           popMatrix();
        } else {
           pushMatrix();
             translate(position.x - 23.5f,position.y);
             image(smallRed,0,0);
           popMatrix();
        }
      }
      if(element == 3){
        if(team == 2){
           pushMatrix();
             translate(position.x + 23.5f,position.y);
             rotate(PI);
             image(smallGreen,0,0);
           popMatrix();
        } else {
           pushMatrix();
             translate(position.x - 23.5f,position.y);
             image(smallGreen,0,0);
           popMatrix();
        }
        
      }
    }
    else if(type == 2){
      if(element == 1 ){
        if(team == 2){
           pushMatrix();
             translate(position.x + 30,position.y);
             rotate(PI);
             image(mediumBlue,0,0);
           popMatrix();
        } else {
           pushMatrix();
             translate(position.x - 30,position.y);
             image(mediumBlue,0,0);
           popMatrix();
        }
      }
      if(element == 2 ){
        if(team == 2){
           pushMatrix();
             translate(position.x + 30,position.y);
             rotate(PI);
             image(mediumRed,0,0);
           popMatrix();
        } else {
           pushMatrix();
             translate(position.x - 30,position.y);
             image(mediumRed,0,0);
           popMatrix();
        }
      }
      if(element == 3){
        if(team == 2){
           pushMatrix();
             translate(position.x + 30,position.y);
             rotate(PI);
             image(mediumGreen,0,0);
           popMatrix();
        } else {
           pushMatrix();
             translate(position.x - 30,position.y);
             image(mediumGreen,0,0);
           popMatrix();
        }
        
      }
    }
    else if(type == 3){
      if(element == 1 ){
        if(team == 2){
           pushMatrix();
             translate(position.x + 38,position.y);
             rotate(PI);
             image(heavyBlue,0,0);
           popMatrix();
        } else {
           pushMatrix();
             translate(position.x - 38,position.y);
             image(heavyBlue,0,0);
           popMatrix();
        }
      }
      if(element == 2 ){
        if(team == 2){
           pushMatrix();
             translate(position.x + 38,position.y);
             rotate(PI);
             image(heavyRed,0,0);
           popMatrix();
        } else {
           pushMatrix();
             translate(position.x - 38,position.y);
             image(heavyRed,0,0);
           popMatrix();
        }
      }
      if(element == 3){
        if(team == 2){
           pushMatrix();
             translate(position.x + 38,position.y);
             rotate(PI);
             image(heavyGreen,0,0);
           popMatrix();
        } else {
           pushMatrix();
             translate(position.x - 38,position.y);
             image(heavyGreen,0,0);
           popMatrix();
        }
        
      }
    }
    }
    
  }
  
  public void assignID(int team, int lane, int element, int type){
    /**  Assigns a common id and a unique id for each new minion. There is a chance that minions may have identical uIDs should create an error catch for that
         Team:     1 = Player 1   // 2 = Player 2 //
         Lane:     1 = Lane 1    // 2 = Lane 2   // 3 = Lane 3 //
         Element:  1 = Water    // 2 = Fire     // 3 = Earth  //
         Type:     1 = Light   // 2 = Medium   // 3 = Heavy  //
     **/
     id += team * 1000;
     id += lane * 100;
     id += element * 10;
     id += type * 1;
     
     uID = (int) random(1000,2000);
  }
  
  public void assignStats(int t){ //assigns stats to newly created minion based on type
    if(type == 1){
      health = 2;
      atk = 2;
      spd = 3;
    }
    else if(type == 2){
      health = 4;
      atk = 4;
      spd = 2;
    }
    else if(type == 3){
      health = 6;
      atk = 6;
      spd = 1;
    }
  }
  
  public void getLaneRef(){ //adds all friendly minions to a refrence array called friendly minions
    friendlyMinions.removeAll(friendlyMinions);
      if (team == 1){
        for(Minion m : p1Minions){
          if(lane == m.lane && m.type == 1){
            friendlyMinions.add(m);
          }
        }
        for(Minion m : p1Minions){
              if(lane == m.lane && m.type == 2){
                friendlyMinions.add(m);
              }
        }
        for(Minion m : p1Minions){
              if(lane == m.lane && m.type == 3){
                friendlyMinions.add(m);
              }
        }
        for(Minion m : friendlyMinions){
           ////println(m.uID); 
        }
      }
      else if(team == 2){
        for(Minion m : p2Minions){
            if(lane == m.lane && m.type == 1){
              friendlyMinions.add(m);
              
            }
        }
        for(Minion m : p2Minions){
              if(lane == m.lane && m.type == 2){
                friendlyMinions.add(m);
              }
        }
        for(Minion m : p2Minions){
              if(lane == m.lane && m.type == 3){
                friendlyMinions.add(m);
              }
        }
        for(Minion m : friendlyMinions){
           ////println(m.uID); 
        }
      }
  }
  public void getEnemyRef(){ //gets a refrence of enemy minions and adds it to an arrayList();
      int dist;
      if(type == 4){ dist = 10; } else { dist = 5; }
      enemyMinions.removeAll(enemyMinions);
      if(team == 1){
        for(Minion m : p2Minions){
          if(!m.dead)
           enemyMinions.add(m);
         ////println(m.type);
        }
      }
      if(team == 2){
        for(Minion m : p1Minions){
          if(!deadP1Minions.contains(m)){
            enemyMinions.add(m);
          }
         ////println(m.type);
        }
      }
      for(Minion m : enemyMinions){
        if(team == 1){
           println("p1 position.y" + position.y);
           if(lane == m.lane &&  position.y - (m.position.y ) <= dist && !deadP2Minions.contains(m)){
             
             combat = true;
             combatTarget = m;
             println("combatTarget.position.y: " + combatTarget.position.y);
             //println("target aquired");
           }
        }
        if(team == 2){
           println("p2 position.y" + position.y);
           if(lane == m.lane &&  (m.position.y) - position.y <= dist && !deadP1Minions.contains(m)){
             //println(m.position.y);
             //println(position.y);
             //println(m.position.y - position.y);
             combat = true; 
             combatTarget = m;
             println("combatTarget.position.y: " + combatTarget.position.y);
             if(!m.dead && atk >= m.health || (atk*2 >= health && (element == m.element - 1 || (element == 1 && m.element == 3))) ){
               
               if(type == 3 || (type != 1 && m.type != 3)){
                 m.combatTarget = this;
                 
                 m.attack();
               }
             }
             //println("target aquired");
           }
           
        }
      }
  }
  public void checkTurn(){ //checks to see if it is this minions time to start moving forward
    int minCount = 0;
    for(Minion m : friendlyMinions){
       if(minCount == turn && m.uID == uID){
         println(uID + "'s turn");
         myTurn = true; 
       } else { //println(m.uID + " != " + uID); 
       minCount++; 
     }
    } 
  }
  public void move(){
   ////println(element + " is moving");
   if(team == 1)
      position.y -= spd;
    else if (team == 2)
      position.y += spd;  
  }
  public void attack(){ //damage calculations, takes damage from opponent.
    //println("Enemy Type: " + combatTarget.element);
    image(explosion, position.x - 50, position.y);
    if(( element == combatTarget.element + 1 ) || (element == 1 && combatTarget.element == 3)){
      //println("Enemy Type Advantage");
      health = health - (combatTarget.atk)*2;
    } else if ( ( element == combatTarget.element - 1 ) || (element == 3 && combatTarget.element == 1) ){
      //println("Enemy Type DisAdvantage");
      health = health - (combatTarget.atk)/2;
    }
    else {
      health = health - (combatTarget.atk); 
    }
  }
  public void checkHealth(){ //checks to see if the minion is dead
    if(team == 1){
       if(health <= 0){
          //println("p1 is dead enemy health: " + combatTarget.health);
          dead = true;
          
       }
    }
    else if(team == 2){
       if(health <= 0){
          //println("p2 is dead enemy health"  + combatTarget.health);
          dead = true;
       }
    }
  }
  public void checkBase(){ // checks to see if the minion has reached the enemy base
     if(team == 1){
        if(position.y <= 0){
           pushMatrix();
            translate(position.x + 50, position.y + 241);
            rotate(PI);
            image(blast,0, 0);
           popMatrix();
           p1BaseHealth -= atk/2;
           dead = true;
        }
     }
     if(team == 2){
        if(position.y >= height){
           pushMatrix();
            //rotate(PI);
            image(blast,position.x - 50, position.y - 241);
           popMatrix();
           p2BaseHealth -= atk/2;
           dead = true;
        }
     }
     
  }
}
/* ********************************************************************
Beyond The Keep - Prototype
Tower Class
LAST MODIFIED: February 06th 2014
CREATED BY: JAKE DEUGO, TYLER DINARDO, CHRIS HOSMAR & ZACHARY SULLIVAN
******************************************************************** */
class Tower{
  /** Local Variables **/
  Tower(){
   
  }
  public void update(){
    
  }
  public void render(){
    
  }
}

public void battlePhase(){
   
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



public void planningPhase(){
   
  
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
       text("END YOUR TURN", width - 200, height/2 + 37.5f);
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
       text("CONFIRM", 50+12.5f, 475+12.5f);
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
       text("END YOUR TURN", width - 200, height/2 - 12.5f);
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
         text("CONFIRM", 50+12.5f, 450+12.5f);
       popMatrix();
     }
   }
}

public void mousePressed(){
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

/***
This code is the result of my own incompetence, should've created a loadImage(); function in the minion class.
Lots of redundent code here, if I had more time I would adjust this. 
Basically this code creates little preview minions after you've made selections, gives you a sense of progress.

***/

public void previewMinions(int p1L1Count, int p1L2Count, int p1L3Count,int p2L1Count, int p2L2Count, int p2L3Count){
 if(player1Turn){
 for(Minion m : p1Minions){
     if(m.lane == 1){
        if(m.type == 1){
            if(m.element == 1){
               image(smallBlue,m.position.x - 22, height/2 + 25  + (25 * p1L1Count));
               p1L1Count++;
            }
            if(m.element == 2){
               image(smallRed,m.position.x - 22, height/2 + 25 + (25 * p1L1Count));
               p1L1Count++;
            }
            if(m.element == 3){
               image(smallGreen,m.position.x-22, height/2 + 25 + (25 * p1L1Count));
               p1L1Count++;
            }
         }
         if(m.type == 2){
            if(m.element == 1){
               image(mediumBlue,m.position.x - 30, height/2 + 25  + (25 * p1L1Count));
               p1L1Count++;
            }
            if(m.element == 2){
               image(mediumRed,m.position.x - 30, height/2 + 25 + (25 * p1L1Count));
               p1L1Count++;
            }
            if(m.element == 3){
               image(mediumGreen,m.position.x-30, height/2 + 25 + (25 * p1L1Count));
               p1L1Count++;
            }
         }
         if(m.type == 3){
            if(m.element == 1){
               image(heavyBlue,m.position.x - 38, height/2 + 25  + (25 * p1L1Count));
               p1L1Count++;
            }
            if(m.element == 2){
               image(heavyRed,m.position.x - 38, height/2 + 25  + (25 * p1L1Count));
               p1L1Count++;
            }
            if(m.element == 3){
               image(heavyGreen,m.position.x-38, height/2 + 25  + (25 * p1L1Count));
               p1L1Count++;
            }
         }
       }
       if(m.lane == 2){
        if(m.type == 1){
            if(m.element == 1){
               image(smallBlue,m.position.x - 22, height/2 + 25  + (25 * p1L2Count));
               p1L2Count++;
            }
            if(m.element == 2){
               image(smallRed,m.position.x - 22, height/2 + 25 + (25 * p1L2Count));
               p1L2Count++;
            }
            if(m.element == 3){
               image(smallGreen,m.position.x-22, height/2 + 25 + (25 * p1L2Count));
               p1L2Count++;
            }
         }
         if(m.type == 2){
            if(m.element == 1){
               image(mediumBlue,m.position.x - 30, height/2 + 25  + (25 * p1L2Count));
               p1L2Count++;
            }
            if(m.element == 2){
               image(mediumRed,m.position.x - 30, height/2 + 25 + (25 * p1L2Count));
               p1L2Count++;
            }
            if(m.element == 3){
               image(mediumGreen,m.position.x-30, height/2 + 25 + (25 * p1L2Count));
               p1L2Count++;
            }
         }
         if(m.type == 3){
            if(m.element == 1){
               image(heavyBlue,m.position.x - 38, height/2 + 25 + (25 * p1L2Count));
               p1L2Count++;
            }
            if(m.element == 2){
               image(heavyRed,m.position.x - 38, height/2 + 25 + (25 * p1L2Count));
               p1L2Count++;
            }
            if(m.element == 3){
               image(heavyGreen,m.position.x-38, height/2 + 25 + (25 * p1L2Count));
               p1L2Count++;
            }
         }
       }
       if(m.lane == 3){
        if(m.type == 1){
            if(m.element == 1){
               image(smallBlue,m.position.x - 22, height/2 + 25  + (25 * p1L3Count));
               p1L3Count++;
            }
            if(m.element == 2){
               image(smallRed,m.position.x - 22, height/2 + 25 + (25 * p1L3Count));
               p1L3Count++;
            }
            if(m.element == 3){
               image(smallGreen,m.position.x-22, height/2 + 25 + (25 * p1L3Count));
               p1L3Count++;
            }
         }
         if(m.type == 2){
            if(m.element == 1){
               image(mediumBlue,m.position.x - 30, height/2 + 25  + (25 * p1L3Count));
               p1L3Count++;
            }
            if(m.element == 2){
               image(mediumRed,m.position.x - 30, height/2 + 25 + (25 * p1L3Count));
               p1L3Count++;
            }
            if(m.element == 3){
               image(mediumGreen,m.position.x-30, height/2 + 25 + (25 * p1L3Count));
               p1L3Count++;
            }
         }
         if(m.type == 3){
            if(m.element == 1){
               image(heavyBlue,m.position.x - 38, height/2 + 25 + (25 * p1L3Count));
               p1L3Count++;
            }
            if(m.element == 2){
               image(heavyRed,m.position.x - 38, height/2  + 25 + (25 * p1L3Count));
               p1L3Count++;
            }
            if(m.element == 3){
               image(heavyGreen,m.position.x-38, height/2  + 25+ (25 * p1L3Count));
               p1L3Count++;
            }
         }
       }
     }
 }
     ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
 if(player2Turn){
  for(Minion m : p2Minions){
     if(m.lane == 1){
        if(m.type == 1){
            if(m.element == 1){
               pushMatrix();
               translate(m.position.x - 22, height/2 - 100  - (25 * p2L1Count));
              // rotate(PI);
              
               image(smallBlue,0,0);
               popMatrix();
               p2L1Count++;
            }
            if(m.element == 2){
              pushMatrix();
               translate(m.position.x - 22, height/2 - 100  - (25 * p2L1Count));
              // rotate(PI);
               image(smallRed,0,0);
               popMatrix();
               p2L1Count++;
            }
            if(m.element == 3){
              pushMatrix();
               translate(m.position.x-22, height/2 - 100 - (25 * p2L1Count));
              // rotate(PI);
               image(smallGreen,0,0);
               popMatrix();
               p2L1Count++;
            }
         }
         if(m.type == 2){
            if(m.element == 1){
               image(mediumBlue,m.position.x - 30, height/2 - 100  - (25 * p2L1Count));
               p2L1Count++;
            }
            if(m.element == 2){
               image(mediumRed,m.position.x - 30, height/2 - 100 - (25 * p2L1Count));
               p2L1Count++;
            }
            if(m.element == 3){
               image(mediumGreen,m.position.x-30, height/2 -100 - (25 * p2L1Count));
               p2L1Count++;
            }
         }
         if(m.type == 3){
            if(m.element == 1){
               image(heavyBlue,m.position.x - 38, height/2 - 100  - (25 * p2L1Count));
               p2L1Count++;
            }
            if(m.element == 2){
               image(heavyRed,m.position.x - 38, height/2 - 100 - (25 * p2L1Count));
               p2L1Count++;
            }
            if(m.element == 3){
               image(heavyGreen,m.position.x-38, height/2 - 100 - (25 * p2L1Count));
               p2L1Count++;
            }
         }
       }
       if(m.lane == 2){
        if(m.type == 1){
            if(m.element == 1){
               image(smallBlue,m.position.x - 22, height/2 - 100  - (25 * p2L2Count));
               p2L2Count++;
            }
            if(m.element == 2){
               image(smallRed,m.position.x - 22, height/2 - 100 - (25 * p2L2Count));
               p2L2Count++;
            }
            if(m.element == 3){
               image(smallGreen,m.position.x-22, height/2 -100 - (25 * p2L2Count));
               p2L2Count++;
            }
         }
         if(m.type == 2){
            if(m.element == 1){
               image(mediumBlue,m.position.x - 30, height/2 -100  - (25 * p2L2Count));
               p2L2Count++;
            }
            if(m.element == 2){
               image(mediumRed,m.position.x - 30, height/2 - 100 - (25 * p2L2Count));
               p2L2Count++;
            }
            if(m.element == 3){
               image(mediumGreen,m.position.x-30, height/2 - 100 - (25 * p2L2Count));
               p2L2Count++;
            }
         }
         if(m.type == 3){
            if(m.element == 1){
               image(heavyBlue,m.position.x - 38, height/2 - 100 - (25 * p2L2Count));
               p2L2Count++;
            }
            if(m.element == 2){
               image(heavyRed,m.position.x - 38, height/2 - 100 - (25 * p2L2Count));
               p2L2Count++;
            }
            if(m.element == 3){
               image(heavyGreen,m.position.x-38, height/2 - 100 - (25 * p2L2Count));
               p2L2Count++;
            }
         }
       }
       if(m.lane == 3){
        if(m.type == 1){
            if(m.element == 1){
               image(smallBlue,m.position.x - 22, height/2 - 100- (25 * p2L3Count));
               p2L3Count++;
            }
            if(m.element == 2){
               image(smallRed,m.position.x - 22, height/2 - 100 - (25 * p2L3Count));
               p2L3Count++;
            }
            if(m.element == 3){
               image(smallGreen,m.position.x-22, height/2 -100 - (25 * p2L3Count));
               p2L3Count++;
            }
         }
         if(m.type == 2){
            if(m.element == 1){
               image(mediumBlue,m.position.x - 30, height/2 - 100  - (25 * p2L3Count));
               p2L3Count++;
            }
            if(m.element == 2){
               image(mediumRed,m.position.x - 30, height/2 - 100 - (25 * p2L3Count));
               p2L3Count++;
            }
            if(m.element == 3){
               image(mediumGreen,m.position.x-30, height/2 - 100 - (25 * p2L3Count));
               p2L3Count++;
            }
         }
         if(m.type == 3){
            if(m.element == 1){
               image(heavyBlue,m.position.x - 38, height/2- 100 - (25 * p2L3Count));
               p2L3Count++;
            }
            if(m.element == 2){
               image(heavyRed,m.position.x - 38, height/2  - 100 - (25 * p2L3Count));
               p2L3Count++;
            }
            if(m.element == 3){
               image(heavyGreen,m.position.x-38, height/2  - 100 - (25 * p2L3Count));
               p2L3Count++;
            }
         }
       }
     }
 }
}
  
  /**
  for(Minion m : p1Minions){
      if(m.lane == 1){
        if(m.type == 1){
            if(m.element == 1){
               image(smallBlue,m.position.x - 22, height/2 + 25  + (25 * p1L1LightCount));
               p1L1LightCount++;
            }
            if(m.element == 2){
               image(smallRed,m.position.x - 22, height/2 + 25 + (25 * p1L1LightCount));
               p1L1LightCount++;
            }
            if(m.element == 3){
               image(smallGreen,m.position.x-22, height/2 + 25 + (25 * p1L1LightCount));
               p1L1LightCount++;
            }
         }
         if(m.type == 2){
            if(m.element == 1){
               image(mediumBlue,m.position.x - 30, height/2 + 25  + (25 * p1L1LightCount) + (25 * p1L1MediumCount));
               p1L1MediumCount++;
            }
            if(m.element == 2){
               image(mediumRed,m.position.x - 30, height/2 + 25 + (25 * p1L1LightCount) + (25 * p1L1MediumCount));
               p1L1MediumCount++;
            }
            if(m.element == 3){
               image(mediumGreen,m.position.x-30, height/2 + 25 + (25 * p1L1LightCount) + (25 * p1L1MediumCount));
               p1L1MediumCount++;
            }
         }
         if(m.type == 3){
            if(m.element == 1){
               image(smallBlue,m.position.x - 22, height/2 + (25 * p1L1LightCount) + (25 * p1L1MediumCount) + (25 * p1L1HeavyCount));
               p1L1LightCount++;
            }
            if(m.element == 2){
               image(smallRed,m.position.x - 22, height/2 + (25 * p1L1LightCount)  + (25 * p1L1MediumCount) + (25 * p1L1HeavyCount));
               p1L1LightCount++;
            }
            if(m.element == 3){
               image(smallGreen,m.position.x-22, height/2 + (25 * p1L1LightCount)  + (25 * p1L1MediumCount) + (25 * p1L1HeavyCount));
               p1L1LightCount++;
            }
         }
       }
  }
  
  
}**/
  static public void main(String[] passedArgs) {
    String[] appletArgs = new String[] { "--full-screen", "--bgcolor=#666666", "--stop-color=#cccccc", "BeyondTheKeepMain" };
    if (passedArgs != null) {
      PApplet.main(concat(appletArgs, passedArgs));
    } else {
      PApplet.main(appletArgs);
    }
  }
}
