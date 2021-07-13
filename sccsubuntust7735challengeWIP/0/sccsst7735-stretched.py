from PIL import Image
from PIL import ImageDraw
from PIL import ImageFont

import ST7735

disp = ST7735.ST7735(port=0, cs=0, dc=24, backlight=None, rst=25, width=128, height=160, rotation=0, invert=False,offset_left=0, offset_top=0)
disp.begin()

#ORIGINAL#WIDTH = disp.width
#ORIGINAL#HEIGHT = disp.height
width = disp.width
height = disp.height

disp.begin()

# Load an image.
print('Loading image...')
image = Image.new("RGB", (width, height))
draw = ImageDraw.Draw(image)
draw.rectangle((0, 0, width, height), outline=0, fill=(0, 0, 0))

disp.display(image)
# Draw the image on the display hardware.
print('Drawing image')

#image = Image.open('blinka.jpg')
path = '/home/kali/Desktop/raspberriPiSetupNImage/'
#image = Image.open(path + 'blinka.jpg')
#image = Image.open(path + '8e3a980f0f964cc539b4cbbba2654bb660db6f52_arduino-uno-pinout-diagram.jpeg')

image = Image.open(path + 'R-Pi-4-GPIO-Pinout.webp')




# Resize the image and rotate it so matches the display.
image = image.rotate(0).resize((128,160))

disp.display(image)


# Create blank image for drawing.
# Make sure to create image with mode 'RGB' for full color.
#ST7735.py#if disp.rotation % 180 == 90:
#ST7735.py#    height = disp.width  # we swap height/width to rotate it to landscape!
#ST7735.py#    width = disp.height
#ST7735.py#else:
#ST7735.py#    width = disp.width  # we swap height/width to rotate it to landscape!
#ST7735.py#    height = disp.height
#ST7735.py#image = Image.new("RGB", (width, height))

# Get drawing object to draw on image.
#ST7735.py#draw = ImageDraw.Draw(image)

# Draw a black filled box to clear the image.
#ST7735.py#draw.rectangle((0, 0, width, height), outline=0, fill=(0, 0, 0))
#ST7735.py#disp.image(image)

#ST7735.py#image = Image.open("blinka.jpg")

# Scale the image to the smaller screen dimension
#ST7735.py#image_ratio = image.width / image.height
#ST7735.py#screen_ratio = width / height
#ST7735.py#if screen_ratio < image_ratio:
#ST7735.py#    scaled_width = image.width * height // image.height
#ST7735.py#    scaled_height = height
#ST7735.py#else:
#ST7735.py#    scaled_width = width
#ST7735.py#    scaled_height = image.height * width // image.width
#ST7735.py#image = image.resize((scaled_width, scaled_height), Image.BICUBIC)

# Crop and center the image
#ST7735.py#x = scaled_width // 2 - width // 2
#ST7735.py#y = scaled_height // 2 - height // 2
#ST7735.py#image = image.crop((x, y, x + width, y + height))

# Display image.
#ST7735.py#disp.image(image)


















#img = Image.new('RGB', (WIDTH, HEIGHT))
#ORIGINAL#draw = ImageDraw.Draw(img)

# Load default font.
#ORIGINAL#font = ImageFont.load_default()

# Write some text
#ORIGINAL#draw.text((5, 5), "Hello World!", font=font, fill=(255, 255, 255))

# Write buffer to display hardware, must be called to make things visible on the
# display!
#ORIGINAL#disp.display(img)