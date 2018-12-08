/* Sweep
 by BARRAGAN <http://barraganstudio.com>
 This example code is in the public domain.

 modified 8 Nov 2013
 by Scott Fitzgerald
 http://www.arduino.cc/en/Tutorial/Sweep
*/

#include <Servo.h>

Servo myservo;  // create servo object to control a servo
// twelve servo objects can be created on most boards

int pos = 0;    // variable to store the servo position
const int leftbuttonpin = 2 ;
const int rightbuttonpin = 3; 
int stateleft = 0; 
int stateright = 0; 
bool triggered =false; 
void setup() {

  myservo.attach(9);  // attaches the servo on pin 9 to the servo object
  pinMode(rightbuttonpin,INPUT);
  pinMode(leftbuttonpin,INPUT);
  myservo.write(120); 
 //myservo.write(150); 
}

void loop() {
//  stateright = digitalRead(rightbuttonpin);
//  stateleft = digitalRead(leftbuttonpin);
//
//  if (stateright == HIGH){
//      if (triggered == false){
//        for (pos = 45; pos <= 70; pos += 1) { // goes from 0 degrees to 180 degrees
//              // in steps of 1 degree
//              myservo.write(pos);              // tell servo to go to position in variable 'pos'
//              delay(15);                       // waits 15ms for the servo to reach the position
//        }
//        triggered =true;
//      }
//    }
//  else if (stateleft == HIGH){
//        if (triggered == false){
//           for (pos = 70; pos >= 45; pos -= 1) { // goes from 0 degrees to 180 degrees
//                // in steps of 1 degree
//                myservo.write(pos);              // tell servo to go to position in variable 'pos'
//                delay(15);                       // waits 15ms for the servo to reach the position
//            }
//        }
//        triggered =true;
//    }
//  else if (stateleft == LOW && stateright == LOW){
//         triggered = false;
//    }
 
}
