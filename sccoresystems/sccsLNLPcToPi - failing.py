import time
import struct

#https://stackoverflow.com/questions/51026315/how-to-solve-unicodedecodeerror-in-python-3-6/51027262#51027262
# encoding=utf8  
#import sys  
#reload(sys)  
#sys.setdefaultencoding('utf8')

#import sys, codecs
#sys.stdout = codecs.getwriter('utf-8')(sys.stdout.detach())

f = open(r'\\.\pipe\sccsmscpcTopi', 'r+b', 0)
i = 1

screenrowwidth = 1
somestring = ' '
for x in range(screenrowwidth + 1):
	somestring  = somestring + ' '
	
	
while True:

	#s = somestring.encode('ascii')

    s = 'Message[{0}]'.format(i).encode('ascii')
    i += 1
        
    f.write(struct.pack('I', len(s)) + s)   # Write str length and str
    f.seek(0)                               # EDIT: This is also necessary
    #print('Wrote:', s)

    n = struct.unpack('I', f.read(4))[0]    # Read str length
    s = f.read(n).decode('ascii')           # Read str
    f.seek(0)                               # Important!!!
    #print('Read:', s)

	#bytes = bytearray(contents, 'ascii') #cp1252




    time.sleep(0)
