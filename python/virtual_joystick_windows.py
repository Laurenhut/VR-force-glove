import serial
import struct
import sys
import pyxinput
import time
import struct
from win32api import GetKeyState
from win32con import VK_CAPITAL
# starts serial communications on the bluetooth port
ser = serial.Serial('COM15', 9600, timeout=1)
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
        # checks to see if the current capslock state is duplicated
        capslock_state = GetKeyState(VK_CAPITAL)
        if capslock_state != capslock_state_previous:
            ser.write(struct.pack('>B',capslock_state))
            capslock_state_previous = capslock_state
    # Allow program to be manually stopped
    except KeyboardInterrupt:
        print("Keyboard Interrupt")
        ser.close()
        sys.exit(0)
