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

void setup(){
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

void draw(){
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

void keyPressed(){ // Key based interaction

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
