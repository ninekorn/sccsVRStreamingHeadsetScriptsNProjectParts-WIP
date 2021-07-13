# SPDX-FileCopyrightText: 2021 ladyada for Adafruit Industries
# SPDX-License-Identifier: MIT

"""
Be sure to check the learn guides for more usage information.

This example is for use on (Linux) computers that are using CPython with
Adafruit Blinka to support CircuitPython libraries. CircuitPython does
not support PIL/pillow (python imaging library)!

Author(s): Melissa LeBlanc-Williams for Adafruit Industries
"""
#https://github.com/jake-walker/st7735-python

#sccoresystems by ninekorn

#from PIL import Image
from PIL import Image, ImageDraw, ImageFont

import ST7735
import math
import time

BORDER = 14 #20
FONTSIZE = 16 #24

# Create TFT LCD display class.
disp = ST7735.ST7735(port=0, cs=0, dc=24, backlight=None, rst=25, width=128, height=160, rotation=0, invert=False, offset_left=0, offset_top=0)

#disp = ST7735.ST7735(
#    port=0,
#    cs=0,  # BG_SPI_CSB_BACK or BG_SPI_CS_FRONT #ST7735.BG_SPI_CS_FRONT
#    dc=24,
#    rst=25,
#    width=128,
#    height=160,
#    backlight=None,               # 18 for back BG slot, 19 for front BG slot.
#    rotation=0,
#    spi_speed_hz=4000000, #https://github.com/pimoroni/st7735-python/blob/master/examples/image.py    invert=False,
#    offset_left=0,
#    offset_top=0
#)

# Initialize display.
disp.begin()

width = disp.width
height = disp.height

image = Image.new("RGB", (width, height))
disp.display(image)


# Load an image.
print('Loading image...')
path = '/home/kali/Desktop/raspberriPiSetupNImage/'
#image = Image.open(path + 'blinka.jpg')
#image = Image.open(path + '8e3a980f0f964cc539b4cbbba2654bb660db6f52_arduino-uno-pinout-diagram.jpeg')

image = Image.open(path + 'R-Pi-4-GPIO-Pinout.webp')
#image = Image.open('raspio-portsplus.jpg')

# Resize the image and rotate it so matches the display.
#image = image.rotate(90).resize((128,160))

image_ratio = image.width / image.height
screen_ratio = (width / height)

if screen_ratio < image_ratio:
    scaled_width = image.width * height // image.height
    scaled_height = height
else:
    scaled_width = width
    scaled_height = image.height * width // image.width

somewidth= image.width
someheight = image.height

#image = image.rotate(0).resize((scaled_width, scaled_height), Image.BICUBIC)
image = image.resize((int(width),int(height/image_ratio)), Image.BICUBIC)

