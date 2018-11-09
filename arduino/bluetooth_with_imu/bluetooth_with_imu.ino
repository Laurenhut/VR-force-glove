/*

 Hardware setup:
 MPU9250 Breakout --------- Arduino
 VDD ---------------------- 3.3V
 VDDI --------------------- 3.3V
 SDA ----------------------- A4
 SCL ----------------------- A5
 GND ---------------------- GND
 */

#include "quaternionFilters.h"
#include "MPU9250.h"
#define AHRS true         // Set to false for basic data read
#include <SoftwareSerial.h>  
#include <PWMServo.h>
PWMServo myservo;  // create servo object to control a servo

int pos = 0;    // variable to store the servo position
int bluetoothTx = 2;  // TX-O pin of bluetooth mate, Arduino D2
int bluetoothRx = 3;  // RX-I pin of bluetooth mate, Arduino D3
int blue = 0;
float value = 0.5;

SoftwareSerial bluetooth(bluetoothTx, bluetoothRx);

// Pin definitions
int intPin = 12;  // These can be changed, 2 and 3 are the Arduinos ext int pins
int myLed  = 13;  // Set up pin 13 led for toggling
const int FSR_PIN = A0; // Pin connected to FSR/resistor divider
const float VCC = 4.98; // Measured voltage of Ardunio 5V line
const float R_DIV = 3230.0; // Measured resistance of 3.3k resistor

MPU9250 myIMU;

void setup()
{
  myservo.attach(SERVO_PIN_A);  // attaches the servo on pin 9 to the servo object
  myservo.write(45); 
  Wire.begin();
  Serial.begin(9600);

  // setsup bluetooth communications
  bluetooth.begin(115200);  // The Bluetooth Mate defaults to 115200bps
  bluetooth.print("$");  // Print three times individually
  bluetooth.print("$");
  bluetooth.print("$");  // Enter command mode
  delay(100);  // Short delay, wait for the Mate to send back CMD
  bluetooth.println("U,9600,N");  // Temporarily Change the baudrate to 9600, no parity
  bluetooth.begin(9600);  // Start bluetooth serial at 9600

  // Set up the interrupt pin, its set as active high, push-pull
  pinMode(intPin, INPUT);
  digitalWrite(intPin, LOW);
  pinMode(myLed, OUTPUT);
  digitalWrite(myLed, HIGH);

  // input pin for the forse sensor 
  pinMode(FSR_PIN, INPUT);

  // Read the WHO_AM_I register, this is a good test of communication
  byte c = myIMU.readByte(MPU9250_ADDRESS, WHO_AM_I_MPU9250);
//  Serial.print("MPU9250 "); Serial.print("I AM "); Serial.print(c, HEX);
//  Serial.print(" I should be "); Serial.println(0x71, HEX);

  if (c == 0x71) // WHO_AM_I should always be 0x68
  {
//    Serial.println("MPU9250 is online...");

    // Calibrate gyro and accelerometers, load biases in bias registers
    myIMU.calibrateMPU9250(myIMU.gyroBias, myIMU.accelBias);

    myIMU.initMPU9250();
    // Initialize device for active mode read of acclerometer, gyroscope, and
    // temperature
//    Serial.println("MPU9250 initialized for active data mode....");

    // Read the WHO_AM_I register of the magnetometer, this is a good test of
    // communication
    byte d = myIMU.readByte(AK8963_ADDRESS, WHO_AM_I_AK8963);
//    Serial.print("AK8963 "); Serial.print("I AM "); Serial.print(d, HEX);
//    Serial.print(" I should be "); Serial.println(0x48, HEX);
    // Get magnetometer calibration from AK8963 ROM
    myIMU.initAK8963(myIMU.magCalibration);
    // Initialize device for active mode read of magnetometer
//    Serial.println("AK8963 initialized for active data mode....");
    //Serial.println(0);

   
  } // if (c == 0x71)
  else
  {
    Serial.print("Could not connect to MPU9250: 0x");
    Serial.println(c, HEX);
    while(1) ; // Loop forever if communication doesn't happen
  }

  // initializes the xervo on pin 9 and put it into the freewheeling position

}

