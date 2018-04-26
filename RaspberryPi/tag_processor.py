import time
from coapthon.client.helperclient import HelperClient

# Reads the last appended line and yields that (Thanks StackOverflow)
def follow(target_file):
    target_file.seek(0, 2)
    
    while True:
        line = target_file.readline();
        if not line:
            time.sleep(0.1)
            continue
        yield line

# Posts read tags
def post_tag(tag_id):
    # Create CoAP client using library
    client = HelperClient(server=("192.168.43.75", 5683))
    response = client.post("tags", tag_id)
    if response:
    	print response.pretty_print()
    client.stop()

# Main program
if __name__ == '__main__':
    # Open file for constant reading  
    logfile = open("readtags.txt", "r")
    loglines = follow(logfile)
    
    print("Watching file: readtags.txt")
    
    # This loop yields whenever there is new data available
    for line in loglines:
        tag = line.replace("Tag scanned: ", "").strip()
        print "Posting tag: {}".format(tag)
        post_tag(tag)
        print "Finished posting"
