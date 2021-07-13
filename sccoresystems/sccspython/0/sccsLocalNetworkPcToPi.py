#made by ninekorn
import socket
#import tqdm
import os
import time
import sys
#httpsstackoverflow.comquestions1271320resetting-generator-object-in-python
#import more_itertools as mit

SEPARATOR = SEPARATOR
BUFFER_SIZE = 4096 # send 4096 bytes each time step

hostmain = 0.0.0.0
portmain = 5001

# the ip address or hostname of the server, the receiver #192.168.1.101
host = 192.168.0.121 #192.168.0.107
# the port, let's use 5001
port = 5000
# the name of file we want to send, make sure it exists
filename = sccsmsgPcToPi.txt
# get the file size
filesize = os.path.getsize(filename)

s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
#s = socket.socket()
s.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)

# bind the socket to our local address
#s.bind((hostmain, portmain))


# enabling our server to accept connections
# 5 here is the number of unaccepted connections that
# the system will allow before refusing new connections
#s.listen(5)


someswtc = 0

somecounter = 0
somecountermax = 100

somelistencounter = 0
somelistencountermax = 0

try
       
    #print(f[+] Connecting to {host}{port})
    print(Connecting to  + host +   + str(port))
    s.connect((host, port))
    print([+] Connected.)

    # send the filename and filesize
    s.send(PcToPiMsg.encode()) #{filename}{SEPARATOR}{filesize}

    # start sending the file
    #progress = tqdm.tqdm(range(filesize), filename, unit=B, unit_scale=True, unit_divisor=1024)
    someswtc = 0
except Exception as ex
    print(ex)
    someswtc = -1

somereconnectcounter = 0
somereconnectcountermax = 100



def generator(n)
    i = 0
    while i  n
        yield i

    
#y = mit.seekable(generator())
y = generator(-1)
#httpsstackoverflow.comquestions55024440brokenpipeerror-errno-32-broken-pipe-error-when-running-gans
if __name__ == __main__
    for x in y
        
	


        if someswtc == -2
            s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
            s.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
            
            someswtc = -1
        
        
        #the connection was unsuccessful at the program creation. retrying with countdown
        if someswtc == -1
            if somereconnectcounter = somereconnectcountermax          
                try     
                    #print(f[+] Connecting to {host}{port})
                    #print(f[+] Connecting to ipport , (host,port))
                    print(Connecting to  + host +   + str(port))
                    s.connect((host, port))
                    print([+] Connected.)

                    filename = sccsmsgPcToPi.txt
                    # get the file size
                    filesize = os.path.getsize(filename)
                    
                    # send the filename and filesize
                    #s.send(f{filename}{SEPARATOR}{filesize}.encode())
                    s.send(PcToPiMsg.encode())
             
                    # start sending the file
                    #progress = tqdm.tqdm(range(filesize), filename, unit=B, unit_scale=True, unit_divisor=1024)
                    someswtc = 0
                except Exception as ex
                    print(ex)
                    someswtc = -1
                somereconnectcounter = 0
            somereconnectcounter += 1
        #the connection was unsuccessful at the program creation. retrying with countdown  
        
        
        
        #sending file to the pi client
        if someswtc == 0
            print(sending file to pi.)
            with open(filename, rb) as f
                while True
                    # read the bytes from the file
                    bytes_read = f.read(BUFFER_SIZE)
                    if not bytes_read
                        # file transmitting is done
                        break
                    # we use sendall to assure transimission in 
                    # busy networks
                    s.sendall(bytes_read)
                    # update the progress bar
                    #progress.update(len(bytes_read))
            # close the socket
            s.close()
            someswtc = 1
        #sending file to the pi client    
            
            
          
        #waiting for the pi client to respond that it has received the file.
        if  someswtc == 1           
            if somelistencounter = somelistencountermax
                
                
                
                # create the server socket
                # TCP socket
                #s = socket.socket()
                s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
                s.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
                # bind the socket to our local address
                s.bind((hostmain, portmain))
                #s.setblocking(0)
                # enabling our server to accept connections
                # 5 here is the number of unaccepted connections that
                # the system will allow before refusing new connections
                s.listen(0)
                #print('[] Listening as, ' + string(SERVER_HOST) + , + stringString(SERVER_PORT))
                #print([] Listening as {{SERVER_HOST}}{{SERVER_PORT}})
                print([] Listening ipport,(hostmain,portmain))
                #print([] Listening port,SERVER_PORT)
                
                try
                    # accept connection if there is any
                    client_socket, address = s.accept() 
                    # if below code is executed, that means the sender is connected
                    print('[+]connected.',address)

                    # receive the file infos
                    # receive using client socket, not server socket
                    received = client_socket.recv(BUFFER_SIZE).decode()
                    #filename, filesize = received.split(SEPARATOR)
                    # remove absolute path if there is
                    #filename = os.path.basename(filename)
                    # convert to integer
                    #filesize = int(filesize)
                    filename = received
                    
                    somefilesizeswtc = 0
                
                    try
                        filename,junk = filename.split()
                        somefilesizeswtc = 1
                    except   
                        # convert to integer
                        #filesize = int(filesize)
                        somefilesizeswtc = 0
                        
                    if somefilesizeswtc == 1
                        #with open(filename, wb) as f
                        textfile = open(filename, w)
                        textfile.write(# + junk)
                        textfile.close()
                        # close the client socket
                        client_socket.close()
                        # close the server socket
                        s.close()
                    else
                        print(receiving confirmation file from RaspPi)
                        
                        with open(filename, wb) as f
                            while True
                                # read 1024 bytes from the socket (receive)
                                bytes_read = client_socket.recv(BUFFER_SIZE)
                                
                                if not bytes_read
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
                        print(received confirmation file from RaspPi)
                    
                    
                    # start receiving the file from the socket
                    # and writing to the file stream
                    #progress = tqdm.tqdm(range(filesize), filename, unit='B', unit_scale=True, unit_divisor=1024)
                    
                    
                    
                    
                    someswtc = -2
                except Exception as ex
                    print(ex)

            
                somelistencounter = 0
            somelistencounter += 1
        #waiting for the pi client to respond that it has received the file.   
        





        #resetting the generator
        if somecounter = somecountermax           
            #y.seek(0)
            #print(x)
            #print(reseted iterator generator. program is still alive.)
            somecounter = 0
        somecounter += 1
        #resetting the generator
        
        
        
   
        #sleep the thread. 
        time.sleep(0)
        #sleep the thread.