float force_sensor_value(){
  //checks the force on the force sensor and returns the result 
  
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
//      Serial.println("Force: " + String(force) + " g");
//      Serial.println();
      return force; 
    }
    else{
      return 0; 
      }
}

float imu_data(){
    // checks the value of the imu raw
  
  // If intPin goes high, all data registers have new data
  // On interrupt, check if data ready interrupt
  if (myIMU.readByte(MPU9250_ADDRESS, INT_STATUS) & 0x01)
  {  
    myIMU.readAccelData(myIMU.accelCount);  // Read the x/y/z adc values
    myIMU.getAres();

    // Now we'll calculate the accleration value into actual g's
    // This depends on scale being set
    myIMU.ax = (float)myIMU.accelCount[0]*myIMU.aRes; // - accelBias[0];
    myIMU.ay = (float)myIMU.accelCount[1]*myIMU.aRes; // - accelBias[1];
    myIMU.az = (float)myIMU.accelCount[2]*myIMU.aRes; // - accelBias[2];

    myIMU.readGyroData(myIMU.gyroCount);  // Read the x/y/z adc values
    myIMU.getGres();

    // Calculate the gyro value into actual degrees per second
    // This depends on scale being set
    myIMU.gx = (float)myIMU.gyroCount[0]*myIMU.gRes;
    myIMU.gy = (float)myIMU.gyroCount[1]*myIMU.gRes;
    myIMU.gz = (float)myIMU.gyroCount[2]*myIMU.gRes;

    myIMU.readMagData(myIMU.magCount);  // Read the x/y/z adc values
    myIMU.getMres();
    // User environmental x-axis correction in milliGauss, should be
    // automatically calculated
    myIMU.magbias[0] = +470.;
    // User environmental x-axis correction in milliGauss TODO axis??
    myIMU.magbias[1] = +120.;
    // User environmental x-axis correction in milliGauss
    myIMU.magbias[2] = +125.;

    // Calculate the magnetometer values in milliGauss
    // Include factory calibration per data sheet and user environmental
    // corrections
    // Get actual magnetometer value, this depends on scale being set
    myIMU.mx = (float)myIMU.magCount[0]*myIMU.mRes*myIMU.magCalibration[0] -
               myIMU.magbias[0];
    myIMU.my = (float)myIMU.magCount[1]*myIMU.mRes*myIMU.magCalibration[1] -
               myIMU.magbias[1];
    myIMU.mz = (float)myIMU.magCount[2]*myIMU.mRes*myIMU.magCalibration[2] -
               myIMU.magbias[2];
  } // if (readByte(MPU9250_ADDRESS, INT_STATUS) & 0x01)

  // converts from quaternians to euler
  
  // Must be called before updating quaternions!
  myIMU.updateTime();
  
//  MadgwickQuaternionUpdate(ax, ay, az, gx*PI/180.0f, gy*PI/180.0f, gz*PI/180.0f,  my,  mx, mz);
  MahonyQuaternionUpdate(myIMU.ax, myIMU.ay, myIMU.az, myIMU.gx*DEG_TO_RAD,
                         myIMU.gy*DEG_TO_RAD, myIMU.gz*DEG_TO_RAD, myIMU.my,
                         myIMU.mx, myIMU.mz, myIMU.deltat);

    // Serial print and/or display at 0.5 s rate independent of data rates
    myIMU.delt_t = millis() - myIMU.count;

    // update LCD once per half-second independent of read rate
    if (myIMU.delt_t > 500)
    {
   
      myIMU.yaw   = atan2(2.0f * (*(getQ()+1) * *(getQ()+2) + *getQ() *
                    *(getQ()+3)), *getQ() * *getQ() + *(getQ()+1) * *(getQ()+1)
                    - *(getQ()+2) * *(getQ()+2) - *(getQ()+3) * *(getQ()+3));
      myIMU.pitch = -asin(2.0f * (*(getQ()+1) * *(getQ()+3) - *getQ() *
                    *(getQ()+2)));
      myIMU.roll  = atan2(2.0f * (*getQ() * *(getQ()+1) + *(getQ()+2) *
                    *(getQ()+3)), *getQ() * *getQ() - *(getQ()+1) * *(getQ()+1)
                    - *(getQ()+2) * *(getQ()+2) + *(getQ()+3) * *(getQ()+3));
      myIMU.pitch *= RAD_TO_DEG;
      myIMU.yaw   *= RAD_TO_DEG;


      myIMU.yaw   -= 8.5;
      myIMU.roll  *= RAD_TO_DEG;

//      Serial.print("yaw: ");
//      Serial.println(myIMU.yaw, 2);
    //Serial.print("pitch: ");
    //Serial.println(myIMU.pitch+180, 2);
//      Serial.print("roll: ");
//      Serial.println(myIMU.roll, 2);
//      Serial.println(" ");
      
//      //sends a value through bluetooth
//      bluetooth.println(myIMU.pitch);
      
      myIMU.count = millis();
      myIMU.sumCount = 0;
      myIMU.sum = 0;
    } // if (myIMU.delt_t > 500)
    return myIMU.pitch+180;
  }