x = (scaled_width // 2)- (width // 2)
y = (scaled_height // 2)- (height // 2)

#160/596 = 0.268456
someratioh = math.floor((height/someheight)*10)/10
someratioh = math.floor(someratioh * 10)/10;

#128/
someratiow = math.floor((width/somewidth)*10)/10
someratiow = math.floor(someratiow * 10)/10;

#image.width = 800
#image.height = 596

#596-160 = 436 => 436*0.8 = 348.8 / 2 = 219
#someremainsheight = (abs(someheight - height) * screen_ratio) / 2

#596-160 = 436 => 436*0.8 = 348.8 / 2 = 219
someremainsheight = (abs(someheight - height) * someratioh) / (someheight/width)
someremainswidth = (abs(somewidth - width) * someratiow) / (somewidth/height)

#x =(width // 2)
#y = (height // 2)
#image = image.crop((y, x, y + height, x + width))
image = image.crop((0, -someremainsheight, 0 + (width), -someremainsheight + (height)))

image = image.rotate(0)

#image = image.rotate(0)
#image = image.rotate(90).resize((scaled_height, scaled_width))
#image = image.rotate(90).resize((scaled_height, scaled_width), Image.BICUBIC)
#image = image.rotate(90).resize((height, width), Image.BICUBIC)
#image = image.rotate(90).resize((scaled_width, scaled_height), Image.BICUBIC)
#image = image.rotate(90).resize((width,height))

#text = str(scaled_width)

# Crop and center the image
#x = (scaled_width // 8) - (width // 8)
#y = (scaled_height // 8) - (height // 8)
#image = image.crop((x, y, x + (width), y + (height)))

# Draw the image on the display hardware.

print('Drawing image')

draw = ImageDraw.Draw(image)

font = ImageFont.truetype("/usr/share/fonts/truetype/dejavu/DejaVuSans.ttf", FONTSIZE)

# Draw Some Text
#text = "Hello World!"
#text = "/w:" + str(scaled_width) + "/h:" + str(scaled_height) +  "/x:" + str(x) + "/y:" + str(y)

#text = "width" + str(somewidth)
#text = "height" + str(someheight)

#text = "RATIO" + str(someratioh)

#(font_width, font_height) = font.getsize(text)

#draw.text(
#    (width // 2 - font_width // 2, height // 2 - font_height // 2),
#    text,
#    font=font,
#    fill=(255, 255, 0),
#)

#disp.display(image)

somecounter = 0
somecounterMax = 1

somecounterForDegree = 0
somecounterForDegreeMax = 360

someswtc = True

someoriginalimage = image;

originalsizex = image.width
originalsizey = image.height

somecounter1 = 0
somecounterMax1 = 1

somecounterForzoomFinal = 0
somecounterForzoom = 1
somecounterForzoomSwtc = 0
somecounterForzoomMaxClose = 5
somecounterForzoomMaxFar = 5
somepercentfinal = 1
somecounterForzoomMax = 3



somewidth= image.width
someheight = image.height
    
sizevar = 1;

    
while someswtc == True:
    
    if somecounter >= somecounterMax:
        if somecounterForDegree >= somecounterForDegreeMax:
            
            somecounterForDegree = 0
            
        #image = image.crop((0, -someremainsheight, 0 + (width), -someremainsheight + (height)))
        #image = someoriginalimage.rotate(somecounterForDegree)
        #GOOD#image = someoriginalimage.rotate(somecounterForDegree) 
        #GOOD#disp.display(image) #GOOD#
        
        somecounterForDegree += 1      
        somecounter = 0
    
    somecounter += 1
    
    
    
    
    somepercentfinal = 2
    
    somewidth= image.width #160 
    someheight = image.height #128 
    
    #somewidth= someoriginalimage.width #160 
    #someheight = someoriginalimage.height #128
    
    #if image.width < someoriginalimage.width*0.5:
    #    somewidth*=2
    #    someheight*=2
    #somewidth /= 2
    #someheight /= 2
    
    #if image.width < someoriginalimage.width*0.5 or image.height < someoriginalimage.height*0.5:
    #    someotherw*=2
    #    someotherh*=2
    
    #if somecounter1 >= somecounterMax1:       
    #    somecounter1 = 0       
    #    somecounter1 += 1
      
    if somecounterForzoomSwtc == 50:
        
        if somecounterForzoom < somecounterForzoomMaxClose:              
            someotherw = int(math.floor(somewidth / somepercentfinal))
            someotherh = int(math.floor(someheight / somepercentfinal))
            
            #(abs(128-64) * 0.1) / (64/128)
            #someremainsheightTwo = (abs(someotherh - someheight) * someratiow)# / (someotherh/someheight)
            #someremainswidthTwo  = (abs(someotherw - somewidth) * someratioh)# / (someotherw/somewidth)
            
            somefinalheight = (someotherh / 4) #someotherw / 2.75
            somefinalwidth = (someotherw / 2)
                  
            image = someoriginalimage;
            image = image.resize((int(someotherw),int(someotherh)))
            image = image.crop(((somefinalwidth * -1), (someremainsheight + somefinalheight) * -1, (somefinalwidth*-1) + (width), ((someremainsheight + somefinalheight) * -1) + (height)))
            disp.display(image)
        else:
            somecounterForzoomSwtc = 0
            somecounterForzoom = 0;
        somecounterForzoom+=1  
            
            
            
            
            
    if sizevar == 0:
        sizevar = 1;
           
    if somecounterForzoomFinal >= 5:
        somecounterForzoomSwtc = 1;
            
    if somecounterForzoomSwtc == 0:           
        if somecounterForzoom > somecounterForzoomMax: #somecounterForzoomMaxClose:
                
            someotherw = int(math.floor((somewidth) / somepercentfinal))*sizevar
            someotherh = int(math.floor((someheight) / somepercentfinal))*sizevar
            
            #(abs(128-64) * 0.1) / (64/128)
            
            #someremainsheightTwo = (abs(someotherh - someheight) * someratiow)# / (someotherh/someheight)
            #someremainswidthTwo  = (abs(someotherw - somewidth) * someratioh)# / (someotherw/somewidth)
            
            somefinalheight = (someotherh / 4) #someotherw / 2.75
            somefinalwidth = (someotherw / 2)
            
            image = someoriginalimage;
            image = image.resize((int(someotherw),int(someotherh)))
            
            #image = image.crop(((somefinalwidth * 1), (someremainsheight + somefinalheight) * 1, (somefinalwidth*1) + (width), ((someremainsheight + somefinalheight) * 1) + (height)))
            image = image.crop(((0 * -1), (0 + 0) * -1, (0*-1) + (width), ((0) * -1) + (height)))
            #image = image.crop(((somefinalwidth * 1), (someremainsheight + somefinalheight) * 1, (somefinalwidth*1) + (width), ((someremainsheight + somefinalheight) * 1) + (height)))
            
            #x = someotherw // 2 - width // 2
            #y = someotherh // 2 - height // 2
            
            #image = image.resize((int(somewidth),int(someheight)))
            #image = image.crop((0, 1000, 0 + width, 1000 + height))

            #disp.display(image)
            somecounterForzoom = 0;
            sizevar*=2
            somecounterForzoomFinal+=1
        #else:
            #somecounterForzoomSwtc = 1
            #somecounterForzoom = 0;
            
        somecounterForzoom+=1         
            
            
        
        disp.display(image)
        #somepercentfinal = (somecounterForzoom*100)/1000
         
              
              
    #if somecounterForzoomSwtc == 1:
    #somepercentfinal = (somecounterForzoom*100)/1000
    # if somecounterForzoom <= somecounterForzoomMaxFar:
    #     somecounterForzoomSwtc = 0
    #somepercentfinal = (somecounterForzoom)
    # somecounterForzoom-=1      
    #sometuple = (1)


    
    time.sleep(0.0001)

    #someotherw = int(somewidth * 0.5)
    #someotherh = int(someheight * 0.5)
    
    #someotherw = math.floor(somewidth * somepercentfinal)
    #someotherh = math.floor(someheight * somepercentfinal)
    
    text = "t0:" + str(someremainsheight)# + "/t1:" + str(someotherh)
        
    (font_width, font_height) = font.getsize(text)

    draw.text(
        (width // 2 - font_width // 2, height // 2 - font_height // 2),
        text,
        font=font,
        fill=(255, 255, 0),
    )

    disp.display(image)
    
    
    #image = image.resize((int((somewidth)*somepercentfinal),int((someheight/image_ratio)*somepercentfinal)), Image.BICUBIC)
    #image = image.resize((int((someheight/image_ratio)*somepercentfinal),int((somewidth)*somepercentfinal),), Image.BICUBIC)
    #if somepercentfinal != 0 & somecounterForzoom != 0:
                #image = image.crop((-someremainswidth, -someremainsheight, -someremainswidth + (width), -someremainsheight + (height)))

    


