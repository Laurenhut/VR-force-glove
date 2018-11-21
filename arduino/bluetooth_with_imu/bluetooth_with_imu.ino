/*

 Hardware setup:
 pot --------- Arduino
 VDD ---------------------- 3.3V
 VDDI --------------------- 3.3V
 SDA ----------------------- A4
 SCL ----------------------- A5
 GND ---------------------- GND

 Bluetooth 
 Motor 
 
 */

#define AHRS true         // Set to false for basic data read
#include <SoftwareSerial.h>
#include <PWMServo.h>
PWMServo myservo;  // create servo object to control a servo
// potentiometor 
int potPin = 2;

// variable to store the servo position
int pos = 0;    

// bluetooth pins
int bluetoothTx = 2;  // TX-O pin of bluetooth mate, Arduino D2
int bluetoothRx = 3;  // RX-I pin of bluetooth mate, Arduino D3
int blue = 0;
float value = 0.5;

SoftwareSerial bluetooth(bluetoothTx, bluetoothRx);

// Pin definitions
const int FSR_PIN = A0; // Pin connected to FSR/resistor divider
const float VCC = 4.98; // Measured voltage of Ardunio 5V line
const float R_DIV = 3230.0; // Measured resistance of 3.3k resistor

void setup()
{
  myservo.attach(SERVO_PIN_A);  // attaches the servo on pin 9 to the servo object
  myservo.write(45);
 
  Serial.begin(9600);

  // setsup bluetooth communications
  bluetooth.begin(115200);  // The Bluetooth Mate defaults to 115200bps
  bluetooth.print("$");  // Print three times individually
  bluetooth.print("$");
  bluetooth.print("$");  // Enter command mode
  delay(100);  // Short delay, wait for the Mate to send back CMD
  bluetooth.println("U,9600,N");  // Temporarily Change the baudrate to 9600, no parity
  bluetooth.begin(9600);  // Start bluetooth serial at 9600

  // input pin for the forse sensor
  pinMode(FSR_PIN, INPUT);

}

//checks the force on the force sensor and returns the result
float force_sensor_value(){

    int fsrADC = analogRead(FSR_PIN);
    if (fsrADC != 0) // If the analog reading is non-zero
    {
      // Use ADC reading to calculate voltage:
      float fsrV = fsrADC * VCC / 1023.0;
      // Use voltage and static resistor value to
      // calculate FSR resistance:
      float fsrR = R_DIV * (VCC / fsrV - 1.0);
      // Guesstimate force based on slopes in figure 3 of
      // FSR datasheet:
      float force;
      float fsrG = 1.0 / fsrR; // Calculate conductance
      // Break parabolic curve down into two linear slopes:
      if (fsrR <= 600)
        force = (fsrG - 0.00075) / 0.00000032639;
      else
        force =  fsrG / 0.000000642857;
      return force;
    }
    else{

      return 0;
      }
}

// read the value from the sensor
float pot_data()
{
    float val = analogRead(potPin);

    return val;
  }

void  change_motor(int current_position)
{
      // freewheeling position
     if (current_position == true)
     {
        myservo.write(110);
      }
     //locked position
     else
     {
        myservo.write(45);

      }

  }

struct Data
  {
    int bottom, top; 
  };

struct Data calibrate()
  {
    int var =0;
    int var2 = 0; 
    int bottom, top; 
     
    Serial.println("calibrating hold at bottom for 5s");
           delay(5000); 
  
    bottom = pot_data();  
  
    Serial.println("calibrating hold at top for 5s");
    delay(5000); 
    
    top = pot_data(); 
    Serial.println("done calibrating");
    return {bottom, top};
  }

bool glove_locked = false;
float frozen_position; 
struct Data cal;
bool start = true; 
float position_pot; 

void loop()
{
    if (start == true){  
      cal =calibrate();   
      Serial.println(cal.bottom); 
      Serial.println(cal.top);  
      start =false; 
    }
   
  // checks if the bluetooth sent signal to lock the glove
    if(bluetooth.available())  // If ths bluetooth sent any characters
  {
    // prints out the values sent by the bluetooth module
    blue = bluetooth.read();
    Serial.println(char(blue));
    
    if(char(blue) == "1")
      {
        Serial.println("locking: ");
        glove_locked = true; 
  
      }
    else if (char(blue)== "0")
      {
        Serial.println("unlocking ");
        glove_locked = false; 
      }

    else
      {
         switch (glove_locked) 
         {
          case true:
            Serial.println("unlocking ");
            glove_locked = false;
            break;
          case false: 
            Serial.println("locking ");
            glove_locked = true;
            break;
          }
    
      }

    change_motor(glove_locked);

       //sends command to lock the glove locks the glove
  }


   //locks the position of the fingers and will not update it until the force sensor reads correctly 
  if (glove_locked == true)
    {
      float finger_force = force_sensor_value();
      if (finger_force >10.0)
        {
          bluetooth.println(frozen_position);
          Serial.print("pos frozen: ");
          Serial.print(frozen_position);
          Serial.print(" force: ");
          Serial.println(finger_force);
        }
      // if your not puhing on the force sensor then it will begin updating as normal
      else if (finger_force <10.0)
        {
          position_pot= (pot_data()-cal.bottom)/(cal.top-cal.bottom);
          Serial.print("pos locked: ");
          Serial.println(position_pot);
          bluetooth.println(position_pot);
        }

    }
   //sends the position of the fingers through bluetooth as freewheeling
   else
   {
        position_pot= (pot_data()-cal.bottom)/(cal.top-cal.bottom);
        bluetooth.println(position_pot);
    }
   frozen_position =  position_pot; 
   delay(50);

}