void  change_motor(int current_position){
     if (current_position == 1){
        for (pos = 45; pos <= 110; pos += 1) { // goes from 0 degrees to 180 degrees
        // in steps of 1 degree
        myservo.write(pos);              // tell servo to go to position in variable 'pos'
        delay(15);                       // waits 15ms for the servo to reach the position
        }
      }
     else{
                 for (pos = 110; pos >= 45; pos -= 1) { // goes from 0 degrees to 180 degrees
        // in steps of 1 degree
        myservo.write(pos);              // tell servo to go to position in variable 'pos'
        delay(15);                       // waits 15ms for the servo to reach the position
        }
      }

  }
  struct Data {
  float bottom, top;
};
struct Data  calibrate(){
    int var = 0; 
    int var2 = 0; 
    float average_bottom = 0;
    float average_top = 0;
    Serial.println("calibrating hold for 5s");
    while (var< 2000){
      average_bottom = average_bottom+ imu_data();
      delay(1);
      var++;

      }
      Serial.println("hold in top position");
      delay(5000);
      while (var2< 2000){
      average_top = average_top+ imu_data();
      delay(1);
      var2++;

      }
      Serial.println("done calibration results:");
      average_bottom = average_bottom/2000;
      average_top = average_top/2000;
      return {average_bottom, average_top};
  }
bool start = true; 
float imu_offset;
struct Data stuff;
void loop()
{
  if (start == true){
      stuff =calibrate(); 
      Serial.println(stuff.bottom);
      Serial.println(stuff.top);
      imu_offset =imu_data();
      Serial.println("sample calculation");
      Serial.println(imu_offset);
      Serial.println((imu_offset- stuff.bottom)/(stuff.top-stuff.bottom));
      start =false;
    }
  // checks if the bluetooth sent any characters
    if(bluetooth.available())  // If ths bluetooth sent any characters
  {
    // prints out the values sent by the bluetooth module
    blue = bluetooth.read();
    change_motor(blue);
    //bluetooth.println(5);  
       //sends commsnd to lock the glove locks the glove
  }
//  //sends a value through bluetooth
//   //bluetooth.println(force_sensor_value());
//   Serial.println(imu_data());
    Serial.println((imu_data()- stuff.bottom)/(stuff.top-stuff.bottom));
   bluetooth.println((imu_data()- stuff.bottom)/(stuff.top-stuff.bottom) );
   delay(10);

}
