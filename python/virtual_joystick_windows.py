import serial
import struct
import sys
import pyxinput
import time
import struct
# starts serial communications on the bluetooth port
ser = serial.Serial('COM11', 9600, timeout=1)
print("\nConnected")

MyVirtual = pyxinput.vController()
MyRead = pyxinput.rController(1) # For Controller 1
inp ="a"
while(True):
    try:
        # Read serial data
        s = ser.readline()
        # s = s.decode("utf-8")

        if s !=" ":
            thumb = float(s)/100
            print(thumb)
            MyVirtual.set_value('AxisLx', thumb)
            print(MyRead.gamepad)
        ser.write(struct.pack('>B',7))
        # if inp == "a":
        #     MyVirtual.set_value('AxisLx', thumb)
        #     inp ="s"
        # elif inp == "s":
        #     MyVirtual.set_value('AxisLx', -32767)
            # inp ="a"
        # print(MyRead.buttons)
    # Allow program to be manually stopped
    except KeyboardInterrupt:
        print("Keyboard Interrupt")
        ser.close()
        sys.exit(0)
