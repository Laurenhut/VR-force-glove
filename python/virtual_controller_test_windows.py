# import pyxinput
#
# #pyxinput.test_virtual()
# pyxinput.test_read()
import pyxinput
import time

MyVirtual = pyxinput.vController()
MyRead = pyxinput.rController(1) # For Controller 1
inp ="a"
while inp != "e":

    if inp == "a":

        MyVirtual.set_value('AxisLx', 0.5)

        inp ="s"
    elif inp == "s":

        MyVirtual.set_value('AxisLx', -32767)

        inp ="a"

    print(MyRead.gamepad)
    print(MyRead.buttons)
    time.sleep(2)
