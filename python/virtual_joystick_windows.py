import serial
import struct
import sys
import pyxinput
import time
import struct
from win32api import GetKeyState
from win32con import VK_CAPITAL
# starts serial communications on the bluetooth port
ser = serial.Serial('COM17', 9600, timeout=10)
print("\nConnected")

MyVirtual = pyxinput.vController()
MyRead = pyxinput.rController(1) # For Controller 1
inp ="a"
capslock_state_previous = 0
count = 1

while(True):
    try:

        # Read serial data
        s = ser.readline()
        q = s.decode("utf-8")
        if q !='' and q != 0:
            thumb = float(s)
            if thumb < 0.0:
                thumb = 0.0
            elif thumb > 1.0:
                thumb = 1.0
            #print(((thumb/4)+.75))

            MyVirtual.set_value('AxisLy', ((thumb/4)+.75)*-1)
            #print(MyRead.gamepad)
        # checks to see if the current capslock state is duplicated
        capslock_state = GetKeyState(VK_CAPITAL)
        if capslock_state != capslock_state_previous and capslock_state >= 0:
            print("sending")
            print(capslock_state)
            if capslock_state == 1:
                ser.write("1".encode())
            elif capslock_state == 0:
                ser.write("0".encode())
            # time.sleep(1)
            # J = ser.readline()
        capslock_state_previous = capslock_state


    # Allow program to be manually stopped
    except KeyboardInterrupt:
        print("Keyboard Interrupt")
        ser.close()
        sys.exit(0)
