import uinput
import time
def main():
    # the bluetoothe module will feed into this controller script
    # this controller script will then
    #useful commands  evtest
    #usefull command #2 sudo jstest /dev/input/js0
    events = (
        uinput.BTN_JOYSTICK, # maybe the force sensor values
        uinput.ABS_X+(0, 0, 0, 0), # accelerometor values
        uinput.ABS_Y + (0, 0, 0, 0),
        )
    x_axis = 0
    y_axis = 0
    ans= "g"
    with uinput.Device(events) as device:
        while ans != "e":
            ans = raw_input("press a button: a, s, d, f ")
            print (ans)
            if ans == "a":
                while True:
                    x_axis-=1
                    device.emit(uinput.ABS_X,x_axis)
                    #the acceleromitor values will go here
                    time.sleep(0.01)
            elif ans == 'd':
                x_axis+=1
                device.emit(uinput.ABS_X, x_axis)
            elif ans =='w':
                y_axis+=1
                device.emit(uinput.ABS_Y, y_axis)
            elif ans =='s':
                y_axis-=1
                device.emit(uinput.ABS_Y, y_axis)
            elif ans =='e':
                print("end")


if __name__ == "__main__":
    main()

# import time
#
# import uinput
#
# def main():
#     events = (
#         uinput.REL_X,
#         uinput.REL_Y,
#         uinput.BTN_LEFT,
#         uinput.BTN_RIGHT,
#         )
#
#     with uinput.Device(events) as device:
#         for i in range(20):
#             # syn=False to emit an "atomic" (5, 5) event.
#             device.emit(uinput.REL_X, 5, syn=False)
#             device.emit(uinput.REL_Y, 5)
#
#             # Just for demonstration purposes: shows the motion. In real
#             # application, this is of course unnecessary.
#             time.sleep(0.01)
#
# if __name__ == "__main__":
#     main()
