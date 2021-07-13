import time
import struct
import os
import PIL 
from PIL import Image 


#f = open(r'\\.\pipe\sccsmscpcTopi', 'r+b', 0)
i = 1

screenrowwidth = 82944
somestring = ' '
for x in range(screenrowwidth):
	somestring  = somestring + ' '
	
#path = '/home/kali/Desktop/raspberriPiSetupNImage/'
#path = 'C:/Users/steve/OneDrive/Desktop/backup recovered/#uploaded/sccoresystems'
path = r'C:\Users\steve\OneDrive\Desktop\#screencapture'

filename = 'sccsscreencapture.jpg'
finalpath = ''

copypastepath = r'C:\Users\steve\OneDrive\Desktop\pythonscreencapture'
finalcopypastepath = ''

while True:

	finalpath = os.path.join(path, filename)
	image = Image.open(finalpath)

	#finalcopypastepath = os.path.join(copypastepath, filename)
	#image = Image.save(finalcopypastepath)

	print(finalpath)
	i+=1
	time.sleep(0)
