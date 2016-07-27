import os
import re
import shutil
import sys
import xml.etree.ElementTree as ET

tree = ET.parse('sound_settings_Tennis.xml')
root = tree.getroot()
menu_dict = {}
menu_rdict = {}
game_dict = {}
game_rdict = {}
id_sounds = 0

def buildDict():
    global id_sounds
    for item in root.findall('*/menu/*/audio_file'):
        case = item.attrib['case']
        path = item.find('./path').text
        path = path[path.rfind('/') + 1:-1];
        menu_dict[case] = path;
        menu_rdict[path] = case;
    for item in root.findall('*/player/*/audio_file'):
        case = item.attrib['case']
        path = item.find('./path').text
        pos = item.find('./position_vals').text
        pattern = re.compile('^default_\d$')
        if pattern.match(case):
            id_sounds = id_sounds + 1
            continue
        path = path[path.rfind('/') + 1:-1];
        case = case + '_:_' + pos
        game_dict[case] = path;
        game_rdict[path] = case;

def listSounds():
    print 'menu:'
    for item in menu_dict.keys():
        print '\t', item
    print 'game:'
    for item in game_dict.keys():
        print '\t', item
    print id_sounds, 'id sounds should also be provided'

def listSoundset(soundset):
    try:
        os.chdir(soundset)
    except:
        print 'Soundset ', soundset, 'does not exist'
        return
    print '\tmenu:'
    for f in [f for f in os.listdir('./menu_sounds') if f.endswith('.wav')]:
        if f in menu_rdict:
            print '\t\t', menu_rdict[f]
    print '\tgame'
    for f in [f for f in os.listdir('./player1') if f.endswith('.wav')]:
        if f in game_rdict:
            print '\t\t', game_rdict[f]
    print ''
    pattern = re.compile("^\d\.wav$")
    print '\t', len([f for f in os.listdir('.') if pattern.match(f)]), 'out of', id_sounds, 'id sounds'
    os.chdir('..')

def reverseListSoundset(soundset):
    try:
        os.chdir(soundset)
    except:
        print 'Soundset ', soundset, 'does not exist'
        return
    sounds = {}
    print '\tmenu:'
    for f in [f for f in os.listdir('./menu_sounds') if f.endswith('.wav')]:
        if f in menu_rdict:
            sounds[menu_rdict[f]] = 1
    for key in menu_dict:
        if not key in sounds:
            print '\t\t', key
    sounds = {}
    print '\n\tgame:'
    for f in [f for f in os.listdir('./player1') if f.endswith('.wav')]:
        if f in game_rdict:
            sounds[game_rdict[f]] = 1
    for key in game_dict:
        if not key in sounds:
            print '\t\t', key
    pattern = re.compile("^\d\.wav$")
    print '\n\tMissing', id_sounds - len([f for f in os.listdir('.') if pattern.match(f)]), 'id sounds'
    os.chdir('..')

def createSoundset(folder, name):
    pattern = re.compile("^[a-zA-Z1234567890_-]+$")
    if not pattern.match(folder):
        print 'Folder names may only contain, latin characters, numbers, and the special characters \'-\' and \'_\''
    else:
        try:
            os.chdir(folder)
            print 'Folder already exists'
        except:
            os.mkdir(folder)
            os.chdir(folder)
            f = open('info', 'w')
            f.write('name=' + name)
            os.mkdir('menu_sounds')
            os.mkdir('player1')
            print 'Initiated soundset ', name

def deleteSoundset(folder):
    try:
        shutil.rmtree(folder)
        print 'Deleted soundset ', folder
    except:
        print 'No folder named ', folder, ' exists'

def listSoundsets():
    pattern = re.compile("^[a-zA-Z1234567890_-]+$")
    for f in [f for f in os.listdir('.') if pattern.match(f)]:
        print f

def setSound(folder, name, filepath):
    pattern = re.compile("^\d$")
    if not filepath.endswith('.wav'):
        print filepath, ' is not a .wav file'
        return
    if not os.path.isfile(filepath):
        print filepath, ' is not a file'
        return
    try:
        os.chdir(folder)
    except:
        print folder, ' is not an existing soundset'
        return
    if name in menu_dict:
        name = menu_dict[name]
        os.chdir('menu_sounds')
    elif name in game_dict:
        name = game_dict[name]
        os.chdir('player1')
    elif pattern.match(name):
        sid = int(float(name))
        if sid > id_sounds:
            print name, 'exceeds expected number of soundset ids(' + str(id_sounds) + ')'
            return
        name = str(sid) + ".wav"
    else:
        print name, 'is not a valid sound id'
        return
    try:
        shutil.copyfile(filepath, name)
        print 'File copied'
    except:
        print 'Failed to copy file'

def printHelp():
    print 'Welcome to LEAP Soundset Manager'
    print 'Usage: manager <command> [arguments]'
    print 'Available commands: list, create, delete, soundsets, set, help\n'
    print 'list:'
    print '\tUsage: manager list [soundset]'
    print '\tLists all sounds used in the game with their ids'
    print '\tIf a soundset is specified then the command will list all sounds\n\tcurrently provided within the soundset followed by all sounds\n\tmissing from the soundset\n'
    print 'create:'
    print '\tUsage: manager create <folder> <name>'
    print '\tPrepares a new soundset under folder <folder> named <name>\n\tThis folder can only contain latin characters, numerals and hyphens\n\tThe in-game name of the soundset can contain any characters\n'
    print 'delete:'
    print '\tUsage: manager delete <folder>'
    print '\tDeletes the soundset located in the specified folder\n'
    print 'soundsets:'
    print '\tUsage: manager soundsets'
    print '\tPrints a list of existing soundsets\n'
    print 'set:'
    print '\tUsage: manager set <folder> <sound_id> <sound_file>'
    print '\tAdds a .wav sound file to the soundset\n\tThe folder argument specifies the target soundset\n\tThe sound id argument represents the target game sound\n\tFinally the full path to the sound file is required\n'
    print 'help:'
    print '\tUsage: manager help'
    print '\tPrints this message'

def parseArguments(argv):
    argc = len(argv)
    if (argc == 1):
        print 'No arguments provided'
    else:
        if argv[1] == 'list':
            if argc == 3:
                print 'Overriden sounds:'
                listSoundset(argv[2])
                print 'Missing sounds:'
                reverseListSoundset(argv[2])
            else:
                listSounds()
        elif argv[1] == 'create':
            if argc == 4:
                createSoundset(argv[2], argv[3])
            else:
                print 'Missing arguments'
        elif argv[1] == 'delete':
            if argc == 3:
                deleteSoundset(argv[2])
            else:
                print 'Missing arguments'
        elif argv[1] == 'soundsets':
            listSoundsets()
        elif argv[1] == 'set':
            if argc == 5:
                setSound(argv[2], argv[3], argv[4])
            else:
                print 'Missing arguments'
        elif argv[1] == 'help':
            printHelp()
        else:
            print 'Unknown command: ' + argv[1]
            print 'Use "manager help" to display usage information'
    raw_input('\nPress Enter to continue...')
    return

buildDict()
parseArguments(sys.argv)
