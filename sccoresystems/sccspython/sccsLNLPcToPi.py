#made by ninekorn
import socket
import os
import time
import sys
import struct
#https://stackoverflow.com/questions/1271320/resetting-generator-object-in-python
#import more_itertools as mit
#import tqdm

# device's IP address
PC_HOST = "192.168.0.107"
PC_PORT = 5001

# device's IP address
SERVER_HOST = "0.0.0.0"
SERVER_PORT = 5000

# receive 4096 bytes each time
BUFFER_SIZE = 4096
SEPARATOR = "<SEPARATOR>"

clientfilename = "sccsmsgPiToPc.txt"
# get the file size
clientfilesize = os.path.getsize(clientfilename)

# create the server socket
# TCP socket
#s = socket.socket()
s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
s.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)

# bind the socket to our local address
s.bind((SERVER_HOST, SERVER_PORT))

# enabling our server to accept connections
# 5 here is the number of unaccepted connections that
# the system will allow before refusing new connections
s.listen(5)
print("Listening " + str(SERVER_HOST) + " " + str(SERVER_PORT)) #f"[*] Listening as {SERVER_HOST}:{SERVER_PORT}"

# accept connection if there is any
client_socket, address = s.accept()
# if below code is executed, that means the sender is connected
print(str(address) + " is connected" ) #f"[+] {address} is connected."

# receive the file infos
# receive using client socket, not server socket
received = client_socket.recv(BUFFER_SIZE).decode()
#filename = received
#filesize = 0
#filename, filesize = received.split(SEPARATOR)
# remove absolute path if there is
filename = os.path.basename(received)
# convert to integer
filesize = 0#int(filesize)

somecounter = 0
somecountermax = 1;
# start receiving the file from the socket
# and writing to the file stream
#f"Receiving {filename}"
#progress = tqdm.tqdm(range(filesize), "Receiving " + str(filename), unit="B", unit_scale=True, unit_divisor=1024,mininterval=0,miniters=1,smoothing=1)

someswtc = 0
somefinalcounter = 0;



f = open(r'\\.\pipe\sccsmscpcTopi', 'r+b', 0)
i = 1

screenrowwidth = 8294400
somestring = ""

for x in range(screenrowwidth):
    somestring  = somestring + ' '

someitergenresetcounter = 0
someitergenresetcountermax = 0

def generator(n):
    i = 0    
    while i > n:
        yield i
    #i += 1
   
#y = mit.seekable(generator())
y = generator(-1)

if __name__ == "__main__":
    for x in y:

        #section to read the buffer
        s = somestring.encode('ascii') #'Message[{0}]'.format(i).encode('ascii')
        i += 1

        f.write(struct.pack('I', len(s)) + s)   # Write str length and str
        f.seek(0)                               # EDIT: This is also necessary
        print('Wrote:', s)

        n = struct.unpack('I', f.read(4))[0]    # Read str length
        s = f.read(n).decode('ascii')           # Read str
        f.seek(0)                               # Important!!!
        print('Read:', s)
        #section to read the buffer
        
        if someswtc == -1:
        
            s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
            s.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)                  
                            
            # bind the socket to our local address
            s.bind((SERVER_HOST, SERVER_PORT))

            # enabling our server to accept connections
            # 5 here is the number of unaccepted connections that
            # the system will allow before refusing new connections
            s.listen(5)
            print("Listening " + str(SERVER_HOST) + " " + str(SERVER_PORT)) #f"[*] Listening as {SERVER_HOST}:{SERVER_PORT}"
            
            
            try:
                # accept connection if there is any
                client_socket, address = s.accept()
                # if below code is executed, that means the sender is connected
                print(str(address) + " is connected" ) #f"[+ {address} is connected."
                somesocketreceiverswtc = 1
                # receive the file infos
                # receive using client socket, not server socket
                received = client_socket.recv(BUFFER_SIZE).decode()
                #filename, filesize = received.split(SEPARATOR)
                filename = received
                filesize = 0
                
                #filename, filesize = received.split(SEPARATOR)
                # remove absolute path if there is
                #filename = os.path.basename(filename)
                # convert to integer
                #filesize = int(filesize)
                somecounter = 0
                somecountermax = 1;
                # start receiving the file from the socket
                # and writing to the file stream
                #f"Receiving {filename}"
                #progress = tqdm.tqdm(range(filesize), "Receiving " + str(filename), unit="B", unit_scale=True, unit_divisor=1024,mininterval=0,miniters=1,smoothing=1)
                someswtc = 0
            
            except Exception as ex:
                print(ex)
                
                
                
                
                

            



        if someswtc == 0:
            
            somefilesizeswtc = 0

            try:
                filename,junk = filename.split(">>>>")
                somefilesizeswtc = 1
            except:   
                # convert to integer
                #filesize = int(filesize)
                somefilesizeswtc = 0
                
            if somefilesizeswtc == 1:
                #with open(filename, "wb") as f:
                textfile = open(filename, "w")
                textfile.write("#" + junk)
                textfile.close()
                # close the client socket
                client_socket.close()
                # close the server socket
                s.close()
            else:
                with open(filename, "wb") as f:
                    while True:
                        # read 1024 bytes from the socket (receive)
                        bytes_read = client_socket.recv(BUFFER_SIZE)
                        
                        if not bytes_read:
                            # nothing is received
                            # file transmitting is done
                            break   
                        # write to the file the bytes we just received
                        f.write(bytes_read)
                        # update the progress bar
                        #progress.update(len(bytes_read))
                        
                # close the client socket
                client_socket.close()
                # close the server socket
                s.close()
            someswtc = 1
            
                
        if someswtc == 1:      
             
            print("creating socket")
            
            # create the server socket
            # TCP socket
            #s = socket.socket()
            s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
            s.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
            #s.setblocking(1)                

            someswtc = 2
            

        if someswtc == 2:
            print("ready to receive another file. advising server.")    
                           
            # bind the socket to our local address
            #s.bind((PC_HOST, PC_PORT))
            try:           
                print("[+] Connecting.")
                s.connect((PC_HOST, PC_PORT))
                print("[+] Connected.")
                someswtc = 3
            except Exception as ex:
                print(ex)
                

            #s.send(("PiToPc-received-" + str(filename)).encode())
            #someswtc = 0
                
        if someswtc == 3:
            #print("wait for the programmer ninekorn to insert code here.")
            print("sending confirmation receipt file to server with request for another file or not.")
            #s.send(("PiToPc-received-" + str(filename)).encode())
            #s.setblocking(1)
            
            
            
            #s.send(f"{clientfilename}{SEPARATOR}{clientfilesize}".encode())
            s.send("PiToPcReceipt".encode())
            #s.close()
            #s = socket.socket()
            #s.bind()
            #s.listen(5)
            
            # start sending the file
            #progress = tqdm.tqdm(range(clientfilesize), clientfilename, unit="B", unit_scale=True, unit_divisor=1024,mininterval=0,miniters=1,smoothing=1)

            with open(clientfilename, "rb") as f:
                while True:
                    # read the bytes from the file
                    bytes_read = f.read(BUFFER_SIZE)
                    if not bytes_read:
                        # file transmitting is done
                        break
                    # we use sendall to assure transimission in 
                    # busy networks
                    s.sendall(bytes_read)
                    # update the progress bar
                    #progress.update(len(bytes_read))
            # close the socket
            s.close()

            someswtc = -1
         
        if someitergenresetcounter >= someitergenresetcountermax:
            #y.seek(0)
            #print(x)
            someitergenresetcounter = 0
        someitergenresetcounter += 1

        time.sleep(0)