import time
import struct

f = open(r'\\.\pipe\NPtest', 'r+b', 0) #wb == binary mode #r+b == string or something
i = 1

byterowlen = int(((160*128)/10)*4)
#s = "".encode('ascii')


screenrowwidth = 4#int((160*128)/10)*4

somearraytest = bytearray(screenrowwidth)

somestring = ""
for x in range(screenrowwidth):
	somestring  = somestring+ ' '
	#s = (str(screenrowwidth) +  str(" ")).encode('ascii')
	
	
#s = (somestring).encode('ascii')


while True:


	try:

		#print(somestring)

		s = 'Message[{0}]'.format(i).encode('ascii')
		
		
		#s = 'Message[{0}]'.format(i).encode('ascii')

		
		i += 1
		f.write(struct.pack('I', len(s)) + s)   # Write str length and str
		f.seek(0)                               # EDIT: This is also necessary
		#print('Wrote:', s)
		
		
		
		n = struct.unpack('I', f.read(4))[0]    # Read str length
		s = f.read(n).decode('ascii')           # Read str
		f.seek(0)                               # Important!!!
		#print('Read:', s)
		
		#s = '[{0}]'.format(s).encode('ascii')

		#f.write(somearraytest)   # Write str length and str #struct.pack('I', len(s)) + s
		#f.seek(0) # EDIT: This is also necessary
		#print('Wrote:', s)

		"""
		n = struct.unpack('I', f.read(4))[0]    # Read str length
		s = f.read(n).decode('ascii')           # Read str
		f.seek(0)                               # Important!!!
		#print('Read:', s)
		"""



		time.sleep(0)
	except Exception as ex:
		print(ex)

"""
import os
import sys
import time
import win32file

print("hello world!")


fileHandle = win32file.CreateFile("\\\\.\\pipe\\Demo", win32file.GENERIC_READ | win32file.GENERIC_WRITE, 0, None, win32file.OPEN_EXISTING, 0, None)
left, data = win32file.ReadFile(fileHandle, 4096)
print(data) # prints \rprint "hello")
"""


"""
			
f.write(struct.pack('I', len(s)) + s)   # Write str length and str
f.seek(0)                               # EDIT: This is also necessary
#print('Wrote:', s)



n = struct.unpack('I', f.read(4))[0]    # Read str length
s = f.read(n).decode('ascii')           # Read str
f.seek(0)                               # Important!!!
#print('Read:', s)
	
"""