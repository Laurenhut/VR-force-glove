## Test reading data from a Bluetooth serial port
import serial
import time
import struct
import sys

# Set up serial port to COM5 (bluetooth module) with baudrate 115200
ser = serial.Serial('COM11', 9600, timeout=1)
print("\nConnected")

while(True):
    try:
        # Read serial data
        s = ser.readline()
        s = s.decode("utf-8")
        ser.write(b'hi')
        print(s)
    # Allow program to be manually stopped
    except KeyboardInterrupt:
        print("Keyboard Interrupt")
        ser.close()
        sys.exit(0)
