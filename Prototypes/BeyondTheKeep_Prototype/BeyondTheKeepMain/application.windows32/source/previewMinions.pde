
/***
This code is the result of my own incompetence, should've created a loadImage(); function in the minion class.
Lots of redundent code here, if I had more time I would adjust this. 
Basically this code creates little preview minions after you've made selections, gives you a sense of progress.

***/

void previewMinions(int p1L1Count, int p1L2Count, int p1L3Count,int p2L1Count, int p2L2Count, int p2L3Count){
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
