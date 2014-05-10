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
  
  void update(){
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
  void render(){
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
             translate(position.x + 23.5,position.y);
             rotate(PI);
             image(smallBlue,0,0);
           popMatrix();
        } else {
           pushMatrix();
             translate(position.x - 23.5,position.y);
             image(smallBlue,0,0);
           popMatrix();
        }
      }
      if(element == 2 ){
        if(team == 2){
           pushMatrix();
             translate(position.x + 23.5,position.y);
             rotate(PI);
             image(smallRed,0,0);
           popMatrix();
        } else {
           pushMatrix();
             translate(position.x - 23.5,position.y);
             image(smallRed,0,0);
           popMatrix();
        }
      }
      if(element == 3){
        if(team == 2){
           pushMatrix();
             translate(position.x + 23.5,position.y);
             rotate(PI);
             image(smallGreen,0,0);
           popMatrix();
        } else {
           pushMatrix();
             translate(position.x - 23.5,position.y);
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
  
  void assignID(int team, int lane, int element, int type){
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
  
  void assignStats(int t){ //assigns stats to newly created minion based on type
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
  
  void getLaneRef(){ //adds all friendly minions to a refrence array called friendly minions
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
  void getEnemyRef(){ //gets a refrence of enemy minions and adds it to an arrayList();
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
  void checkTurn(){ //checks to see if it is this minions time to start moving forward
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
  void move(){
   ////println(element + " is moving");
   if(team == 1)
      position.y -= spd;
    else if (team == 2)
      position.y += spd;  
  }
  void attack(){ //damage calculations, takes damage from opponent.
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
  void checkHealth(){ //checks to see if the minion is dead
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
  void checkBase(){ // checks to see if the minion has reached the enemy base
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
