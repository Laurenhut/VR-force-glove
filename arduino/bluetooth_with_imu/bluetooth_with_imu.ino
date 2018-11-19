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
  myservo.write(50);
 
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
    return val/1023.0;
  }

void  change_motor(int current_position)
{
      // freewheeling position
     if (current_position == 1)
     {
        for (pos = 45; pos <= 110; pos += 1) 
        {
          myservo.write(pos);
          delay(15); 
        }
      }
     //locked position
     else
     {
        for (pos = 110; pos >= 45; pos -= 1) 
        {
          myservo.write(pos);
          delay(15);
        }
      }

  }


bool glove_locked = false;
float frozen_position; 
void loop()
{

  // checks if the bluetooth sent signal to lock the glove
    if(bluetooth.available())  // If ths bluetooth sent any characters
  {
    // prints out the values sent by the bluetooth module
    blue = bluetooth.read();
    change_motor(blue);
    
    // if the motor is locked pay attention to the force sensor 
    if(blue != 1)
    {
      glove_locked = true; 
      pot_data() = frozen_position; 
    }

       //sends command to lock the glove locks the glove
  }

   //locks the position of the fingers and will not update it until the force sensor reads correctly 
  if (glove_locked == true){
      if (force_sensor_value()>10){
              bluetooth.println(frozen_position);
        }
      // if your not puhing on the force sensor then it will begin updating as normal
      else {
          glove_locked == false; 
          bluetooth.println(pot_data());
        }
    }
   //sends the position of the fingers through bluetooth as freewheeling
   else{
       bluetooth.println(pot_data());
    }
   delay(10);

}
