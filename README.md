VR Force glove
====
This is the repository for a locking vr force glove. This readme will go over how to assemble and run the software needed to make the glove work. To find more information on the design an build process see the post made on my portfolio made [here]()
***

setup and use guide
=======
 parts needed
---
* 3d printer
* arduino nano
* load cell
* hobbyist servo motor
* 1/8" hand ratchet
* blue smirf silver Bluetooth module
* potentiometor  
* m2 and m5 screws
* linear slide and carriage
* x gauge wire

assembling the glove
---

1. print the stl files located in the stl folder
2. slide the bottom half of the strain gauge into the thumb piece as shown. and tighten the bottom bolt  
3. slip the 1/4" hand ratchet into the opening of the index_attachment and push the ratchet head into the corresponding opening on the side of the body piece
4. place the control_lever over the ratchet head and make sure that the lever is facing upwards and can swing clockwise
  - if the ratchet is in the incorrect orientation return to step 3 and reorient it
5. put the control_lever through the center hole of the ratchet_cap
6. place the potentiometor into the pot_casing piece and put the knob of the potentiometor into the ratchet_cap piece
7. slide the pot_casing price into its slots on the handle piece as well as sliding the ratchet_cap over the ratchet
8. make sure that the control_lever is able to flick the locking direction of the ratchet when the cap on top
9. place a small piece of glue or tape onto the ratchet_cap to lock it into place on the ratchet_case
10. slide the control rod into the hole on the end of the control_lever and the hole on the end of the hobby servo horn.
11. place the hobby servo into the
10. place the index_ring_adapter over the linear slide carriage and attach using m2 screws
11. test to make sure the index_ring_adapter and slide are working correctly
  - note: you may have to angle the linear slide slightly to make finger actuation more comfortable
12. screw the puck_mount onto the vive puck
13. place the puck_mount onto the back of the handle piece
14. slide the stabilization rods into the holes of the puck_mount. and place the caps over the rods to make sure they don't slide out

Running the software
---
0. assemble the circuit shown in diagram 1
1. upload the program to the arduino nano and open the serial monitor
2. follow the steps for calibration
  - note: the bottom number should be smaller than the top number. if these values are reversed check your potentiometor
3. pair your blue smirf with your windows computer
4. start Steam VR, and make sure your Vive is properly setup and the vive puck is paired
5. open up the unity project
6. run the python script " <insert python script>"
  - note: in steam vr a game pad controller icon should appear
7. run the unity project
8. the device should be working now grab something and test it
