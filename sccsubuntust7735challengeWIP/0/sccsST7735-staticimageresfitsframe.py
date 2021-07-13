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

BORDER = 20
FONTSIZE = 24

# Create TFT LCD display class.
#disp = ST7735.ST7735(port=0, cs=0, dc=24, backlight=None, rst=25, width=128, height=160, rotation=0, invert=False, offset_left=0, offset_top=0)
disp = ST7735.ST7735(port=0, cs=0, dc=24, backlight=None, rst=25, width=128, height=160, rotation=0, invert=False, offset_left=0, offset_top=0)

# Initialize display.
disp.begin()

width = disp.width
height = disp.height

# Load an image.
print('Loading image...')
path = '/home/kali/Desktop/raspberriPiSetupNImage/'
#image = Image.open(path + 'blinka.jpg')
#image = Image.open(path + '8e3a980f0f964cc539b4cbbba2654bb660db6f52_arduino-uno-pinout-diagram.jpeg')

image = Image.open(path + 'R-Pi-4-GPIO-Pinout.webp')

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


#image = image.rotate(0).resize((scaled_width, scaled_height), Image.BICUBIC)
image = image.rotate(0).resize((int(width),int(height/image_ratio)), Image.BICUBIC)

x = (scaled_width // 2)- (width // 2)
y = (scaled_height // 2)- (height // 2)

#x =(width // 2)
#y = (height // 2)

#image = image.crop((y, x, y + height, x + width))
image = image.crop((0, 0, 0 + (width), 0 + (height)))

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
text = "/w:" + str(scaled_width) + "\n" + "/h:" + str(scaled_height) +  "/x:" + str(x) + "/y:" + str(y)

(font_width, font_height) = font.getsize(text)

draw.text(
    (width // 2 - font_width // 2, height // 2 - font_height // 2),
    text,
    font=font,
    fill=(255, 255, 0),
)

disp.display(image)
