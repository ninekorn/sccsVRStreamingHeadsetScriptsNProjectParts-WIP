#https://github.com/jake-walker/st7735-python
from PIL import Image

import ST7735

# Create TFT LCD display class.
#disp = ST7735.ST7735(port=0, cs=0, dc=24, backlight=None, rst=25, width=128, height=160, rotation=0, invert=False, offset_left=0, offset_top=0)
disp = ST7735.ST7735(port=0, cs=0, dc=24, backlight=None, rst=25, width=128, height=160, rotation=0, invert=False, offset_left=0, offset_top=0)

# Initialize display.
disp.begin()

width = disp.width  # we swap height/width to rotate it to landscape!
height = disp.height

# Load an image.
print('Loading image...')
#image = Image.open('raspio-portsplus.jpg')
path = '/home/kali/Desktop/raspberriPiSetupNImage/'
#image = Image.open(path + 'blinka.jpg')
#image = Image.open(path + '8e3a980f0f964cc539b4cbbba2654bb660db6f52_arduino-uno-pinout-diagram.jpeg')
image = Image.open(path + 'R-Pi-4-GPIO-Pinout.webp')


# Resize the image and rotate it so matches the display.
#image = image.rotate(90).resize((128,160))

image_ratio = image.width / image.height
screen_ratio = width / height

if screen_ratio < image_ratio:
    scaled_width = image.width * height // image.height
    scaled_height = height
else:
    scaled_width = width
    scaled_height = image.height * width // image.width
    
image = image.rotate(90).resize((scaled_width, scaled_height), Image.BICUBIC)

# Crop and center the image
x = scaled_width // 2 - width // 2
y = scaled_height // 2 - height // 2
image = image.crop((x, y, x + width, y + height))


# Draw the image on the display hardware.
print('Drawing image')
disp.display(image)